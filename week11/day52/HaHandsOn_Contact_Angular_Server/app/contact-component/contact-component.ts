import { Component,OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { ChangeDetectorRef } from '@angular/core';
import { ContactServices } from '../services/contact-services';
import { Contact } from '../models/contact';


@Component({
  selector: 'app-contact-component',
  imports: [FormsModule],
  templateUrl: './contact-component.html',
  styleUrl: './contact-component.css',
})
export class ContactComponent implements OnInit {
  public data:Contact[]=[];

  public newContact:Contact={
    contactId: 0,
    firstName: '',
    lastName: '',
    emailId: '',
    mobileNo: '',
    designation: '',
    companyId: 0,
    departmentId: 0,      
    departmentName: '' 
  };

  constructor(private contactService:ContactServices,private changeDetectorRef:ChangeDetectorRef){}

  ngOnInit(): void {
      this.getContactsClick();
  }

  getContactsClick():void{
    this.contactService.getContacts().subscribe({next:(response)=>
      {
        console.log(response);
        this.data=response;
        this.changeDetectorRef.detectChanges();
      },
      error:(err)=>{
        console.error(err);
        alert("Failed to load Contacts");
      }
  });

  }

  addContactClick():void{
    this.contactService.addContact(this.newContact).subscribe({
          next: (res: any) => {
            alert(res.message);
            this.getContactsClick();
            this.resetForm();
          },
          error: (err) => {
            alert("Failed to add contact");
        }
    });
  }

  editContactClick(contactObj:Contact)
  {
      this.newContact={...contactObj};
  }

  updateContactClick(){
      this.contactService.updateContact(this.newContact.contactId, this.newContact).subscribe({
          next: (res: any) => {
              alert(res.message);
              this.getContactsClick();
              this.resetForm();
            },
          error: (err) => {
              alert("Update failed");
            }
      });
  }

  deleteContactClick(id: number): void {
    this.contactService.deleteContact(id).subscribe({
      next: (res: any) => {
        console.log(res);
        alert(res.message);
        this.getContactsClick();
      },
      error: (err: any) => {
        alert("You are not allowed to perform this operation");
        this.changeDetectorRef.detectChanges();
      }
    });
  }

   resetForm() {
    this.newContact = {
      contactId: 0,
      firstName: '',
      lastName: '',
      emailId: '',
      mobileNo: '',
      designation: '',
      companyId: 0,
      departmentId: 0,
      departmentName: ''
   };
   
  }
}
