import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class DataService {
  public getMessage(uname:string):string{
    return `Hello ${uname} , Good morning..`;
  }
}
