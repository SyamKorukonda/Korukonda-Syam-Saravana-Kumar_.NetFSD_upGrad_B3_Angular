import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { combineLatest } from 'rxjs';
import {
  BehaviorSubject, Observable,
  debounceTime, distinctUntilChanged, map,
  filter, switchMap, tap, catchError, throwError
} from 'rxjs';
import { ApiResponse } from '../models/api-response.model';
import { Product, ProductRequest } from '../models/product.model';
import { environment } from '../../environments/environments';

@Injectable({ providedIn: 'root' })
export class ProductService {

  // BehaviorSubject — holds full product list from API

  private products$ = new BehaviorSubject<Product[]>([]);

  // BehaviorSubject — holds live search input

  private searchTerm$ = new BehaviorSubject<string>('');

  // BehaviorSubject — holds selected category

  private category$ = new BehaviorSubject<string>('');

  // BehaviorSubject — holds selected maxPrice
  private maxPrice$ = new BehaviorSubject<number | null>(null);

  // BehaviorSubject — loading spinner state

  private loading$ = new BehaviorSubject<boolean>(false);

  products = this.products$.asObservable();
  loading = this.loading$.asObservable();

  //  Filtered stream 
  // debounceTime — wait 300ms after user stops typing before filtering
  // distinctUntilChanged — skip if search term is the same as before
  // map — transform term to lowercase
  // filter — filter products matching search + category

  filteredProducts$: Observable<Product[]> = combineLatest([
    this.searchTerm$,
    this.category$,
    this.maxPrice$,
    this.products$
  ]).pipe(
    map(([term, cat, max, products]) => {
      term = term.trim().toLowerCase();

      return products.filter(p => {

        const matchSearch =
          !term ||
          p.name.toLowerCase().includes(term) ||
          p.description.toLowerCase().includes(term);

        const matchCat =
          !cat ||
          (p.category ?? '').toLowerCase() === cat.toLowerCase();

        const matchPrice =
          !max || p.price <= max;

        return matchSearch && matchCat && matchPrice;
      });
    })
  );

  constructor(private http: HttpClient) { }

  // Update search term — triggers debounced filteredProducts$ stream

  search(term: string): void {
    this.searchTerm$.next(term);
  }

  // Update category filter

  setCategory(cat: string): void {
    this.category$.next(cat);
    this.searchTerm$.next(this.searchTerm$.value);
  }

  setMaxPrice(max: number | null) {
    this.maxPrice$.next(max);
  }



  // GET /gateway/products

  getAll(): Observable<Product[]> {
    this.loading$.next(true);
    return this.http.get<ApiResponse<Product[]>>(
      `${environment.gatewayUrl}/products`
    ).pipe(
      map(res => res.data),
      filter(list => Array.isArray(list)),
      tap(list => { this.products$.next(list); this.loading$.next(false); }),
      catchError(err => { this.loading$.next(false); return throwError(() => err); })
    );
  }

  // GET/gateway/products/category/{category}
  getByCategory(category: string): Observable<Product[]> {
    this.loading$.next(true);

    return this.http.get<ApiResponse<Product[]>>(
      `${environment.gatewayUrl}/products/category/${category}`
    ).pipe(
      map(res => res.data),   //  IMPORTANT
      tap(list => {
        this.products$.next(list);  // update stream
        this.loading$.next(false);
      }),
      catchError(err => {
        this.loading$.next(false);
        return throwError(() => err);
      })
    );
  }

  // GET /gateway/products/{id}

  getById(id: number): Observable<Product> {
    return this.http.get<ApiResponse<Product>>(
      `${environment.gatewayUrl}/products/${id}`
    ).pipe(
      map(res => res.data),
      catchError(err => throwError(() => err))
    );
  }

  // POST /gateway/products (Admin)

  create(dto: ProductRequest): Observable<Product> {
    return this.http.post<ApiResponse<Product>>(
      `${environment.gatewayUrl}/products`, dto
    ).pipe(
      map(res => res.data),
      tap(p => this.products$.next([...this.products$.value, p])),
      catchError(err => throwError(() => err))
    );
  }

  // PUT /gateway/products/{id} (Admin)

  update(id: number, dto: ProductRequest): Observable<ApiResponse<string>> {
    return this.http.put<ApiResponse<string>>(
      `${environment.gatewayUrl}/products/${id}`, dto
    ).pipe(
      tap(() => {
        this.products$.next(
          this.products$.value.map(p => p.productId === id ? { ...p, ...dto } : p)
        );
      }),
      catchError(err => throwError(() => err))
    );
  }

  // DELETE /gateway/products/{id} (Admin)

  delete(id: number): Observable<ApiResponse<string>> {
    return this.http.delete<ApiResponse<string>>(
      `${environment.gatewayUrl}/products/${id}`
    ).pipe(
      tap(() => this.products$.next(this.products$.value.filter(p => p.productId !== id))),
      catchError(err => throwError(() => err))
    );
  }
}
