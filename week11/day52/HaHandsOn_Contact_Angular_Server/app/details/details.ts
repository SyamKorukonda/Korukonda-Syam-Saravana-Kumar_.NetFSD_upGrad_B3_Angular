import { Component } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';

@Component({
  selector: 'app-details',
  imports: [RouterLink],
  templateUrl: './details.html',
  styleUrl: './details.css',
})
export class Details {
  empObj:any={};

  public empArray:any[]=[
     {empno: 1025, ename : "Scott", job : "Manager", sal : 56000},
    {empno: 1026, ename : "Smith", job : "Sr.Manager", sal : 65000},
    {empno: 1027, ename : "Sandy", job : "Lead", sal : 45000},
    {empno: 1028, ename : "Sam", job : "Group Manager", sal : 75000}
  ];

  constructor(private activateRoute:ActivatedRoute){
     // Reading route parameter value 
     let id=this.activateRoute.snapshot.params["id"];

     this.empObj=this.empArray.find(item=>item.empno==id);
  }

}
