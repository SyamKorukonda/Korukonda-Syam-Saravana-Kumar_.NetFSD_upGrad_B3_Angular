import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { ProductService } from '../services/product.service';
import { CartService } from '../services/cart.service';
import { OrderService } from '../services/order.service';
import { AuthService } from '../services/auth.service';
import { Product } from '../models/product.model';

@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './product-details.component.html',
  styleUrls: ['./product-details.component.css']
})
export class ProductDetailsComponent implements OnInit {

  // Product
  product: Product | null = null;

  // UI states
  loading = true;
  adding = false;
  buying = false;

  // Quantity
  qty = 1;

  // Messages
  toast = '';
  error = '';

  // Auth
  isLoggedIn = false;

  // Review text
  reviewText = '';

  // Average rating
  avgRating = 4.2;
  reviewCount = 120;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private productService: ProductService,
    private cartService: CartService,
    private orderService: OrderService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {

    // Track login
    this.authService.isLoggedIn.subscribe(v => this.isLoggedIn = v);

    const id = Number(this.route.snapshot.paramMap.get('id'));

    if (!id || id <= 0) {
      this.loading = false;
      this.error = 'Invalid product';
      return;
    }

    // Load product
    this.productService.getById(id).subscribe({
      next: p => {
        this.product = p;
        this.loading = false;
      },
      error: () => {
        this.loading = false;
        this.error = 'Product not found';
      }
    });
  }

  // Quantity validation
  clampQty(): void {
    if (!this.product) return;

    if (!this.qty || this.qty < 1) this.qty = 1;

    if (this.qty > this.product.stock) {
      this.qty = this.product.stock;
    }
  }

  decreaseQty(): void {
    if (this.qty > 1) this.qty--;
  }

  increaseQty(): void {
    if (this.product && this.qty < this.product.stock) {
      this.qty++;
    }
  }

  // Add to cart
  addToCart(): void {

    if (!this.product) return;

    this.clampQty();
    this.adding = true;
    this.error = '';

    this.cartService.addItem({
      productId: this.product.productId,
      quantity: this.qty
    }).subscribe({
      next: () => {

        this.adding = false;

        this.toast = 'Added to cart!';
        setTimeout(() => this.toast = '', 3000);

        this.cartService.toggle();
      },

      error: err => {

        this.adding = false;

        this.error = err.error?.message ?? 'Failed to add.';
      }
    });
  }

  // Buy now
  buyNow(): void {

    if (!this.product) return;

    this.clampQty();
    this.buying = true;
    this.error = '';

    this.orderService.placeOrderDirect({
      cartItems: [{
        productId: this.product.productId,
        quantity: this.qty
      }]
    }).subscribe({
      next: () => {

        this.buying = false;

        this.toast = 'Order placed successfully!';
        setTimeout(() => this.toast = '', 2000);

        this.router.navigate(['/my-orders']);
      },

      error: err => {

        this.buying = false;

        this.error = err.error?.message ?? 'Failed to place order.';
      }
    });
  }

  // Review submit
  addReview(): void {

    if (!this.reviewText.trim()) return;

    console.log('Review:', this.reviewText);

    this.toast = 'Review submitted!';
    setTimeout(() => this.toast = '', 2000);

    this.reviewText = '';
  }

  // Image fallback
  onImgErr(e: Event): void {
    (e.target as HTMLImageElement).src =
      'assets/images/no-image.png';
  }
}