import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { of, throwError } from 'rxjs';

import { LoginComponent } from '../../pages/login/login.component';
import { AuthService } from '../../services/auth.service';


// Mock AuthService
// We create fake methods instead of calling real backend APIs
function makeAuthService() {

  return jasmine.createSpyObj(
    'AuthService',
    ['login'],
    {

      // Fake login status observable
      isLoggedIn: of(false),

      // Fake current user observable
      currentUser: of('')
    }
  );
}


describe('LoginComponent', () => {

  let component: LoginComponent;

  // Used to access component HTML and TS
  let fixture: ComponentFixture<LoginComponent>;

  // Fake AuthService object
  let authSpy: jasmine.SpyObj<AuthService>;


  // Runs before every test case
  // Used to create component and setup testing module
  beforeEach(async () => {

    // Create fake AuthService
    authSpy = makeAuthService();

    await TestBed.configureTestingModule({

      // Import required modules
      imports: [
        LoginComponent,
        ReactiveFormsModule,
        RouterTestingModule
      ],

      // Replace real AuthService with fake service
      providers: [
        {
          provide: AuthService,
          useValue: authSpy
        }
      ]

    }).compileComponents();


    // Create component instance
    fixture = TestBed.createComponent(LoginComponent);

    // Access component class
    component = fixture.componentInstance;

    // Trigger component lifecycle methods
    fixture.detectChanges();
  });


  // Helper method
  // Used to avoid writing same form code again and again
  function fillForm(
    email = 'test@shopez.com',
    password = 'Test@1234'
  ) {

    component.form.patchValue({
      email,
      password
    });
  }


  // Component creation test
  // Checks whether component is created successfully
  it('should create component', () => {

    expect(component).toBeTruthy();
  });


  // Initial form values test
  // Checks default values when component loads
  it('should initialize with default values', () => {

    // Email should be empty initially
    expect(component.form.get('email')?.value)
      .toBe('');

    // Password should be empty initially
    expect(component.form.get('password')?.value)
      .toBe('');

    // Password should be hidden initially
    expect(component.show)
      .toBeFalse();

    // Loading should be false initially
    expect(component.loading)
      .toBeFalse();

    // Error message should be empty initially
    expect(component.error)
      .toBe('');
  });


  // Email validation test
  // Checks email validation rules
  it('should validate email correctly', () => {

    // Empty email should be invalid
    component.form.get('email')
      ?.setValue('');

    expect(component.form.get('email')?.invalid)
      .toBeTrue();


    // Wrong email format should be invalid
    component.form.get('email')
      ?.setValue('wrongemail');

    expect(component.form.get('email')?.invalid)
      .toBeTrue();


    // Correct email format should be valid
    component.form.get('email')
      ?.setValue('test@shopez.com');

    expect(component.form.get('email')?.valid)
      .toBeTrue();
  });


  // Password validation test
  // Checks password minimum length validation
  it('should validate password correctly', () => {

    // Small password should be invalid
    component.form.get('password')
      ?.setValue('123');

    expect(component.form.get('password')?.invalid)
      .toBeTrue();


    // Password with 8+ characters should be valid
    component.form.get('password')
      ?.setValue('Test@1234');

    expect(component.form.get('password')?.valid)
      .toBeTrue();
  });


  // Form validation test
  // Checks full form validity
  it('should make form valid with correct values', () => {

    // Fill valid data
    fillForm();

    // Entire form should become valid
    expect(component.form.valid)
      .toBeTrue();
  });


  // Invalid form submit test
  // Login API should not call when form is invalid
  it('should not call login when form is invalid', () => {

    // Submit empty form
    component.submit();

    // Login method should not be called
    expect(authSpy.login)
      .not.toHaveBeenCalled();
  });


  // Successful login test
  // Checks whether login service is called correctly
  it('should call login service with correct values', () => {

    // Fake successful API response
    authSpy.login.and.returnValue(

      of({
        success: true,
        message: 'Login successful',

        data: {
          token: 'token',
          role: 'Customer',
          message: ''
        }
      })
    );

    // Fill valid form data
    fillForm();

    // Submit form
    component.submit();

    // Verify login method called with correct data
    expect(authSpy.login)
      .toHaveBeenCalledWith({

        email: 'test@shopez.com',
        password: 'Test@1234'
      });
  });


  // Success loading test
  // Loading should stop after successful login
  it('should stop loading after successful login', () => {

    authSpy.login.and.returnValue(

      of({
        success: true,
        message: '',

        data: {
          token: 'token',
          role: 'Customer',
          message: ''
        }
      })
    );

    fillForm();

    component.submit();

    // Loading should become false
    expect(component.loading)
      .toBeFalse();
  });


  // Error handling test
  // Shows backend error message on failure
  it('should show error message on login failure', () => {

    // Fake failed API response
    authSpy.login.and.returnValue(

      throwError(() => ({
        error: {
          message: 'Invalid credentials'
        }
      }))
    );

    // Fill wrong credentials
    fillForm(
      'wrong@shopez.com',
      'WrongPass1'
    );

    // Submit form
    component.submit();

    // Verify error message
    expect(component.error)
      .toBe('Invalid credentials');
  });


  // Failure loading test
  // Loading should stop after API failure
  it('should stop loading after failed login', () => {

    authSpy.login.and.returnValue(

      throwError(() => ({
        error: {
          message: 'Error'
        }
      }))
    );

    fillForm();

    component.submit();

    // Loading should become false
    expect(component.loading)
      .toBeFalse();
  });


  // Password visibility test
  // Checks password show/hide functionality
  it('should toggle password visibility', () => {

    // Initially hidden
    expect(component.show)
      .toBeFalse();

    // Show password
    component.show = true;

    expect(component.show)
      .toBeTrue();

    // Hide password again
    component.show = false;

    expect(component.show)
      .toBeFalse();
  });


  // Getter test
  // Checks whether form controls are accessible
  it('should return form controls using f getter', () => {

    expect(component.f['email'])
      .toBeTruthy();

    expect(component.f['password'])
      .toBeTruthy();
  });

});