import { Student,Pass_Marks } from "./student.js";

export function getGrade(marks: number): string {
  if (marks >= 90) return "A";
  if (marks >= 75) return "B";
  if (marks >= Pass_Marks) return "C";
  return "F";
}

export function getTopper(students: Student[]): Student {
  return students.reduce((topper, current) =>
    current.marks > topper.marks ? current : topper
  );
}