import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, map, tap, catchError, throwError } from 'rxjs';
import { ApiResponse } from '../models/api-response.model';
import { OrderResponse, PlaceOrderRequest } from '../models/order.model';
import { environment } from '../../environments/environments';

@Injectable({ providedIn: 'root' })
export class OrderService {

  // BehaviorSubject — holds current user's orders

  private orders$ = new BehaviorSubject<OrderResponse[]>([]);

  // BehaviorSubject — holds all orders for Admin

  private allOrders$ = new BehaviorSubject<OrderResponse[]>([]);

  orders    = this.orders$.asObservable();
  allOrders = this.allOrders$.asObservable();

  constructor(private http: HttpClient) {}

  //  POST /gateway/orders/from-cart 
  // OrderService reads JWT userId → fetches cart from CartService internally
  // → clears cart automatically after order is placed
  // No request body needed — all data comes from CartService via HTTP

  placeOrderFromCart(): Observable<OrderResponse> {
    return this.http.post<ApiResponse<OrderResponse>>(
      `${environment.gatewayUrl}/orders/from-cart`, {}
    ).pipe(
      map(res => res.data),
      tap(order => {
        // Prepend new order to top of BehaviorSubject list
        this.orders$.next([order, ...this.orders$.value]);
      }),
      catchError(err => throwError(() => err))
    );
  }

  //  POST /gateway/orders 
  // Direct order — sends cartItems: [{ productId, quantity }] in body
  // Used for "Buy Now" from product detail page

  placeOrderDirect(dto: PlaceOrderRequest): Observable<OrderResponse> {
    return this.http.post<ApiResponse<OrderResponse>>(
      `${environment.gatewayUrl}/orders`, dto
    ).pipe(
      map(res => res.data),
      tap(order => {
        this.orders$.next([order, ...this.orders$.value]);
      }),
      catchError(err => throwError(() => err))
    );
  }

  // GET /gateway/orders/my-orders — current user's orders

  getMyOrders(): Observable<OrderResponse[]> {
    return this.http.get<ApiResponse<OrderResponse[]>>(
      `${environment.gatewayUrl}/orders/my-orders`
    ).pipe(
      map(res => res.data),
      tap(orders => this.orders$.next(orders)),
      catchError(err => throwError(() => err))
    );
  }

  // GET /gateway/orders/all-orders — Admin only

  getAllOrders(): Observable<OrderResponse[]> {
    return this.http.get<ApiResponse<OrderResponse[]>>(
      `${environment.gatewayUrl}/orders/all-orders`
    ).pipe(
      map(res => res.data),
      tap(orders => this.allOrders$.next(orders)),
      catchError(err => throwError(() => err))
    );
  }

  // GET /gateway/orders/{id}

  getById(id: number): Observable<OrderResponse> {
    return this.http.get<ApiResponse<OrderResponse>>(
      `${environment.gatewayUrl}/orders/${id}`
    ).pipe(
      map(res => res.data),
      catchError(err => throwError(() => err))
    );
  }

  // PATCH /gateway/orders/{id}/cancel

  cancelOrder(id: number): Observable<ApiResponse<string>> {
    return this.http.patch<ApiResponse<string>>(
      `${environment.gatewayUrl}/orders/${id}/cancel`, {}
    ).pipe(
      tap(() => {
        const cancel = (list: OrderResponse[]) =>
          list.map(o => o.orderId === id ? { ...o, isCancelled: true } : o);
        this.orders$.next(cancel(this.orders$.value));
        this.allOrders$.next(cancel(this.allOrders$.value));
      }),
      catchError(err => throwError(() => err))
    );
  }
}
