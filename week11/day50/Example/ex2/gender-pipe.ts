import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'gender',
})
export class GenderPipe implements PipeTransform {
 public transform(value:string):string 
 {
      let output:string="";
      if(value=="M"||value=="m") output="Male";
      if(value=="F"||value=="f") output="Female";

      return output;
  }
}
