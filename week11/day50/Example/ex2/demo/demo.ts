import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { GenderPipe } from '../gender-pipe';
import { CommonModule } from '@angular/common';
import {GradePipe} from '../grade-pipe';


@Component({
  selector: 'app-demo',
  imports: [FormsModule,CommonModule, GenderPipe,GradePipe],
  templateUrl: './demo.html',
  styleUrl: './demo.css',
})
export class Demo {
  public  usersArray:any[]  = 
  [
    {uname  :  "Smith",  gender:  "M",grade:1},
    {uname  :  "Scott",  gender:  "m",grade:2},
    {uname  :  "Nancy",  gender:  "F",grade:3},
    {uname  :  "Ruth",   gender:  "f",grade:4},
  ];

}
