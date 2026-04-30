import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Contact } from '../models/contact';

@Injectable({
  providedIn: 'root',
})
export class ContactServices {

  readonly API_URL:string="https://localhost:7152/api/Contacts/";

  constructor(private httpClient:HttpClient){}

  private getHeaders(){
    let token=sessionStorage.getItem("AUTH_TOKEN"); 
    return{
      Authorization: `Bearer ${token}`
    };
  }

  getContacts(){
    return this.httpClient.get<Contact[]>(this.API_URL,{headers:this.getHeaders()});
  }

  getContactById(id:number){
    return this.httpClient.get<Contact>(this.API_URL+id,{headers:this.getHeaders()});
  }

  addContact(contact:Contact){
    return this.httpClient.post<Contact>(this.API_URL,contact,{headers:this.getHeaders()});
  }

  updateContact(id:number,contact:Contact){
    return this.httpClient.put<Contact>(this.API_URL+id,contact,{headers:this.getHeaders()});
  }

  deleteContact(id:number){
    return this.httpClient.delete<Contact>(this.API_URL+id,{headers:this.getHeaders()});
  }

}
