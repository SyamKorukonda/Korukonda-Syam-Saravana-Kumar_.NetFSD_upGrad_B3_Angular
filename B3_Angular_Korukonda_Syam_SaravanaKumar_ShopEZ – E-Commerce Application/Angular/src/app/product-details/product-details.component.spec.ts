/// <reference types="jasmine" />

import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ActivatedRoute, Router } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { of, throwError } from 'rxjs';

import { ProductDetailsComponent } from './product-details.component';
import { ProductService } from '../services/product.service';
import { CartService } from '../services/cart.service';
import { OrderService } from '../services/order.service';
import { AuthService } from '../services/auth.service';


// Mock product data
// Used instead of backend API response
const mockProduct = {

  productId: 1,

  name: 'Laptop',

  description: 'Gaming laptop',

  price: 75000,

  stock: 10,

  imageUrl: null,

  category: 'Electronics'
};


// Mock ProductService
// Used to avoid real backend calls
function makeProductService() {

  return jasmine.createSpyObj(
    'ProductService',

    ['getById']
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


// Mock OrderService
// Used to avoid real order API calls
function makeOrderService() {

  return jasmine.createSpyObj(
    'OrderService',

    ['placeOrderDirect']
  );
}


// Mock AuthService
// Used to avoid real authentication logic
function makeAuthService() {

  return jasmine.createSpyObj(
    'AuthService',

    [],

    {

      // Fake login status
      isLoggedIn: of(true)
    }
  );
}


// describe()
// Used to group related test cases for one component
describe('ProductDetailsComponent', () => {

  // let
  // Used to declare variables for testing
  let component: ProductDetailsComponent;

  // ComponentFixture
  // Used to access component HTML and TypeScript
  let fixture: ComponentFixture<ProductDetailsComponent>;

  // jasmine.SpyObj
  // Used to create fake service methods
  let productSpy: jasmine.SpyObj<ProductService>;

  let cartSpy: jasmine.SpyObj<CartService>;

  let orderSpy: jasmine.SpyObj<OrderService>;

  let authSpy: jasmine.SpyObj<AuthService>;

  let router: Router;


  // beforeEach()
  // Runs before every test case
  // Used for setup and initialization
  beforeEach(async () => {

    // Create fake services
    productSpy = makeProductService();

    cartSpy = makeCartService();

    orderSpy = makeOrderService();

    authSpy = makeAuthService();

    // and.returnValue()
    // Used to return fake observable response
    productSpy.getById.and.returnValue(

      of(mockProduct as any)
    );

    // TestBed
    // Used to configure Angular testing module
    await TestBed.configureTestingModule({

      // imports
      // Used to import required Angular modules/components
      imports: [
        ProductDetailsComponent,
        RouterTestingModule
      ],

      // providers
      // Used to replace real services with fake services
      providers: [

        {
          provide: ActivatedRoute,

          // useValue
          // Used to provide fake route data
          useValue: {
            snapshot: {
              paramMap: {

                // get()
                // Used to get route parameter
                get: () => '1'
              }
            }
          }
        },

        {
          provide: ProductService,
          useValue: productSpy
        },

        {
          provide: CartService,
          useValue: cartSpy
        },

        {
          provide: OrderService,
          useValue: orderSpy
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
      TestBed.createComponent(ProductDetailsComponent);

    // componentInstance
    // Used to access component TypeScript
    component = fixture.componentInstance;

    // inject()
    // Used to get service instance from TestBed
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
  // Checks whether product loads correctly
  it('should load product on init', () => {

    // toHaveBeenCalledWith()
    // Checks whether method is called with correct value
    expect(productSpy.getById)
      .toHaveBeenCalledWith(1);

    // toBe()
    // Used to compare exact value
    expect(component.product?.name)
      .toBe('Laptop');
  });


  // Quantity validation test
  it('should clamp quantity correctly', () => {

    component.product = mockProduct as any;

    component.qty = 20;

    // clampQty()
    // Used to validate quantity range
    component.clampQty();

    expect(component.qty)
      .toBe(10);
  });


  // increaseQty() test
  it('should increase quantity', () => {

    component.product = mockProduct as any;

    component.qty = 1;

    // increaseQty()
    // Used to increase quantity
    component.increaseQty();

    expect(component.qty)
      .toBe(2);
  });


  // decreaseQty() test
  it('should decrease quantity', () => {

    component.qty = 3;

    // decreaseQty()
    // Used to decrease quantity
    component.decreaseQty();

    expect(component.qty)
      .toBe(2);
  });


  // addToCart() test
  it('should add product to cart', () => {

    cartSpy.addItem.and.returnValue(
      of({} as any)
    );

    component.product = mockProduct as any;

    component.qty = 2;

    // addToCart()
    // Used to add product into cart
    component.addToCart();

    expect(cartSpy.addItem)
      .toHaveBeenCalledWith({

        productId: 1,

        quantity: 2
      });

    expect(cartSpy.toggle)
      .toHaveBeenCalled();
  });


  // buyNow() test
  it('should place order successfully', () => {

    // spyOn()
    // Used to track method calls
    spyOn(router, 'navigate');

    orderSpy.placeOrderDirect
      .and.returnValue(of({} as any));

    component.product = mockProduct as any;

    // buyNow()
    // Used to place direct order
    component.buyNow();

    expect(orderSpy.placeOrderDirect)
      .toHaveBeenCalled();

    expect(component.toast)
      .toBe('Order placed successfully!');
  });


  // setRating() test
  it('should set rating correctly', () => {

    // setRating()
    // Used to set product rating
    component.setRating(5);

    expect(component.rating)
      .toBe(5);
  });


  // submitRating() test
  it('should submit rating successfully', () => {

    component.rating = 4;

    // submitRating()
    // Used to submit rating
    component.submitRating();

    expect(component.toast)
      .toBe('Rating submitted!');

    expect(component.rating)
      .toBe(0);
  });


  // addReview() test
  it('should submit review successfully', () => {

    component.reviewText =
      'Great product';

    // addReview()
    // Used to submit product review
    component.addReview();

    expect(component.toast)
      .toBe('Review submitted!');

    expect(component.reviewText)
      .toBe('');
  });


  // onImgErr() test
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

});