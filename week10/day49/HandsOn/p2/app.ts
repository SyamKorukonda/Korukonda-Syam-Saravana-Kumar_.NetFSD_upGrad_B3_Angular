import { Student } from "./student.js";
import { getGrade, getTopper } from "./studentService.js";
import { formatName, calculateAverage } from "./utils.js";


const students: Student[] = [
  { id: 1, name: "syam", marks: 85 },
  { id: 2, name: "kumar", marks: 92 },
  { id: 3, name: "ravi", marks: 70 }
];


console.log("Formatted Names:");
students.forEach(s => {
  console.log(formatName(s.name));
});

console.log("------------------");


console.log("Grades:");
students.forEach(s => {
  console.log(`${s.name}: ${getGrade(s.marks)}`);
});

console.log("------------------");


console.log("Average Marks:", calculateAverage(students));

console.log("------------------");


const topper = getTopper(students);
console.log("Topper:", topper);