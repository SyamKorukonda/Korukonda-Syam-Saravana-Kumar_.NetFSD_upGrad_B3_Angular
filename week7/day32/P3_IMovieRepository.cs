using WebApplication6.Models;
namespace WebApplication6.Repositories
{
    public interface IMovieRepository
    {
        IEnumerable<Movie> GetAll();

        Movie GetById(int id);

        void Add(Movie movie);  
        void Update(Movie movie);
        void Delete(int id);



    }
}
