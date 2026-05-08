import { TestBed } from '@angular/core/testing';
import {
  HttpClientTestingModule,
  HttpTestingController
} from '@angular/common/http/testing';
import { CartService } from '../services/cart.service';
import { environment } from '../../environments/environments';

//  Mock Data (Fake data used for testing instead of real API) 
const mockItems = [
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
  },
  {
    cartItemId: 2,
    userId: 1,
    productId: 2,
    productName: 'T-Shirt',
    price: 500,
    quantity: 2,
    stock: 50,
    imageUrl: null,
    createdAt: '2024-01-01',
    subtotal: 1000
  }
];

//  Mock API response structure 
const mockSummary = {
  items: mockItems,
  totalPrice: 76000,
  totalItems: 3
};

//  Helper function to simulate backend response 
const mockApiRes = (data: any) => ({
  success: true,
  message: 'Success',
  data
});

//  Test Suite starts here 
describe('CartService', () => {

  let service: CartService;
  let http: HttpTestingController;

  //  Setup before each test 
  beforeEach(() => {

    // Configure testing module with Http testing support
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [CartService]
    });

    // Inject service and http mock controller
    service = TestBed.inject(CartService);
    http = TestBed.inject(HttpTestingController);
  });

  //  Cleanup after each test 
  afterEach(() => {
    // Ensures no pending API calls
    http.verify();
  });

  //  Test: Service creation 
  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  //  Test: Initial BehaviorSubject values 
  it('should have empty cart initially', (done) => {

    // Subscribe to cartItems observable
    service.cartItems.subscribe(items => {

      // Expect empty array initially
      expect(items.length).toBe(0);

      done();
    });
  });

  it('should have count = 0 initially', (done) => {

    service.cartCount.subscribe(count => {

      // Initially no items → count = 0
      expect(count).toBe(0);

      done();
    });
  });

  //  Test: Toggle cart UI 
  it('should open cart when toggle is called', (done) => {

    // Call toggle method
    service.toggle();

    // Check if cart is opened
    service.isCartOpen.subscribe(open => {
      expect(open).toBeTrue();
      done();
    });
  });

  //  Test: Fetch cart from API 
  it('should fetch cart from API', () => {

    // Call service method
    service.getCart().subscribe(res => {

      // Validate response data
      expect(res.items.length).toBe(2);
      expect(res.totalPrice).toBe(76000);
    });

    // Expect HTTP GET request
    const req = http.expectOne(`${environment.gatewayUrl}/cart`);
    expect(req.request.method).toBe('GET');

    // Send mock response
    req.flush(mockApiRes(mockSummary));
  });

  //  Test: Add item to cart 
  it('should add item to cart', () => {

    const newItem = {
      cartItemId: 3,
      productId: 3,
      productName: 'iPhone',
      price: 90000,
      quantity: 1,
      subtotal: 90000
    };

    // Call addItem method
    service.addItem({ productId: 3, quantity: 1 }).subscribe(item => {

      // Validate returned item
      expect(item.productName).toBe('iPhone');
    });

    // Expect POST request
    const req = http.expectOne(`${environment.gatewayUrl}/cart`);
    expect(req.request.method).toBe('POST');

    // Send mock response
    req.flush(mockApiRes(newItem));
  });

  //  Test: Update cart item 
  it('should update item quantity', () => {

    service.updateItem(1, { quantity: 3 }).subscribe(item => {

      // Check updated quantity
      expect(item.quantity).toBe(3);
    });

    // Expect PUT request
    const req = http.expectOne(`${environment.gatewayUrl}/cart/1`);
    expect(req.request.method).toBe('PUT');

    // Send updated mock response
    req.flush(mockApiRes({ ...mockItems[0], quantity: 3 }));
  });

  //  Test: Remove item 
  it('should remove item from cart', () => {

    service.removeItem(1).subscribe(res => {

      // Expect success response
      expect(res.success).toBeTrue();
    });

    // Expect DELETE request
    const req = http.expectOne(`${environment.gatewayUrl}/cart/1`);
    expect(req.request.method).toBe('DELETE');

    req.flush(mockApiRes('Removed'));
  });

  //  Test: Clear cart 
  it('should clear cart', () => {

    service.clearCart().subscribe(res => {

      // Expect success response
      expect(res.success).toBeTrue();
    });

    // Expect DELETE request
    const req = http.expectOne(`${environment.gatewayUrl}/cart/clear`);
    expect(req.request.method).toBe('DELETE');

    req.flush(mockApiRes('Cleared'));
  });

});