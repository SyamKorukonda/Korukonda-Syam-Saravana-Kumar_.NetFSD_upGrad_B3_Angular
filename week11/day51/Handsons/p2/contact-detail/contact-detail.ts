import { Component } from '@angular/core';
import { ContactService } from '../contact-service';
import { Contact } from '../models/contact.model';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-contact-detail',
  standalone:true,
  imports: [],
  templateUrl: './contact-detail.html',
  styleUrl: './contact-detail.css',
})
export class ContactDetail {
  contact?: Contact;

  constructor(private service: ContactService,private route:ActivatedRoute) {}

  ngOnInit() {
    const id = Number(this.route.snapshot.params['id']);
    this.contact = this.service.getContactById(id);
  }
}
