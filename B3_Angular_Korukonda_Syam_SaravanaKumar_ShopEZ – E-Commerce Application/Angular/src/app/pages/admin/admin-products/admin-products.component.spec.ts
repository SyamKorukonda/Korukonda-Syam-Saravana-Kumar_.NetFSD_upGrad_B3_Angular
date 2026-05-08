/// <reference types="jasmine" />

import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { of } from 'rxjs';
import { AdminProductsComponent } from './admin-products.component';
import { ProductService } from '../../../services/product.service';

// Mock product data
// Used instead of backend API data
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
    category: 'Clothing'
  }
];


// Mock ProductService
// Used to avoid real API calls
function makeProductService() {

  return jasmine.createSpyObj(
    'ProductService',

    [
      'getAll',
      'create',
      'update',
      'delete'
    ],

    {

      // Fake products observable
      products: of(mockProducts),

      // Fake loading observable
      loading: of(false)
    }
  );
}


describe('AdminProductsComponent', () => {

  let component: AdminProductsComponent;

  // Used to access component HTML and TS
  let fixture: ComponentFixture<AdminProductsComponent>;

  let productSpy: jasmine.SpyObj<ProductService>;


  // Runs before every test case
  // Creates component and fake service
  beforeEach(async () => {

    // Create fake service
    productSpy = makeProductService();

    // Fake API response
    productSpy.getAll
      .and.returnValue(of(mockProducts));

    await TestBed.configureTestingModule({

      imports: [
        AdminProductsComponent,
        ReactiveFormsModule,
        RouterTestingModule
      ],

      // Replace real service with fake service
      providers: [
        {
          provide: ProductService,
          useValue: productSpy
        }
      ]

    }).compileComponents();


    // Create component
    fixture = TestBed.createComponent(AdminProductsComponent);

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
  // Checks whether getAll() is called
  it('should call getAll on init', () => {

    expect(productSpy.getAll)
      .toHaveBeenCalled();
  });


  // Initial value tests
  // Checks default component values
  it('should initialize default values', () => {

    // Form should be hidden initially
    expect(component.showForm)
      .toBeFalse();

    // editingId should be null initially
    expect(component.editingId)
      .toBeNull();
  });


  // openForm() test
  // Form should open for create operation
  it('should open form', () => {

    component.openForm();

    expect(component.showForm)
      .toBeTrue();
  });


  // openForm() reset test
  // editingId should reset during create mode
  it('should reset editingId in openForm()', () => {

    component.editingId = 1;

    component.openForm();

    expect(component.editingId)
      .toBeNull();
  });


  // edit() test
  // editingId should store selected product ID
  it('should set editingId during edit', () => {

    component.edit(mockProducts[0] as any);

    expect(component.editingId)
      .toBe(1);
  });


  // edit() form patch test
  // Product data should fill form
  it('should populate form during edit', () => {

    component.edit(mockProducts[0] as any);

    expect(component.form.get('name')?.value)
      .toBe('Laptop');

    expect(component.form.get('price')?.value)
      .toBe(75000);
  });


  // edit() UI test
  // Form should become visible during edit
  it('should show form during edit', () => {

    component.edit(mockProducts[0] as any);

    expect(component.showForm)
      .toBeTrue();
  });


  // cancelForm() UI test
  // Form should close after cancel
  it('should hide form after cancel', () => {

    component.openForm();

    component.cancelForm();

    expect(component.showForm)
      .toBeFalse();
  });


  // cancelForm() reset test
  // editingId should reset after cancel
  it('should reset editingId after cancel', () => {

    component.editingId = 1;

    component.cancelForm();

    expect(component.editingId)
      .toBeNull();
  });


  // submit() validation test
  // create() should NOT call when form is invalid
  it('should not call create when form is invalid', () => {

    component.openForm();

    component.submit();

    expect(productSpy.create)
      .not.toHaveBeenCalled();
  });


  // submit() create test
  // create() should call during add operation
  it('should call create when adding product', () => {

  productSpy.create.and.returnValue(

    of({
      ...mockProducts[0],
      productId: 3
    } as any)
  );

  // Open form in create mode
  component.openForm();

  // Fill all required form fields
  component.form.patchValue({

    name: 'Headphones',

    description: 'Wireless headphones',

    price: 5000,

    stock: 20,

    imageUrl: 'test.jpg',

    category: 'Electronics'
  });

  // Submit form
  component.submit();

  // Verify create() is called
  expect(productSpy.create)
    .toHaveBeenCalled();
});


  // submit() update test
  // update() should call during edit operation
  it('should call update when editing product', () => {

    productSpy.update.and.returnValue(

      of({
        success: true,
        message: 'Updated',
        data: 'Updated'
      })
    );

    // Open edit mode
    component.edit(mockProducts[0] as any);

    // Update form data
    component.form.patchValue({

      name: 'Updated Laptop',

      description: 'New description',

      price: 80000,

      stock: 8,

      imageUrl: null
    });

    component.submit();

    expect(productSpy.update)
      .toHaveBeenCalledWith(
        1,
        jasmine.any(Object)
      );
  });


  // delete() success test
  // delete() should call after confirmation
  it('should call delete after confirmation', () => {

    productSpy.delete.and.returnValue(

      of({
        success: true,
        message: 'Deleted',
        data: 'Deleted'
      })
    );

    // Fake confirm popup
    spyOn(window, 'confirm')
      .and.returnValue(true);

    component.delete(1);

    expect(productSpy.delete)
      .toHaveBeenCalledWith(1);
  });


  // delete() cancel test
  // delete() should NOT call when confirmation is cancelled
  it('should not call delete when confirmation is cancelled', () => {

    spyOn(window, 'confirm')
      .and.returnValue(false);

    component.delete(1);

    expect(productSpy.delete)
      .not.toHaveBeenCalled();
  });


  // Image fallback test
  // Default image should load when image fails
  it('should show fallback image on image error', () => {

    const img = document.createElement('img');

    const event = {
      target: img
    } as any;

    component.onImgErr(event);

    expect(img.src)
      .toContain('no-image.png');
  });

});