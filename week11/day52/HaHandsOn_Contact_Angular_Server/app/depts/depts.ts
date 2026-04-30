import { Component,OnInit } from '@angular/core';

@Component({
  selector: 'app-depts',
  imports: [],
  templateUrl: './depts.html',
  styleUrl: './depts.css',
})
export class Depts  implements OnInit {

  public deptslist:string[]=["sales","Account","Testing","Marketing","Develoupment"];

  constructor(){}
  ngOnInit(){}
}
