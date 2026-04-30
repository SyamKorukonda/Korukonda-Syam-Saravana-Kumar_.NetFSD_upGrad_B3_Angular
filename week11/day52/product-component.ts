import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { ProductService } from '../services/product-service';

@Component({
  selector: 'app-product-component',
  imports: [],
  templateUrl: './product-component.html',
  styleUrl: './product-component.css',
})
export class ProductComponent {

  public data:Product[] = [];

  constructor(private productService:ProductService){}

  buttonClick():void
  {     
    this.productService.getProducts().subscribe((response) =>  
    {     
      this.data = response;
    });
  }

}
