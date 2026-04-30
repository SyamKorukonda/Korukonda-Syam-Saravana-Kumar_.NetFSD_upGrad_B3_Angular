import { Component, signal } from '@angular/core';
import { Home } from './home/home';
import { RouterLink, RouterOutlet } from '@angular/router';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet,RouterLink],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
 
  constructor(private router: Router) {}

  logout(): void {
    sessionStorage.removeItem("AUTH_TOKEN");
    this.router.navigate(['/login']);
  }
}
