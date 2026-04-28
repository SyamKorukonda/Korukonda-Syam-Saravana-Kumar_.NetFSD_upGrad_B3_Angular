import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { ContactService } from '../contact-service';
import { Contact } from '../models/contact.model';

@Component({
  selector: 'app-add-contact',
  imports: [FormsModule],
  templateUrl: './add-contact.html',
  styleUrl: './add-contact.css',
})
export class AddContact {
  contact: Contact = {
    id: 0,
    name: '',
    email: '',
    phone: ''
  };

  constructor(
    private service: ContactService,
    private router: Router
  ) {}

  add() {
    this.service.addContact(this.contact);
    this.router.navigate(['/contacts']);
  }
}
