import { Component, signal } from '@angular/core';
import { Hello } from "./hello/hello";
import { RouterOutlet } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { Demo } from './demo/demo';
import { Contact } from './contact/contact';

@Component({
  selector: 'app-root',
  imports: [Contact],
  templateUrl: './app.html',
  styleUrl: './app.css'
})

export class App
{
  // members of the class
  public uname:string = "Scott";
}