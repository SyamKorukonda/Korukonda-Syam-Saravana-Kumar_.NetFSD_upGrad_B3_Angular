import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, map, tap, catchError, throwError } from 'rxjs';
import { ApiResponse } from '../models/api-response.model';
import { CartItemResponse, CartSummary, AddToCartRequest, UpdateCartRequest } from '../models/cart-item.model';
import { environment } from '../../environments/environments';

@Injectable({ providedIn: 'root' })
export class CartService {

  // BehaviorSubject — cartItems$ — holds all cart items for logged-in user

  private cartItems$ = new BehaviorSubject<CartItemResponse[]>([]);
  // BehaviorSubject — cartCount$ — total item count shown in Navbar badge

  private cartCount$ = new BehaviorSubject<number>(0);
  // BehaviorSubject — totalPrice$ — grand total for checkout

  private totalPrice$ = new BehaviorSubject<number>(0);
  // BehaviorSubject — isCartOpen$ — toggle sidebar open/close

  private isCartOpen$ = new BehaviorSubject<boolean>(false);

  // Expose as read-only Observables

  cartItems  = this.cartItems$.asObservable();
  cartCount  = this.cartCount$.asObservable();
  totalPrice = this.totalPrice$.asObservable();
  isCartOpen = this.isCartOpen$.asObservable();

  constructor(private http: HttpClient) {}

  // Toggle sidebar open/close — called from Navbar cart button

  toggle(): void { this.isCartOpen$.next(!this.isCartOpen$.value); }
  close():  void { this.isCartOpen$.next(false); }

  // GET /gateway/cart — Returns CartSummaryDto { items, totalPrice, totalItems }

  getCart(): Observable<CartSummary> {
    return this.http.get<ApiResponse<CartSummary>>(
      `${environment.gatewayUrl}/cart`
    ).pipe(
      map(res => res.data),
      tap(summary => this.syncState(summary)),
      catchError(err => throwError(() => err))
    );
  }

  // POST /gateway/cart — AddToCartDto { productId, quantity }
  // Returns CartItemResponseDto (single item added)

  addItem(dto: AddToCartRequest): Observable<CartItemResponse> {
    return this.http.post<ApiResponse<CartItemResponse>>(
      `${environment.gatewayUrl}/cart`, dto
    ).pipe(
      map(res => res.data),
      tap(item => {
        // Check if item already in local list — update or append

        const existing = this.cartItems$.value.find(i => i.productId === item.productId);
        if (existing) {
          this.cartItems$.next(
            this.cartItems$.value.map(i => i.productId === item.productId ? item : i)
          );
        } else {
          this.cartItems$.next([...this.cartItems$.value, item]);
        }
        this.recalculate();
      }),
      catchError(err => throwError(() => err))
    );
  }

  // PUT /gateway/cart/{cartItemId} — UpdateCartDto { quantity }
  // Returns updated CartItemResponseDto

  updateItem(cartItemId: number, dto: UpdateCartRequest): Observable<CartItemResponse> {
    return this.http.put<ApiResponse<CartItemResponse>>(
      `${environment.gatewayUrl}/cart/${cartItemId}`, dto
    ).pipe(
      map(res => res.data),
      tap(updated => {
        this.cartItems$.next(
          this.cartItems$.value.map(i => i.cartItemId === cartItemId ? updated : i)
        );
        this.recalculate();
      }),
      catchError(err => throwError(() => err))
    );
  }

  // DELETE /gateway/cart/{cartItemId} — Remove single item

  removeItem(cartItemId: number): Observable<ApiResponse<string>> {
    return this.http.delete<ApiResponse<string>>(
      `${environment.gatewayUrl}/cart/${cartItemId}`
    ).pipe(
      tap(() => {
        this.cartItems$.next(
          this.cartItems$.value.filter(i => i.cartItemId !== cartItemId)
        );
        this.recalculate();
      }),
      catchError(err => throwError(() => err))
    );
  }

  // DELETE /gateway/cart/clear — Clear all items for user

  clearCart(): Observable<ApiResponse<string>> {
    return this.http.delete<ApiResponse<string>>(
      `${environment.gatewayUrl}/cart/clear`
    ).pipe(
      tap(() => this.resetState()),
      catchError(err => throwError(() => err))
    );
  }

  // Snapshot — used by checkout to build PlaceOrderRequest

  getItems(): CartItemResponse[] {
    return this.cartItems$.value;
  }

  getCount(): number {
    return this.cartCount$.value;
  }

  // Sync BehaviorSubjects from CartSummaryDto response

  private syncState(summary: CartSummary): void {
    this.cartItems$.next(summary.items);
    this.totalPrice$.next(summary.totalPrice);
    this.cartCount$.next(summary.totalItems);
  }

  // Recalculate after local add/update/remove

  private recalculate(): void {
    const items = this.cartItems$.value;
    const total = items.reduce((sum, i) => sum + i.subtotal, 0);
    const count = items.reduce((sum, i) => sum + i.quantity, 0);
    this.totalPrice$.next(total);
    this.cartCount$.next(count);
  }

  private resetState(): void {
    this.cartItems$.next([]);
    this.totalPrice$.next(0);
    this.cartCount$.next(0);
  }
}
