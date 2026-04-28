import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'grade',
})
export class GradePipe implements PipeTransform {
  transform(value:number):string {
    switch (value){
      case 1: return 'Outstanding';
      case 2: return 'Excellent';
      case 3: return 'Good';
      case 4: return 'Average';
      default: return 'Unknown';

    }
  }
}
