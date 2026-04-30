import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class AuthServices {

readonly API_URL:string="https://localhost:7152/api/Auth/";
constructor(private httpClient:HttpClient){}

checkUserCredentails(userName:string,password:string)
{
  let userObj={username:userName,password:password};

  console.log("Sending:", userObj);
  return this.httpClient.post(this.API_URL+"login",userObj);
}

}
