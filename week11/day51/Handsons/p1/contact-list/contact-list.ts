import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ContactService } from '../contact-service';
import { Contact } from '../models/contact.model';

@Component({
  selector: 'app-contact-list',
  standalone:true,
  imports: [CommonModule,FormsModule],
  templateUrl: './contact-list.html',
  styleUrl: './contact-list.css',
})
export class ContactList {
  contacts: Contact[] = [];

  newContact: Contact = {
    id: 0,
    name: '',
    email: '',
    phone: ''
  };

  constructor(private service: ContactService) {
    this.contacts = this.service.getContacts();
  }

  addContact() {
    this.service.addContact({ ...this.newContact });//spread operator  copy all properties of one object into new object
    this.contacts = this.service.getContacts();
    this.newContact = { id: 0, name: '', email: '', phone: '' };
  }

  deleteContact(id: number) {
    this.service.deleteContact(id);
    this.contacts = this.service.getContacts();
  }
}
