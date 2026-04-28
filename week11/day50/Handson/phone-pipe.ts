import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'phone',
  standalone: true
})
export class PhonePipe implements PipeTransform {
  public transform(value:string):string {
    return value.replace(/(\d{3})(\d{3})(\d{4})/,'$1-$2-$3');
  }
}
