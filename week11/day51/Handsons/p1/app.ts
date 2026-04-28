import { Component, signal } from '@angular/core';
import { Hello } from "./hello/hello";
import { RouterOutlet } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { Demo } from './demo/demo';
import { Contact } from './contact/contact';
import { ProductList } from './product-list/product-list';
import { ProductInfo } from './product-info/product-info';
import { ContactList } from './contact-list/contact-list';
import { ContactDetail } from './contact-detail/contact-detail';

@Component({
  selector: 'app-root',
   standalone: true,
  imports: [ContactList,ContactDetail],
  templateUrl: './app.html',
  styleUrl: './app.css'
})

export class App
{
  // members of the class
  public uname:string = "Scott";
}