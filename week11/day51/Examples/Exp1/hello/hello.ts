import { Component } from '@angular/core';
import { DataService } from '../data-service';

@Component({
  selector: 'app-hello',
  providers:[DataService],
  imports: [],
  templateUrl: './hello.html',
  styleUrl: './hello.css',
})
export class Hello {
  public message:string="";

  //service injectio in Component

  constructor(private dataService:DataService){}

  public buttonClick():void
  {
    // invoke Service methods
    this.message=this.dataService.getMessage("Syam");
  }
}
