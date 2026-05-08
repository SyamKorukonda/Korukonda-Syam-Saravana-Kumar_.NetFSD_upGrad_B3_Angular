import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { Observable, take } from 'rxjs';
import { CartService } from '../services/cart.service';
import { OrderService } from '../services/order.service';
import { CartItemResponse } from '../models/cart-item.model';

@Component({
  selector: 'app-checkout',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './checkout.component.html',
  styleUrls: ['./checkout.component.css']
})
export class CheckoutComponent implements OnInit {

  // Reactive data
  
  cartItems$!: Observable<CartItemResponse[]>;
  totalPrice$!: Observable<number>;

  //  UI states

  loading = true;
  placing = false;
  error = '';
  success = '';

  constructor(
    private cartService: CartService,
    private orderService: OrderService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.cartItems$ = this.cartService.cartItems;
    this.totalPrice$ = this.cartService.totalPrice;

    this.cartService.getCart().subscribe(() => {
      this.loading = false;
    });
  }

  //  Increase quantity

  increase(item: CartItemResponse) {
    if (!item || item.quantity >= item.stock) return;

    this.cartService.updateItem(item.cartItemId, {
      quantity: item.quantity + 1
    }).subscribe();
  }

  //  Decrease quantity

  decrease(item: CartItemResponse) {
    if (!item || item.quantity <= 1) return;

    this.cartService.updateItem(item.cartItemId, {
      quantity: item.quantity - 1
    }).subscribe();
  }

  //  Remove item

  remove(id: number) {
    if (!id) return;
    this.cartService.removeItem(id).subscribe();
  }

  //  Clear cart

  clear() {
    this.cartService.clearCart().subscribe();
  }

  // Place Order (FINAL FIXED VERSION)

  placeOrder() {

    this.error = '';

    //  Prevent empty cart

    this.cartItems$.pipe(take(1)).subscribe(items => {

      if (!items || items.length === 0) {
        this.error = 'Cart is empty';
        return;
      }

      this.placing = true;

      this.orderService.placeOrderFromCart().subscribe({
        next: () => {
          this.placing = false;

          this.success = 'Order placed successfully!';

          // Sync UI
          this.cartService.clearCart().subscribe();

          setTimeout(() => {
            this.router.navigate(['/my-orders']);
          }, 1500);
        },
        error: err => {
          this.placing = false;
          this.error = err.error?.message ?? 'Failed to place order';
        }
      });

    });
  }

  //  Image fallback

  onImgErr(e: Event) {
    (e.target as HTMLImageElement).src = 'assets/images/no-image.png';
  }
}