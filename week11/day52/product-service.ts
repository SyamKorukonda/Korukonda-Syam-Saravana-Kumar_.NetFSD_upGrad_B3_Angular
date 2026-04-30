import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ProductService {

    readonly API_URL:string = "https://localhost:7045/api/Products";

    constructor(private httpClient:HttpClient){}

    getProducts() {
      return this.httpClient.get<Product[]>(this.API_URL);
    }
}
