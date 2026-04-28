import { Component } from '@angular/core';
import { ContactService } from '../contact-service';
import { Contact } from '../models/contact.model';

@Component({
  selector: 'app-contact-detail',
  imports: [],
  templateUrl: './contact-detail.html',
  styleUrl: './contact-detail.css',
})
export class ContactDetail {
  contact?: Contact;

  constructor(private service: ContactService) {
    this.contact = this.service.getContactById(3);
  }
}
