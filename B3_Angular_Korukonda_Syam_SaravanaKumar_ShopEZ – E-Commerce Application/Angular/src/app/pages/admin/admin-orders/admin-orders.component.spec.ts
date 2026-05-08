/// <reference types="jasmine" />

import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { of } from 'rxjs';
import { AdminOrdersComponent } from './admin-orders.component';
import { OrderService } from '../../../services/order.service';

// Mock order data
// Used instead of backend API response
const mockAllOrders = [

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
  }
];


// Mock OrderService
// Used to avoid real backend calls
function makeOrderService() {

  return jasmine.createSpyObj(
    'OrderService',

    [
      'getAllOrders',
      'cancelOrder'
    ],

    {

      // Fake all orders observable
      allOrders: of(mockAllOrders)
    }
  );
}


describe('AdminOrdersComponent', () => {

  let component: AdminOrdersComponent;

  // Used to access component HTML and TS
  let fixture: ComponentFixture<AdminOrdersComponent>;

  let orderSpy: jasmine.SpyObj<OrderService>;


  // Runs before every test case
  // Creates component and fake service
  beforeEach(async () => {

    // Create fake service
    orderSpy = makeOrderService();

    // Fake API response
    orderSpy.getAllOrders
      .and.returnValue(of(mockAllOrders));

    await TestBed.configureTestingModule({

      imports: [
        AdminOrdersComponent,
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
    fixture = TestBed.createComponent(AdminOrdersComponent);

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
  // Checks whether getAllOrders() is called
  it('should call getAllOrders on init', () => {

    expect(orderSpy.getAllOrders)
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
  // Order IDs should appear in UI
  it('should display order IDs', async () => {

    await fixture.whenStable();

    const el = fixture.nativeElement;

    expect(el.textContent)
      .toContain('1');

    expect(el.textContent)
      .toContain('2');
  });


  // Template test
  // Total amounts should appear in UI
  it('should display total amounts', async () => {

    await fixture.whenStable();

    const el = fixture.nativeElement;

    expect(el.textContent)
      .toContain('76000');

    expect(el.textContent)
      .toContain('90000');
  });


  // Template test
  // Product names should appear in UI
  it('should display product names', async () => {

    await fixture.whenStable();

    const el = fixture.nativeElement;

    expect(el.textContent)
      .toContain('Laptop');

    expect(el.textContent)
      .toContain('iPhone');
  });


  // cancel() success test
  // cancelOrder() should call after confirmation
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

    component.cancel(mockAllOrders[0] as any);

    expect(orderSpy.cancelOrder)
      .toHaveBeenCalledWith(1);
  });


  // cancel() validation test
  // API should NOT call when confirmation is cancelled
  it('should not cancel order when confirm is false', () => {

    spyOn(window, 'confirm')
      .and.returnValue(false);

    component.cancel(mockAllOrders[0] as any);

    expect(orderSpy.cancelOrder)
      .not.toHaveBeenCalled();
  });


  // cancellingId reset test
  // cancellingId should become null after cancellation
  it('should reset cancellingId after cancel', () => {

    orderSpy.cancelOrder.and.returnValue(

      of({
        success: true,
        message: 'Cancelled',
        data: 'Cancelled'
      })
    );

    spyOn(window, 'confirm')
      .and.returnValue(true);

    component.cancel(mockAllOrders[0] as any);

    expect(component.cancellingId)
      .toBeNull();
  });

});