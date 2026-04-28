import { Component } from '@angular/core';
import { CommonModule, TitleCasePipe } from '@angular/common';
import { ProductService } from '../product-service';
import { Product } from '../models/product.model';


@Component({
  selector: 'app-product-list',
  standalone:true,
  imports: [CommonModule,TitleCasePipe],
  templateUrl: './product-list.html',
  styleUrl: './product-list.css',
})
export class ProductList {
  public productsArray: Product[] = [];

  constructor(private productService: ProductService) {
    this.productsArray = this.productService.getProducts();
  }
}
