import { Injectable } from '@angular/core';
import { Product } from './models/product.model';

@Injectable({
  providedIn: 'root',
})
export class ProductService {

  private products: Product[] = [
    { id: 1, name: 'Laptop', price: 50000 },
    { id: 2, name: 'Mobile', price: 20000 },
    { id: 3, name: 'Tablet', price: 30000 }
  ];

  getProducts(): Product[] {
    return this.products;
  }

  getProductById(id: number): Product | undefined {
    return this.products.find(p => p.id === id);
  }
}