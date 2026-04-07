using Dapper;
using Microsoft.Data.SqlClient;
using WebApplication11.Models;
namespace WebApplication8.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly  string _conStr;

        public StudentRepository(IConfiguration config)
        {
            _conStr = config.GetConnectionString("DefaultConnection");
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_conStr);
        }

       public  IEnumerable<Student> GetStudentsWithCourses()
        {
            using (var conn = GetConnection())
            {
                var sql = @"SELECT s.StudentId,s.StudentName,s.CourseId,c.CourseId,c.CourseName
                          FROM Students s
                           JOIN Courses c ON  s.CourseId=c.CourseId";

                var res = conn.Query<Student, Course, Student>
                    (sql, (student, course) =>
                            {
                                student.Course = course;
                                return student;
                            },
                            splitOn: "CourseId"
                            );
                return res;

            };
            
        }


        public IEnumerable<Course> GetCoursesWithStudents()
        {
            using(var conn = GetConnection())
            {
                var sql = @"SELECT c.CourseId, c.CourseName,
                         s.StudentId, s.StudentName, s.CourseId
                        FROM Courses c
                         LEFT JOIN Students s ON c.CourseId=s.CourseId";
                
                var courObj=new Dictionary<int, Course>();

                var res = conn.Query<Course, Student, Course>(sql,
                    (course, student) =>
                    {
                        //Check if course already exists 
                        if (!courObj.TryGetValue(course.CourseId, out var currentCourse))
                        {
                            //If not exists then add new course

                            currentCourse = course;
                            currentCourse.Students = new List<Student>();
                            courObj.Add(course.CourseId, currentCourse);
                        }

                        if (student != null) //If student exists  
                        {
                            //then add student to that course’s student list

                            currentCourse.Students.Add(student);
                        }
                        return currentCourse;//Return the mapped object for each row to Dapper

                    }, splitOn: "StudentId"
                   );
                return courObj.Values;
            };
        }

    }
}
