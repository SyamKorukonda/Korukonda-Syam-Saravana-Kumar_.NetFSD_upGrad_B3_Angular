import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ContactService } from '../contact-service';
import { Contact } from '../models/contact.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-contact-list',
  standalone:true,
  imports: [CommonModule,FormsModule],
  templateUrl: './contact-list.html',
  styleUrl: './contact-list.css',
})
export class ContactList {
  contacts: Contact[] = [];

  constructor(
    private service: ContactService,
    private router: Router
  ) {
    this.contacts = this.service.getContacts();
  }

  viewContact(id: number) {
    this.router.navigate(['/contact', id]);
  }

  deleteContact(id: number) {
    this.service.deleteContact(id);
    this.contacts = this.service.getContacts();
  }
}
