import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { of } from 'rxjs';

import { NavbarComponent } from './navbar.component';
import { AuthService } from '../services/auth.service';
import { CartService } from '../services/cart.service';
import { ThemeService } from '../services/theme.service';


// Mock AuthService
// Used to avoid calling real authentication APIs
function makeAuth(
  loggedIn: boolean,
  role: string
) {

  return jasmine.createSpyObj(
    'AuthService',
    ['logout'],
    {

      // Fake login status
      isLoggedIn: of(loggedIn),

      // Fake user role
      currentUser: of(role)
    }
  );
}


// Mock CartService
// Used to avoid real cart service calls
function makeCart(count: number) {

  return jasmine.createSpyObj(
    'CartService',
    ['toggle', 'getCart'],
    {

      // Fake cart count
      cartCount: of(count),

      // Fake cart items
      cartItems: of([]),

      // Fake total price
      totalPrice: of(0),

      // Fake cart open state
      isCartOpen: of(false)
    }
  );
}


// Mock ThemeService
// Used to test dark/light mode
function makeTheme(isDark: boolean) {

  return jasmine.createSpyObj(
    'ThemeService',
    ['toggle'],
    {

      // Fake dark mode state
      isDarkMode: of(isDark)
    }
  );
}


describe('NavbarComponent', () => {

  let component: NavbarComponent;

  // Used to access component HTML and TS
  let fixture: ComponentFixture<NavbarComponent>;

  let authSpy: jasmine.SpyObj<AuthService>;

  let cartSpy: jasmine.SpyObj<CartService>;

  let themeSpy: jasmine.SpyObj<ThemeService>;


  // Runs before every test case
  // Creates component and fake services
  beforeEach(async () => {

    // Create fake services
    authSpy = makeAuth(false, '');

    cartSpy = makeCart(0);

    themeSpy = makeTheme(false);

    // Fake cart response
    cartSpy.getCart.and.returnValue(of({} as any));

    await TestBed.configureTestingModule({

      imports: [
        NavbarComponent,
        RouterTestingModule
      ],

      // Replace real services with fake services
      providers: [

        {
          provide: AuthService,
          useValue: authSpy
        },

        {
          provide: CartService,
          useValue: cartSpy
        },

        {
          provide: ThemeService,
          useValue: themeSpy
        }
      ]

    }).compileComponents();


    // Create component
    fixture = TestBed.createComponent(NavbarComponent);

    // Access component class
    component = fixture.componentInstance;

    // Trigger lifecycle methods
    fixture.detectChanges();
  });


  // Component creation test
  // Checks whether component is created successfully
  it('should create component', () => {

    expect(component).toBeTruthy();
  });


  // Observable initialization test
  // Checks whether observables are assigned properly
  it('should initialize observables', () => {

    expect(component.isLoggedIn$)
      .toBeTruthy();

    expect(component.role$)
      .toBeTruthy();

    expect(component.cartCount$)
      .toBeTruthy();

    expect(component.isDark$)
      .toBeTruthy();
  });


  // Cart toggle test
  // Checks whether toggleCart() calls cart service
  it('should call toggleCart()', () => {

    component.toggleCart();

    expect(cartSpy.toggle)
      .toHaveBeenCalled();
  });


  // Theme toggle test
  // Checks whether toggleTheme() calls theme service
  it('should call toggleTheme()', () => {

    component.toggleTheme();

    expect(themeSpy.toggle)
      .toHaveBeenCalled();
  });


  // Logout test
  // Checks whether logout() calls auth service
  it('should call logout()', () => {

    component.logout();

    expect(authSpy.logout)
      .toHaveBeenCalled();
  });


  // Guest UI test
  // Login and Register should appear for guest users
  it('should show Login and Register links', () => {

    const el = fixture.nativeElement;

    expect(el.textContent)
      .toContain('Login');

    expect(el.textContent)
      .toContain('Register');
  });


  // Brand name test
  // Checks application name in navbar
  it('should show ShopEZ brand name', () => {

    const el = fixture.nativeElement;

    expect(el.textContent)
      .toContain('ShopEZ');
  });


  // Default cart count test
  // Checks whether cart count starts with 0
  it('should show default cart count as 0', (done) => {

    component.cartCount$.subscribe(count => {

      expect(count)
        .toBe(0);

      done();
    });
  });


  // Updated cart count test
  // Checks whether cart count updates correctly
  it('should update cart count correctly', (done) => {

    // Directly update observable
    component.cartCount$ = of(5);

    component.cartCount$.subscribe(count => {

      expect(count)
        .toBe(5);

      done();
    });
  });


  // Dark mode test
  // Checks default dark mode state
  it('should have dark mode disabled initially', (done) => {

    component.isDark$.subscribe(isDark => {

      expect(isDark)
        .toBeFalse();

      done();
    });
  });


  // Logged-in UI test
  // Logout button should appear after login
  it('should show Logout when user is logged in', () => {

    // Directly update login observable
    component.isLoggedIn$ = of(true);

    fixture.detectChanges();

    const el = fixture.nativeElement;

    expect(el.textContent)
      .toContain('Logout');
  });


  // Admin role test
  // Admin menu should appear for admin users
  it('should show Admin menu for admin role', () => {

    // Directly update role observable
    component.role$ = of('Admin');

    fixture.detectChanges();

    const el = fixture.nativeElement;

    expect(el.textContent)
      .toContain('Admin');
  });


  // Customer role test
  // Admin menu should NOT appear for customer users
  it('should not show Admin menu for customer role', () => {

    // Set customer role
    component.role$ = of('Customer');

    fixture.detectChanges();

    const el = fixture.nativeElement;

    expect(el.textContent)
      .not.toContain('Dashboard');
  });

});