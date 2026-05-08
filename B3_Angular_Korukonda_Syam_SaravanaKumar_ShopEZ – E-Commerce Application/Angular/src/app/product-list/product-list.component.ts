import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { Subject, takeUntil, Observable } from 'rxjs';

import { ProductService } from '../services/product.service';
import { CartService } from '../services/cart.service';
import { AuthService } from '../services/auth.service';
import { Product } from '../models/product.model';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit, OnDestroy {


   categories = [
    { name: 'Electronics', img: 'assets/categories/electronics.png' },
    { name: 'Fashion', img: 'assets/categories/fashion.png' },
    { name: 'Mobiles', img: 'assets/categories/mobiles.png' },
    { name: 'Furniture', img: 'assets/categories/furniture.png' },
    { name: 'Groceries', img: 'assets/categories/groceries.png' },
    { name: 'Fruits', img: 'assets/categories/fruits.png' },
    { name: 'Camera', img: 'assets/categories/camera.png' },
    { name: 'Two-wheelers', img: 'assets/categories/bike.png' },
    { name: 'Shoes', img: 'assets/categories/shoes.png' },
    { name: 'Bags', img: 'assets/categories/bags.png' },
    { name: 'Appliances', img: 'assets/categories/appliances.png' },
    { name: 'Accessories', img: 'assets/categories/accessories.png' },
    { name: 'Jewellery', img: 'assets/categories/jewellery.png' },
    { name: 'Toys', img: 'assets/categories/toys.png' }

  ];


  // Loading state
  loading$!: Observable<boolean>;

  // Auth
  isLoggedIn = false;
  isAdmin = false;

  // Filters
  searchTerm = '';
  selectedCat = '';
  maxPrice: number | null = null;

  // Products
  displayedProducts: Product[] = [];

  // UI
  addingId: number | null = null;
  toast = '';

  // Destroy
  private destroy$ = new Subject<void>();

  constructor(
    private productService: ProductService,
    private cartService: CartService,
    private authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {

    // Loading observable
    this.loading$ = this.productService.loading;

    // Auth tracking
    this.authService.isLoggedIn
      .pipe(takeUntil(this.destroy$))
      .subscribe(v => this.isLoggedIn = v);

    this.authService.currentUser
      .pipe(takeUntil(this.destroy$))
      .subscribe(role => this.isAdmin = role === 'Admin');

    // Load products from API
    this.productService.getAll().subscribe();

    // Subscribe to filtered products stream (MAIN SOURCE)
    this.productService.filteredProducts$
      .pipe(takeUntil(this.destroy$))
      .subscribe(products => {
        this.displayedProducts = products;
      });
  }

  //  SEARCH
  onSearch(term: string) {

    this.searchTerm = term;

    if (!term || term.trim().length < 2) {
      this.productService.search('');
      return;
    }

    this.productService.search(term.trim());
  }

  //  CATEGORY FILTER
  onCategory(cat: string) {
    this.selectedCat = cat;
    this.productService.setCategory(cat);
  }

  //  PRICE FILTER
  onMaxPrice(max: number | null) {
    this.maxPrice = max;
    this.productService.setMaxPrice(max);   // uses service stream
  }

  //  CLEAR FILTERS
  clearFilters() {
    this.searchTerm = '';
    this.selectedCat = '';
    this.maxPrice = null;

    this.productService.search('');
    this.productService.setCategory('');
    this.productService.setMaxPrice(null);
  }

  //  ADD TO CART
  addToCart(product: Product) {

    if (!product || !product.productId) return;

    if (this.addingId === product.productId) return;

    this.addingId = product.productId;

    this.cartService.addItem({
      productId: product.productId,
      quantity: 1
    }).subscribe({
      next: () => {
        this.addingId = null;

        this.toast = `${product.name} added to cart!`;
        setTimeout(() => this.toast = '', 3000);

        this.cartService.toggle();
      },
      error: () => {
        this.addingId = null;
      }
    });
  }

  //  BUY NOW
  buyNow(product: Product) {
    this.router.navigate(['/products', product.productId]);
  }

  //  IMAGE FALLBACK
  onImgErr(e: Event) {
    (e.target as HTMLImageElement).src = 'assets/images/no-image.png';
  }

  //  CLEANUP
  ngOnDestroy(): void {
    this.destroy$.next();
    this.destroy$.complete();
  }
}