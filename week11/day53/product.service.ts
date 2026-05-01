// product.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Product } from '../models/product';

@Injectable({ providedIn: 'root' })
export class ProductService {

  private apiUrl = 'https://dummyjson.com/products/';

  constructor(private http: HttpClient) {}

  searchProducts(term: string): Observable<Product[]> {
   // let url = `${this.apiUrl}search?q=${term}`;
   // console.log(url);
    return this.http.get<Product[]>(`${this.apiUrl}search?q=${term}`);
  }
}