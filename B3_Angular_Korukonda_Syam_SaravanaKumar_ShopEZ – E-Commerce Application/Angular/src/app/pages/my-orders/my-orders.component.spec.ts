/// <reference types="jasmine" />

import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { of, throwError } from 'rxjs';

import { MyOrdersComponent } from '../../pages/my-orders/my-orders.component';
import { OrderService } from '../../services/order.service';


// Mock order data
// Used instead of backend API response
const mockOrders = [

  {
    orderId: 1,
    userId: 1,
    orderDate: '2024-01-01T10:00:00',
    totalAmount: 76000,
    isCancelled: false,

    items: [
      {
        productId: 1,
        productName: 'Laptop',
        quantity: 1,
        price: 75000,
        subtotal: 75000
      }
    ]
  },

  {
    orderId: 2,
    userId: 1,
    orderDate: '2024-01-02T12:00:00',
    totalAmount: 500,
    isCancelled: true,

    items: [
      {
        productId: 2,
        productName: 'T-Shirt',
        quantity: 1,
        price: 500,
        subtotal: 500
      }
    ]
  }
];


// Mock OrderService
// Used to avoid real backend calls
function makeOrderService() {

  return jasmine.createSpyObj(
    'OrderService',

    [
      'getMyOrders',
      'cancelOrder'
    ],

    {

      // Fake orders observable
      orders: of(mockOrders)
    }
  );
}


describe('MyOrdersComponent', () => {

  let component: MyOrdersComponent;

  // Used to access component HTML and TS
  let fixture: ComponentFixture<MyOrdersComponent>;

  let orderSpy: jasmine.SpyObj<OrderService>;


  // Runs before every test case
  // Creates component and fake service
  beforeEach(async () => {

    // Create fake service
    orderSpy = makeOrderService();

    // Fake API response
    orderSpy.getMyOrders
      .and.returnValue(of(mockOrders));

    await TestBed.configureTestingModule({

      imports: [
        MyOrdersComponent,
        RouterTestingModule
      ],

      // Replace real service with fake service
      providers: [
        {
          provide: OrderService,
          useValue: orderSpy
        }
      ]

    }).compileComponents();


    // Create component
    fixture = TestBed.createComponent(MyOrdersComponent);

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


  // ngOnInit test
  // Checks whether getMyOrders() is called
  it('should call getMyOrders on init', () => {

    expect(orderSpy.getMyOrders)
      .toHaveBeenCalled();
  });


  // Loading state test
  // Loading should stop after orders load
  it('should stop loading after init', () => {

    expect(component.loading)
      .toBeFalse();
  });


  // Observable initialization test
  // Checks whether orders$ is assigned
  it('should initialize orders$', () => {

    expect(component.orders$)
      .toBeTruthy();
  });


  // Template test
  // Order ID should appear in UI
  it('should display order ID', async () => {

    await fixture.whenStable();

    const el = fixture.nativeElement;

    expect(el.textContent)
      .toContain('1');
  });


  // Template test
  // Total amount should appear in UI
  it('should display total amount', async () => {

    await fixture.whenStable();

    const el = fixture.nativeElement;

    expect(el.textContent)
      .toContain('76000');
  });


  // Template test
  // Product name should appear in UI
  it('should display product name', async () => {

    await fixture.whenStable();

    const el = fixture.nativeElement;

    expect(el.textContent)
      .toContain('Laptop');
  });


  // cancelOrder success test
  // Checks whether cancelOrder() is called correctly
  it('should call cancelOrder with correct ID', () => {

    // Fake successful response
    orderSpy.cancelOrder.and.returnValue(

      of({
        success: true,
        message: 'Cancelled',
        data: 'Cancelled'
      })
    );

    // Fake confirm popup
    spyOn(window, 'confirm')
      .and.returnValue(true);

    component.cancel(mockOrders[0] as any);

    expect(orderSpy.cancelOrder)
      .toHaveBeenCalledWith(1);
  });


  // cancelOrder validation test
  // API should not call if user cancels confirmation
  it('should not call cancelOrder when confirm is false', () => {

    spyOn(window, 'confirm')
      .and.returnValue(false);

    component.cancel(mockOrders[0] as any);

    expect(orderSpy.cancelOrder)
      .not.toHaveBeenCalled();
  });


  // cancellingId success test
  // cancellingId should reset after successful cancellation
  it('should reset cancellingId after success', () => {

    orderSpy.cancelOrder.and.returnValue(

      of({
        success: true,
        message: 'Cancelled',
        data: 'Cancelled'
      })
    );

    spyOn(window, 'confirm')
      .and.returnValue(true);

    component.cancel(mockOrders[0] as any);

    expect(component.cancellingId)
      .toBeNull();
  });


  // cancellingId error test
  // cancellingId should reset after API failure
  it('should reset cancellingId after error', () => {

    orderSpy.cancelOrder.and.returnValue(

      throwError(() => new Error('fail'))
    );

    spyOn(window, 'confirm')
      .and.returnValue(true);

    component.cancel(mockOrders[0] as any);

    expect(component.cancellingId)
      .toBeNull();
  });

});