// Angular Testing Imports

// TestBed -> Creates Angular testing module
import { TestBed } from '@angular/core/testing';

// HttpClientTestingModule -> Mock HTTP requests
// HttpTestingController -> Control and verify HTTP calls
import {
  HttpClientTestingModule,
  HttpTestingController
} from '@angular/common/http/testing';

// Service under test
import { OrderService } from '../services/order.service';

// Environment base URL
import { environment } from '../../environments/environments';


// Mock Data
// Used instead of real backend responses

// Sample Order 1
const mockOrder1 = {
  orderId: 1,
  userId: 1,
  orderDate: '2024-01-01T10:00:00',
  totalAmount: 76000,
  isCancelled: false,

  // Order items
  items: [
    {
      productId: 1,
      productName: 'Laptop',
      quantity: 1,
      price: 75000,
      subtotal: 75000
    },
    {
      productId: 2,
      productName: 'T-Shirt',
      quantity: 2,
      price: 500,
      subtotal: 1000
    }
  ]
};

// Sample Order 2
const mockOrder2 = {
  orderId: 2,
  userId: 2,
  orderDate: '2024-01-02T12:00:00',
  totalAmount: 90000,
  isCancelled: false,
  items: [
    {
      productId: 3,
      productName: 'iPhone',
      quantity: 1,
      price: 90000,
      subtotal: 90000
    }
  ]
};


// Common Mock API Response
// Simulates backend API response structure

const mockApiRes = (data: any, msg = 'Success') => ({
  success: true,
  message: msg,
  data
});


// Test Suite

describe('OrderService', () => {

  // Service instance
  let service: OrderService;

  // Used to monitor and verify HTTP requests
  let http: HttpTestingController;


  // Runs before every test case
  // Creates testing module
  beforeEach(() => {

    TestBed.configureTestingModule({

      // Import mock HTTP module
      imports: [HttpClientTestingModule],

      // Register service
      providers: [OrderService]
    });

    // Inject service instance
    service = TestBed.inject(OrderService);

    // Inject HTTP testing controller
    http = TestBed.inject(HttpTestingController);
  });


  // Runs after every test case
  // Ensures no pending HTTP requests remain
  afterEach(() => {
    http.verify();
  });


  // Service creation test
  it('should be created', () => {

    // Verify service instance is created successfully
    expect(service).toBeTruthy();
  });


  // Initial BehaviorSubject state tests

  it('orders$ should be empty initially', (done) => {

    // Subscribe to orders observable
    service.orders.subscribe(list => {

      // Initially array should be empty
      expect(list.length).toBe(0);

      done();
    });
  });


  it('allOrders$ should be empty initially', (done) => {

    service.allOrders.subscribe(list => {

      // Initially no admin orders available
      expect(list.length).toBe(0);

      done();
    });
  });


  // placeOrderFromCart()
  // Place order using cart items

  it('should POST to /orders/from-cart with empty body', () => {

    service.placeOrderFromCart().subscribe(order => {

      // Validate response data
      expect(order.orderId).toBe(1);
      expect(order.totalAmount).toBe(76000);
      expect(order.isCancelled).toBeFalse();
    });

    // Expect API request
    const req = http.expectOne(
      `${environment.gatewayUrl}/orders/from-cart`
    );

    // Verify request method
    expect(req.request.method).toBe('POST');

    // Verify request body
    expect(req.request.body).toEqual({});

    // Send mock response
    req.flush(mockApiRes(mockOrder1, 'Order placed'));
  });


  // Verify BehaviorSubject updates after placing order

  it('should prepend new order to orders$', (done) => {

    service.placeOrderFromCart().subscribe(() => {

      service.orders.subscribe(list => {

        // New order should be added
        expect(list.length).toBe(1);

        // Verify inserted order
        expect(list[0].orderId).toBe(1);

        done();
      });
    });

    http.expectOne(
      `${environment.gatewayUrl}/orders/from-cart`
    ).flush(mockApiRes(mockOrder1));
  });


  // Error handling test

  it('should handle error when cart is empty', () => {

    service.placeOrderFromCart().subscribe({

      // Fail test if success occurs
      next: () => fail('Should have failed'),

      // Validate error status
      error: err => expect(err.status).toBe(400)
    });

    http.expectOne(
      `${environment.gatewayUrl}/orders/from-cart`
    ).flush(
      { message: 'Cart is empty' },
      { status: 400, statusText: 'Bad Request' }
    );
  });

});