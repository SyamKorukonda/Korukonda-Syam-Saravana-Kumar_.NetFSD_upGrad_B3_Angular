/// <reference types="jasmine" />

import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Router } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { FormsModule } from '@angular/forms';
import { of } from 'rxjs';

import { ProductListComponent } from './product-list.component';
import { ProductService } from '../services/product.service';
import { CartService } from '../services/cart.service';
import { AuthService } from '../services/auth.service';


// Mock product data
// Used instead of backend API response
const mockProducts = [

  {
    productId: 1,
    name: 'Laptop',
    description: 'Gaming laptop',
    price: 75000,
    stock: 10,
    imageUrl: null,
    category: 'Electronics'
  },

  {
    productId: 2,
    name: 'T-Shirt',
    description: 'Cotton shirt',
    price: 500,
    stock: 50,
    imageUrl: null,
    category: 'Fashion'
  }
];


// Mock ProductService
// Used to avoid real backend calls
function makeProductService() {

  return jasmine.createSpyObj(
    'ProductService',

    [
      'getAll',
      'search',
      'setCategory',
      'setMaxPrice'
    ],

    {

      // Fake loading observable
      loading: of(false),

      // Fake filtered products observable
      filteredProducts$: of(mockProducts)
    }
  );
}


// Mock CartService
// Used to avoid real cart API calls
function makeCartService() {

  return jasmine.createSpyObj(
    'CartService',

    [
      'addItem',
      'toggle'
    ]
  );
}


// Mock AuthService
// Used to avoid real auth logic
function makeAuthService() {

  return jasmine.createSpyObj(
    'AuthService',

    [],

    {

      // Fake login status
      isLoggedIn: of(true),

      // Fake current role
      currentUser: of('Customer')
    }
  );
}


describe('ProductListComponent', () => {

  // describe()
  // Used to group related test cases

  let component: ProductListComponent;

  // ComponentFixture
  // Used to access component HTML and TS
  let fixture: ComponentFixture<ProductListComponent>;

  // jasmine.SpyObj
  // Used to create fake service methods
  let productSpy: jasmine.SpyObj<ProductService>;

  let cartSpy: jasmine.SpyObj<CartService>;

  let authSpy: jasmine.SpyObj<AuthService>;

  let router: Router;


  // beforeEach()
  // Runs before every test case
  beforeEach(async () => {

    // Create fake services
    productSpy = makeProductService();

    cartSpy = makeCartService();

    authSpy = makeAuthService();

    // and.returnValue()
    // Used to return fake observable response
    productSpy.getAll.and.returnValue(
      of(mockProducts)
    );

    await TestBed.configureTestingModule({

      // imports
      // Used to import Angular modules/components
      imports: [
        ProductListComponent,
        RouterTestingModule,
        FormsModule
      ],

      // providers
      // Used to replace real services with fake services
      providers: [

        {
          provide: ProductService,
          useValue: productSpy
        },

        {
          provide: CartService,
          useValue: cartSpy
        },

        {
          provide: AuthService,
          useValue: authSpy
        }
      ]

    }).compileComponents();


    // createComponent()
    // Used to create component instance
    fixture =
      TestBed.createComponent(ProductListComponent);

    // componentInstance
    // Used to access component TypeScript
    component = fixture.componentInstance;

    // inject()
    // Used to get Router instance
    router = TestBed.inject(Router);

    // detectChanges()
    // Used to trigger Angular lifecycle methods
    fixture.detectChanges();
  });


  // it()
  // Used to write individual test case
  it('should create component', () => {

    // expect()
    // Used to verify expected output
    expect(component)
      .toBeTruthy();
  });


  // ngOnInit() test
  it('should call getAll on init', () => {

    // toHaveBeenCalled()
    // Checks whether method is called
    expect(productSpy.getAll)
      .toHaveBeenCalled();
  });


  // Product loading test
  it('should load products correctly', () => {

    // toBe()
    // Used to compare exact value
    expect(component.displayedProducts.length)
      .toBe(2);
  });


  // Login tracking test
  it('should track login state', () => {

    expect(component.isLoggedIn)
      .toBeTrue();
  });


  // Admin role test
  it('should detect admin role correctly', () => {

    expect(component.isAdmin)
      .toBeFalse();
  });


  // Search test
  it('should call search with valid term', () => {

    // onSearch()
    // Used to filter products
    component.onSearch('Laptop');

    // toHaveBeenCalledWith()
    // Checks whether method is called with correct value
    expect(productSpy.search)
      .toHaveBeenCalledWith('Laptop');
  });


  // Small search validation test
  it('should clear search for small search term', () => {

    component.onSearch('a');

    expect(productSpy.search)
      .toHaveBeenCalledWith('');
  });


  // Category filter test
  it('should filter products by category', () => {

    // onCategory()
    // Used to filter category
    component.onCategory('Electronics');

    expect(productSpy.setCategory)
      .toHaveBeenCalledWith('Electronics');
  });


  // Price filter test
  it('should filter products by max price', () => {

    // onMaxPrice()
    // Used to filter max price
    component.onMaxPrice(5000);

    expect(productSpy.setMaxPrice)
      .toHaveBeenCalledWith(5000);
  });


  // Clear filter test
  it('should clear all filters', () => {

    // clearFilters()
    // Used to reset filters
    component.clearFilters();

    expect(component.searchTerm)
      .toBe('');

    expect(component.selectedCat)
      .toBe('');

    expect(component.maxPrice)
      .toBeNull();
  });


  // Add to cart test
  it('should add product to cart', () => {

    cartSpy.addItem.and.returnValue(
      of({} as any)
    );

    // addToCart()
    // Used to add product into cart
    component.addToCart(
      mockProducts[0] as any
    );

    expect(cartSpy.addItem)
      .toHaveBeenCalledWith({

        productId: 1,

        quantity: 1
      });

    expect(cartSpy.toggle)
      .toHaveBeenCalled();
  });


  // Duplicate add validation test
  it('should not add duplicate cart request', () => {

    component.addingId = 1;

    component.addToCart(
      mockProducts[0] as any
    );

    expect(cartSpy.addItem)
      .not.toHaveBeenCalled();
  });


  // Buy now test
  it('should navigate to product details page', () => {

    // spyOn()
    // Used to track method calls
    spyOn(router, 'navigate');

    // buyNow()
    // Used to navigate product page
    component.buyNow(
      mockProducts[0] as any
    );

    expect(router.navigate)
      .toHaveBeenCalledWith([
        '/products',
        1
      ]);
  });


  // Image fallback test
  it('should show fallback image on image error', () => {

    const img = document.createElement('img');

    const event = {
      target: img
    } as any;

    // onImgErr()
    // Used to show fallback image
    component.onImgErr(event);

    expect(img.src)
      .toContain('no-image.png');
  });


  // Category list test
  it('should contain categories', () => {

    expect(component.categories.length)
      .toBeGreaterThan(0);
  });


  // ngOnDestroy() test
  it('should cleanup subscriptions on destroy', () => {

    // spyOn()
    // Used to track method calls
    const nextSpy =
      spyOn<any>(
        component['destroy$'],
        'next'
      );

    const completeSpy =
      spyOn<any>(
        component['destroy$'],
        'complete'
      );

    // ngOnDestroy()
    // Used to cleanup subscriptions
    component.ngOnDestroy();

    expect(nextSpy)
      .toHaveBeenCalled();

    expect(completeSpy)
      .toHaveBeenCalled();
  });

});