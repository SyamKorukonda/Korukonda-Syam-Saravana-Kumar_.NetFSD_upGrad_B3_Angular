import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { PhonePipe } from '../phone-pipe';
import { StatusPipe } from '../status-pipe';
import { SearchPipe } from '../search-pipe';

@Component({
  selector: 'app-contact',
  standalone: true,
  imports: [CommonModule,FormsModule,PhonePipe,StatusPipe, SearchPipe],
  templateUrl: './contact.html',
  styleUrls: ['./contact.css'],
})
export class Contact {
  searchText = '';
  limit = 5;

  contacts = [
    { name: 'syam', email: 'SYAM@MAIL.COM', phone: '9876543210', status: true },
    { name: 'rahul', email: 'RAHUL@MAIL.COM', phone: '9123456780', status: false },
    { name: 'john', email: 'JOHN@MAIL.COM', phone: '9988776655', status: true },
    { name: 'alex', email: 'ALEX@MAIL.COM', phone: '9012345678', status: false },
    { name: 'maria', email: 'MARIA@MAIL.COM', phone: '9871234567', status: true },
    { name: 'david', email: 'DAVID@MAIL.COM', phone: '8765432109', status: false },
    { name: 'kiran', email: 'KIRAN@MAIL.COM', phone: '7654321098', status: true },
    { name: 'ram', email: 'RAM@MAIL.COM', phone: '6543210987', status: false },
    { name: 'sita', email: 'SITA@MAIL.COM', phone: '5432109876', status: true },
    { name: 'arjun', email: 'ARJUN@MAIL.COM', phone: '4321098765', status: true }
  ];

  changeStatus(contact:any)
  {
    contact.status=!contact.status;
  }

  showLimit(){
    this.limit=this.limit===5 ? this.contacts.length:5;
  }
}
