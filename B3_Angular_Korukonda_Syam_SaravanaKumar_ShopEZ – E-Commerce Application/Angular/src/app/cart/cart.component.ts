import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { CartService } from '../services/cart.service';
import { CartItemResponse } from '../models/cart-item.model';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule,RouterModule],
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {

  isOpen$!:     Observable<boolean>;
  cartItems$!:  Observable<CartItemResponse[]>;
  cartCount$!:  Observable<number>;
  totalPrice$!: Observable<number>;

  constructor(
    private cartService: CartService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.isOpen$     = this.cartService.isCartOpen;
    this.cartItems$  = this.cartService.cartItems;
    this.cartCount$  = this.cartService.cartCount;
    this.totalPrice$ = this.cartService.totalPrice;
    this.cartService.getCart().subscribe({
      error: err => console.error(err)
    });
  }

  close(): void { this.cartService.close(); }

  increase(item: CartItemResponse): void {
  if (item.quantity >= item.stock) return;

  this.cartService.updateItem(item.cartItemId, {
    quantity: item.quantity + 1
  }).subscribe(() => this.cartService.getCart().subscribe());
}


  decrease(item: CartItemResponse): void {
  if (item.quantity <= 1) return;

  this.cartService.updateItem(item.cartItemId, {
    quantity: item.quantity - 1
  }).subscribe(() => this.cartService.getCart().subscribe());
}

  remove(cartItemId: number): void {
  this.cartService.removeItem(cartItemId)
    .subscribe(() => this.cartService.getCart().subscribe());
}


  clear(): void {
  this.cartService.clearCart()
    .subscribe(() => this.cartService.getCart().subscribe());
}

  checkout(): void {
    this.close();
    this.router.navigate(['/checkout']);
  }

  onImgErr(e: Event): void {
    (e.target as HTMLImageElement).src = 'assets/images/no-image.png';
  }
}