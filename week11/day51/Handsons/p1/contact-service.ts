import { Injectable } from '@angular/core';
import { Contact } from './models/contact.model';

@Injectable({
  providedIn: 'root',
})
export class ContactService {
  private contacts: Contact[] = [
    { id: 1, name: 'Syam', email: 'syam@gmail.com', phone: '9876543210' },
    { id: 2, name: 'Ravi', email: 'ravi@gmail.com', phone: '9123456780' },
    { id: 3, name: 'kumar', email: 'kumar@gmail.com', phone: '9123456444' },
    { id: 4, name: 'karthick', email: 'krith@gmail.com', phone: '9123456555' }


  ];

  // get all
  getContacts(): Contact[] {
    return this.contacts;
  }

  // add
  addContact(contact: Contact): void {
    this.contacts.push(contact);
  }

  // get by id
  getContactById(id: number): Contact | undefined {
    return this.contacts.find(c => c.id === id);
  }

  deleteContact(id: number): void {
  this.contacts = this.contacts.filter(c => c.id !== id);
}

}
