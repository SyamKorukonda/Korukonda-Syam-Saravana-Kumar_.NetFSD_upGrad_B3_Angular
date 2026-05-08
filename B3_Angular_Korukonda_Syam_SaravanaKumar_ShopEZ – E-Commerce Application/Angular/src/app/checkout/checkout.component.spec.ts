/// <reference types="jasmine" />

import { ComponentFixture, TestBed } from '@angular/core/testing';
import { Router } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { of, throwError } from 'rxjs';
import { CheckoutComponent } from './checkout.component';
import { CartService } from '../services/cart.service';
import { OrderService } from '../services/order.service';


// Mock cart items
// Used instead of backend data
const mockCartItems = [
  {
    cartItemId: 1,
    userId: 1,
    productId: 1,
    productName: 'Laptop',
    quantity: 1,
    stock: 10,
    price: 75000,
    subtotal: 75000,
    imageUrl: null,
    createdAt: '2024-01-01'
  }
];


// Mock CartService
// Used to avoid real API calls
function makeCartService(items: any[], total: number) {

  return jasmine.createSpyObj(
    'CartService',

    [
      'getCart',
      'updateItem',
      'removeItem',
      'clearCart'
    ],

    {

      // Fake cart items observable
      cartItems: of(items),

      // Fake total price observable
      totalPrice: of(total)
    }
  );
}


// Mock OrderService
// Used to avoid real order API calls
function makeOrderService() {

  return jasmine.createSpyObj(
    'OrderService',
    ['placeOrderFromCart']
  );
}


describe('CheckoutComponent', () => {

  let component: CheckoutComponent;

  // Used to access component HTML and TS
  let fixture: ComponentFixture<CheckoutComponent>;

  let cartSpy: jasmine.SpyObj<CartService>;

  let orderSpy: jasmine.SpyObj<OrderService>;

  let router: Router;


  // Runs before every test case
  // Creates component and fake services
  beforeEach(async () => {

    // Create fake services
    cartSpy = makeCartService(
      mockCartItems,
      75000
    );

    orderSpy = makeOrderService();

    // Fake getCart response
    cartSpy.getCart.and.returnValue(
      of({} as any)
    );

    await TestBed.configureTestingModule({

      imports: [
        CheckoutComponent,
        RouterTestingModule
      ],

      // Replace real services with fake services
      providers: [

        {
          provide: CartService,
          useValue: cartSpy
        },

        {
          provide: OrderService,
          useValue: orderSpy
        }
      ]

    }).compileComponents();


    // Create component
    fixture = TestBed.createComponent(CheckoutComponent);

    // Access component class
    component = fixture.componentInstance;

    // Router instance
    router = TestBed.inject(Router);

    // Trigger lifecycle methods
    fixture.detectChanges();
  });


  // Component creation test
  // Checks whether component is created successfully
  it('should create component', () => {

    expect(component)
      .toBeTruthy();
  });


  // Observable initialization test
  // Checks whether observables are assigned properly
  it('should initialize observables', () => {

    expect(component.cartItems$)
      .toBeTruthy();

    expect(component.totalPrice$)
      .toBeTruthy();
  });


  // ngOnInit test
  // Checks whether getCart() is called
  it('should call getCart on init', () => {

    expect(cartSpy.getCart)
      .toHaveBeenCalled();
  });


  // Loading state test
  // Loading should become false after cart loads
  it('should stop loading after cart loads', () => {

    expect(component.loading)
      .toBeFalse();
  });


  // increase() test
  // Quantity should increase by 1
  it('should increase item quantity', () => {

    cartSpy.updateItem
      .and.returnValue(of({} as any));

    const item = mockCartItems[0];

    component.increase(item as any);

    expect(cartSpy.updateItem)
      .toHaveBeenCalledWith(
        1,
        { quantity: 2 }
      );
  });


  // increase() validation test
  // Quantity should NOT exceed stock
  it('should not increase quantity beyond stock', () => {

    const item = {

      ...mockCartItems[0],

      quantity: 10,

      stock: 10
    };

    component.increase(item as any);

    expect(cartSpy.updateItem)
      .not.toHaveBeenCalled();
  });


  // decrease() test
  // Quantity should decrease by 1
  it('should decrease item quantity', () => {

    cartSpy.updateItem
      .and.returnValue(of({} as any));

    const item = {

      ...mockCartItems[0],

      quantity: 3
    };

    component.decrease(item as any);

    expect(cartSpy.updateItem)
      .toHaveBeenCalledWith(
        1,
        { quantity: 2 }
      );
  });


  // decrease() validation test
  // Quantity should NOT go below 1
  it('should not decrease quantity below 1', () => {

    const item = {

      ...mockCartItems[0],

      quantity: 1
    };

    component.decrease(item as any);

    expect(cartSpy.updateItem)
      .not.toHaveBeenCalled();
  });


  // remove() test
  // Checks whether removeItem() is called
  it('should remove cart item', () => {

    cartSpy.removeItem
      .and.returnValue(of({} as any));

    component.remove(1);

    expect(cartSpy.removeItem)
      .toHaveBeenCalledWith(1);
  });


  // clear() test
  // Checks whether clearCart() is called
  it('should clear cart', () => {

    cartSpy.clearCart
      .and.returnValue(of({} as any));

    component.clear();

    expect(cartSpy.clearCart)
      .toHaveBeenCalled();
  });


  // Empty cart validation test
  // Order should not place when cart is empty
  it('should show error when cart is empty', () => {

    component.cartItems$ = of([]);

    component.placeOrder();

    expect(component.error)
      .toBe('Cart is empty');
  });


  // Successful order test
  // Checks whether order is placed successfully
  it('should place order successfully', () => {

    spyOn(router, 'navigate');

    orderSpy.placeOrderFromCart
      .and.returnValue(of({} as any));

    cartSpy.clearCart
      .and.returnValue(of({} as any));

    component.placeOrder();

    expect(orderSpy.placeOrderFromCart)
      .toHaveBeenCalled();

    expect(component.success)
      .toBe('Order placed successfully!');
  });


  // Loading state during order placement
  // placing should become false after success
  it('should stop placing after successful order', () => {

    orderSpy.placeOrderFromCart
      .and.returnValue(of({} as any));

    cartSpy.clearCart
      .and.returnValue(of({} as any));

    component.placeOrder();

    expect(component.placing)
      .toBeFalse();
  });


  // Failed order test
  // Shows backend error message
  it('should show error when order fails', () => {

    orderSpy.placeOrderFromCart
      .and.returnValue(

        throwError(() => ({
          error: {
            message: 'Order failed'
          }
        }))
      );

    component.placeOrder();

    expect(component.error)
      .toBe('Order failed');
  });


  // Failed loading test
  // placing should stop after failure
  it('should stop placing after failed order', () => {

    orderSpy.placeOrderFromCart
      .and.returnValue(

        throwError(() => ({
          error: {
            message: 'Error'
          }
        }))
      );

    component.placeOrder();

    expect(component.placing)
      .toBeFalse();
  });


  // Image fallback test
  // Default image should load when image fails
  it('should show default image on image error', () => {

    const img = document.createElement('img');

    const event = {
      target: img
    } as any;

    component.onImgErr(event);

    expect(img.src)
      .toContain('no-image.png');
  });

});