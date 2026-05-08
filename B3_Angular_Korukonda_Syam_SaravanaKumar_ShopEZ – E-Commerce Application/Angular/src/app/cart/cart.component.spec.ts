/// <reference types="jasmine" />  //VS Code does not automatically load Jasmine types
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { of } from 'rxjs';
import { CartComponent } from './cart.component';
import { CartService } from '../services/cart.service';


// Mock cart items
// Used instead of real backend data
const mockCartItems = [
  {
    cartItemId: 1,
    userId: 1,
    productId: 1,
    productName: 'Laptop',
    price: 75000,
    quantity: 1,
    stock: 10,
    imageUrl: null,
    createdAt: '2024-01-01',
    subtotal: 75000
  }
];


// Mock CartService
// Used to avoid calling real APIs
function makeCartService(
  isOpen: boolean,
  items: any[],
  count: number,
  total: number
) {

  return jasmine.createSpyObj(
    'CartService',

    [
      'toggle',
      'close',
      'getCart',
      'addItem',
      'updateItem',
      'removeItem',
      'clearCart'
    ],

    {

      // Fake cart open state
      isCartOpen: of(isOpen),

      // Fake cart items
      cartItems: of(items),

      // Fake cart count
      cartCount: of(count),

      // Fake total amount
      totalPrice: of(total)
    }
  );
}


describe('CartComponent', () => {

  let component: CartComponent;

  // Used to access component HTML and TS
  let fixture: ComponentFixture<CartComponent>;

  let cartSpy: jasmine.SpyObj<CartService>;


  // Runs before every test case
  // Creates component and fake service
  beforeEach(async () => {

    // Create fake cart service
    cartSpy = makeCartService(false, [], 0, 0);

    // Fake API response
    cartSpy.getCart.and.returnValue(of({} as any));

    await TestBed.configureTestingModule({

      imports: [
        CartComponent,
        RouterTestingModule
      ],

      // Replace real service with fake service
      providers: [
        {
          provide: CartService,
          useValue: cartSpy
        }
      ]

    }).compileComponents();


    // Create component
    fixture = TestBed.createComponent(CartComponent);

    // Access component class
    component = fixture.componentInstance;

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
  // Checks whether observables are assigned correctly
  it('should initialize observables', () => {

    expect(component.isOpen$)
      .toBeTruthy();

    expect(component.cartItems$)
      .toBeTruthy();

    expect(component.cartCount$)
      .toBeTruthy();

    expect(component.totalPrice$)
      .toBeTruthy();
  });


  // getCart test
  // Checks whether getCart() is called on component load
  it('should call getCart on init', () => {

    expect(cartSpy.getCart)
      .toHaveBeenCalled();
  });


  // close() test
  // Checks whether close() calls cart service
  it('should call close()', () => {

    component.close();

    expect(cartSpy.close)
      .toHaveBeenCalled();
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


  // remove() test
  // Checks whether removeItem() is called correctly
  it('should remove item from cart', () => {

    cartSpy.removeItem
      .and.returnValue(of({} as any));

    component.remove(1);

    expect(cartSpy.removeItem)
      .toHaveBeenCalledWith(1);
  });


  // increase() success test
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
  // Quantity should NOT increase beyond stock
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


  // decrease() success test
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


  // checkout() test
  // Cart should close during checkout
  it('should close cart during checkout', () => {

    component.checkout();

    expect(cartSpy.close)
      .toHaveBeenCalled();
  });


  // Empty cart UI test
  // Empty message should appear when cart has no items
  it('should show empty cart message', () => {

    const el = fixture.nativeElement;

    fixture.detectChanges();

    expect(el.textContent)
      .toContain('empty');
  });


  // Cart items UI test
  // Product name should appear when items exist
  it('should show product name when cart has items', (done) => {

    // Directly update observable
    component.cartItems$ = of(mockCartItems);

    fixture.detectChanges();

    component.cartItems$.subscribe(items => {

      expect(items[0].productName)
        .toBe('Laptop');

      done();
    });
  });


  // Image error test
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