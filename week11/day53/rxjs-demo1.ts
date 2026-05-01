// product-search.component.ts
import { Component, OnInit } from '@angular/core';
import { FormControl, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { debounceTime, distinctUntilChanged, switchMap, catchError, map, finalize, take, filter } from 'rxjs/operators';
 
import { Observable, of } from 'rxjs';
import { CommonModule } from '@angular/common';
import { Product } from '../../models/product';
import { ProductService } from '../../services/product.service';

@Component({
  selector: 'app-rxjs-demo1',
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './rxjs-demo1.html',
  styleUrl: './rxjs-demo1.css',
})
export class RxjsDemo1  { 


 
  searchControl = new FormControl('');
  products$!: Observable<Product[]>;
  isLoading = false;

  constructor(private productService: ProductService) {}

  ngOnInit() {
    this.products$ = this.searchControl.valueChanges.pipe(
       debounceTime(400),                  // wait for typing pause
       distinctUntilChanged(),             // ignore duplicate values
      switchMap(term => {
          if (!term) 
          {            
            return of([]);         // empty input → no results
          }

        this.isLoading = true;

        return this.productService.searchProducts(term).pipe(
          map((res:any) => res.products),     // generate products array from the response object              
         // filter((res:any) => res.category == "beauty"),  
          map((res:any) => res.slice(0,5)),                     
          catchError(() => of([])),        // handle API error; send empty response to UI
          finalize(() => this.isLoading = false) // ALWAYS runs
        );
      })
    );
  }
}