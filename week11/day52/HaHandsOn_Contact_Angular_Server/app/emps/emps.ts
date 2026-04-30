import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-emps',
  imports: [RouterLink],
  templateUrl: './emps.html',
  styleUrl: './emps.css',
})
export class Emps {
  public emps:any[]  = [
    {empno: 1025, ename : "Scott", job : "Manager", sal : 56000},
    {empno: 1026, ename : "Smith", job : "Sr.Manager", sal : 65000},
    {empno: 1027, ename : "Sandy", job : "Lead", sal : 45000},
    {empno: 1028, ename : "Sam", job : "Group Manager", sal : 75000},
  ];

  constructor() 
  {}
}
