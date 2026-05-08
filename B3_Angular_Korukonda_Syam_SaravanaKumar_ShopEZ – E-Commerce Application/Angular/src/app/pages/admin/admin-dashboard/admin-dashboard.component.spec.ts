/// <reference types="jasmine" />

import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { of } from 'rxjs';

import { AdminDashboardComponent } from './admin-dashboard.component';
import { ProductService } from '../../../services/product.service';
import { OrderService } from '../../../services/order.service';
import { AuthService } from '../../../services/auth.service';


// Mock product data
// Used instead of backend API response
const mockProducts = [

  {
    productId: 1,
    name: 'Laptop',
    description: '',
    price: 75000,
    stock: 10,
    imageUrl: null,
    category: 'Electronics'
  },

  {
    productId: 2,
    name: 'T-Shirt',
    description: '',
    price: 500,
    stock: 50,
    imageUrl: null,
    category: 'Clothing'
  }
];


// Mock order data
// Used instead of backend API response
const mockOrders = [

  {
    orderId: 1,
    userId: 1,
    orderDate: '2024-01-01',
    totalAmount: 75000,
    isCancelled: false,
    items: []
  },

  {
    orderId: 2,
    userId: 2,
    orderDate: '2024-01-02',
    totalAmount: 500,
    isCancelled: false,
    items: []
  },

  {
    orderId: 3,
    userId: 3,
    orderDate: '2024-01-03',
    totalAmount: 90000,
    isCancelled: true,
    items: []
  }
];


// Mock user data
// Used instead of backend API response
const mockUsers = [

  {
    userId: 1,
    userName: 'Admin',
    emailAddress: 'a@a.com',
    role: 'Admin'
  },

  {
    userId: 2,
    userName: 'John',
    emailAddress: 'j@j.com',
    role: 'Customer'
  }
];


// Mock ProductService
// Used to avoid real API calls
function makeProductService() {

  return jasmine.createSpyObj(
    'ProductService',

    ['getAll'],

    {

      // Fake products observable
      products: of(mockProducts),

      // Fake loading observable
      loading: of(false)
    }
  );
}


// Mock OrderService
// Used to avoid real backend calls
function makeOrderService() {

  return jasmine.createSpyObj(
    'OrderService',

    ['getAllOrders'],

    {

      // Fake orders observable
      allOrders: of(mockOrders)
    }
  );
}


// Mock AuthService
// Used to avoid real backend calls
function makeAuthService() {

  return jasmine.createSpyObj(
    'AuthService',

    ['getAllUsers'],

    {

      // Fake login state
      isLoggedIn: of(true),

      // Fake current user role
      currentUser: of('Admin')
    }
  );
}


describe('AdminDashboardComponent', () => {

  let component: AdminDashboardComponent;

  // Used to access component HTML and TS
  let fixture: ComponentFixture<AdminDashboardComponent>;

  let productSpy: jasmine.SpyObj<ProductService>;

  let orderSpy: jasmine.SpyObj<OrderService>;

  let authSpy: jasmine.SpyObj<AuthService>;


  // Runs before every test case
  // Creates component and fake services
  beforeEach(async () => {

    // Create fake services
    productSpy = makeProductService();

    orderSpy = makeOrderService();

    authSpy = makeAuthService();

    // Fake API responses
    productSpy.getAll
      .and.returnValue(of(mockProducts));

    orderSpy.getAllOrders
      .and.returnValue(of(mockOrders));

    authSpy.getAllUsers
      .and.returnValue(of(mockUsers));

    await TestBed.configureTestingModule({

      imports: [
        AdminDashboardComponent,
        RouterTestingModule
      ],

      // Replace real services with fake services
      providers: [

        {
          provide: ProductService,
          useValue: productSpy
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


    // Create component
    fixture = TestBed.createComponent(AdminDashboardComponent);

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


  // API call tests
  // Checks whether APIs are called on component load
  it('should call getAll products on init', () => {

    expect(productSpy.getAll)
      .toHaveBeenCalled();
  });


  it('should call getAllOrders on init', () => {

    expect(orderSpy.getAllOrders)
      .toHaveBeenCalled();
  });


  it('should call getAllUsers on init', () => {

    expect(authSpy.getAllUsers)
      .toHaveBeenCalled();
  });


  // Product count test
  // productCount should match product data
  it('should set productCount correctly', () => {

    expect(component.productCount)
      .toBe(2);
  });


  // Order count test
  // orderCount should match order data
  it('should set orderCount correctly', () => {

    expect(component.orderCount)
      .toBe(3);
  });


  // User count test
  // userCount should match user data
  it('should set userCount correctly', () => {

    expect(component.userCount)
      .toBe(2);
  });


  // Loading state test
  // Loading should stop after dashboard data loads
  it('should stop loading after init', () => {

    expect(component.loading)
      .toBeFalse();
  });


  // Quick links test
  // Dashboard should contain 4 navigation links
  it('should contain 4 quick links', () => {

    expect(component.links.length)
      .toBe(4);
  });


  // Products link test
  // Products route should exist
  it('should contain products link', () => {

    const found = component.links
      .some(link =>
        link.route === '/admin/products'
      );

    expect(found)
      .toBeTrue();
  });


  // Orders link test
  // Orders route should exist
  it('should contain orders link', () => {

    const found = component.links
      .some(link =>
        link.route === '/admin/orders'
      );

    expect(found)
      .toBeTrue();
  });


  // Users link test
  // Users route should exist
  it('should contain users link', () => {

    const found = component.links
      .some(link =>
        link.route === '/admin/users'
      );

    expect(found)
      .toBeTrue();
  });


  // Template test
  // Dashboard heading should appear in UI
  it('should display Dashboard heading', async () => {

    await fixture.whenStable();

    const el = fixture.nativeElement;

    expect(el.textContent)
      .toContain('Dashboard');
  });


  // Template test
  // Stat counts should appear in UI
  it('should display stat counts', async () => {

    await fixture.whenStable();

    const el = fixture.nativeElement;

    // Product count
    expect(el.textContent)
      .toContain('2');

    // Order count
    expect(el.textContent)
      .toContain('3');
  });

});