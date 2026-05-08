/// <reference types="jasmine" />

import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { HomeComponent } from '../../pages/home/home.component';


describe('HomeComponent', () => {

  let component: HomeComponent;

  // Used to access component HTML and TS
  let fixture: ComponentFixture<HomeComponent>;


  // Runs before every test case
  // Creates component and testing module
  beforeEach(async () => {

    await TestBed.configureTestingModule({

      imports: [
        HomeComponent,
        RouterTestingModule
      ]

    }).compileComponents();


    // Create component
    fixture = TestBed.createComponent(HomeComponent);

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


  // Features array test
  // Home page should contain 4 features
  it('should contain 4 feature items', () => {

    expect(component.features.length)
      .toBe(4);
  });


  // Feature title test
  // Fast Delivery feature should exist
  it('should contain Fast Delivery feature', () => {

    const found = component.features
      .some(feature =>
        feature.title === 'Fast Delivery'
      );

    expect(found)
      .toBeTrue();
  });


  // Feature title test
  // Secure Payments feature should exist
  it('should contain Secure Payments feature', () => {

    const found = component.features
      .some(feature =>
        feature.title === 'Secure Payments'
      );

    expect(found)
      .toBeTrue();
  });


  // Feature title test
  // Easy Returns feature should exist
  it('should contain Easy Returns feature', () => {

    const found = component.features
      .some(feature =>
        feature.title === 'Easy Returns'
      );

    expect(found)
      .toBeTrue();
  });


  // Feature title test
  // 24/7 Support feature should exist
  it('should contain 24/7 Support feature', () => {

    const found = component.features
      .some(feature =>
        feature.title === '24/7 Support'
      );

    expect(found)
      .toBeTrue();
  });


  // Icon validation test
  // Every feature should contain an icon
  it('should contain icon for every feature', () => {

    component.features.forEach(feature => {

      expect(feature.icon)
        .toBeTruthy();
    });
  });


  // Description validation test
  // Every feature should contain description text
  it('should contain description for every feature', () => {

    component.features.forEach(feature => {

      expect(feature.desc)
        .toBeTruthy();
    });
  });


  // Template test
  // ShopEZ brand name should appear in UI
  it('should display ShopEZ brand name', () => {

    const el = fixture.nativeElement;

    expect(el.textContent)
      .toContain('ShopEZ');
  });


  // Template test
  // Shop Now button should appear
  it('should display Shop Now button', () => {

    const el = fixture.nativeElement;

    expect(el.textContent)
      .toContain('Shop Now');
  });


  // Template test
  // Join Free button should appear
  it('should display Join Free button', () => {

    const el = fixture.nativeElement;

    expect(el.textContent)
      .toContain('Join Free');
  });


  // Template test
  // Feature titles should appear in UI
  it('should display feature titles', () => {

    const el = fixture.nativeElement;

    expect(el.textContent)
      .toContain('Fast Delivery');

    expect(el.textContent)
      .toContain('Secure Payments');

    expect(el.textContent)
      .toContain('Easy Returns');
  });


  // CTA button test
  // Browse products CTA should appear
  it('should display Browse All Products button', () => {

    const el = fixture.nativeElement;

    expect(el.textContent)
      .toContain('Browse');
  });

});