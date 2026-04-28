import { Component } from '@angular/core';
import { ProductService } from '../product-service';
import { Product } from '../models/product.model';
@Component({
  selector: 'app-product-info',
  standalone:true,
  imports: [],
  templateUrl: './product-info.html',
  styleUrl: './product-info.css',
})
export class ProductInfo {
   public productObj: Product | undefined;

  constructor(private productService: ProductService) {
    this.productObj = this.productService.getProductById(1);
  }
}
