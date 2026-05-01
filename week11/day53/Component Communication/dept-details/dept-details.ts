import { Component ,Input} from '@angular/core';
import { Dept } from '../models/deptModel';

@Component({
  selector: 'app-dept-details',
  imports: [],
  templateUrl: './dept-details.html',
  styleUrl: './dept-details.css',
})
export class DeptDetails {

  // This is the Child Component

  @Input()
  deptObj:Dept={ deptno: 0,  dname : "", loc : "" };
}
