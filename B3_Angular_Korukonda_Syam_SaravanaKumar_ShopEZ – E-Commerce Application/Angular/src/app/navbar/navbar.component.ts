import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';
import { CartService } from '../services/cart.service';
import { ThemeService } from '../services/theme.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  isLoggedIn$!: Observable<boolean>;
  role$!:       Observable<string>;
  cartCount$!:  Observable<number>;
  isDark$!:     Observable<boolean>;

  constructor(
    private auth: AuthService,
    private cart: CartService,
    private theme: ThemeService,
    private router: Router   // added router
  ) {}

  ngOnInit(): void {
    this.isLoggedIn$ = this.auth.isLoggedIn;
    this.role$       = this.auth.currentUser;
    this.cartCount$  = this.cart.cartCount;
    this.isDark$     = this.theme.isDarkMode;
  }

  toggleCart()  { this.cart.toggle(); }
  toggleTheme() { this.theme.toggle(); }

  // FIXED LOGOUT
  logout() {

    // clear auth
    this.auth.logout();

    // navigate and block back
    this.router.navigate(['/login'], { replaceUrl: true });
  }
}