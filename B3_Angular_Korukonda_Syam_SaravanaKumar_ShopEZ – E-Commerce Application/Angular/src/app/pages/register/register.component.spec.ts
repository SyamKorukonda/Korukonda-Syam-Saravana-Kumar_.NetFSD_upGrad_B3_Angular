/// <reference types="jasmine" />

import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { of, throwError } from 'rxjs';

import { RegisterComponent } from '../../pages/register/register.component';
import { AuthService } from '../../services/auth.service';


// Mock AuthService
// Used to avoid real backend API calls
function makeAuthService() {

  return jasmine.createSpyObj(
    'AuthService',

    ['register'],

    {

      // Fake login status
      isLoggedIn: of(false),

      // Fake current user
      currentUser: of('')
    }
  );
}


// describe()
// Used to group related test cases for one component/service
describe('RegisterComponent', () => {

  // let
  // Used to declare variables for testing
  let component: RegisterComponent;

  // ComponentFixture
  // Used to access component HTML and TypeScript
  let fixture: ComponentFixture<RegisterComponent>;

  // jasmine.SpyObj
  // Used to create fake service methods
  let authSpy: jasmine.SpyObj<AuthService>;


  // beforeEach()
  // Runs before every test case
  // Used for setup and initialization
  beforeEach(async () => {

    // Create fake AuthService
    authSpy = makeAuthService();

    // TestBed
    // Used to configure Angular testing module
    await TestBed.configureTestingModule({

      // imports
      // Used to import required Angular modules/components
      imports: [
        RegisterComponent,
        ReactiveFormsModule,
        RouterTestingModule
      ],

      // providers
      // Used to replace real services with fake services
      providers: [
        {
          provide: AuthService,
          useValue: authSpy
        }
      ]

    }).compileComponents();


    // createComponent()
    // Used to create component instance
    fixture = TestBed.createComponent(RegisterComponent);

    // componentInstance
    // Used to access component TypeScript code
    component = fixture.componentInstance;

    // detectChanges()
    // Used to trigger Angular lifecycle methods
    fixture.detectChanges();
  });


  // Helper function
  // Used to avoid repeated form code
  function fillForm() {

    // patchValue()
    // Used to update form values
    component.form.patchValue({

      name: 'John Doe',

      email: 'john@shopez.com',

      password: 'Test@1234'
    });
  }


  // it()
  // Used to write individual test case
  it('should create component', () => {

    // expect()
    // Used to check expected output
    expect(component)
      .toBeTruthy();
  });


  // Form validation test
  it('should make form valid with correct values', () => {

    fillForm();

    // valid
    // Checks whether form is valid
    expect(component.form.valid)
      .toBeTrue();
  });


  // Service call test
  it('should call register service', () => {

    // and.returnValue()
    // Used to return fake observable response
    authSpy.register.and.returnValue(

      of({
        success: true,
        message: 'Registered',
        data: 'Success'
      })
    );

    fillForm();

    // submit()
    // Used to execute component logic
    component.submit();

    // toHaveBeenCalled()
    // Checks whether method is called
    expect(authSpy.register)
      .toHaveBeenCalled();
  });


  // Error handling test
  it('should show error message on failure', () => {

    authSpy.register.and.returnValue(

      // throwError()
      // Used to simulate API failure
      throwError(() => ({
        error: {
          message: 'Email already exists'
        }
      }))
    );

    fillForm();

    component.submit();

    // toBe()
    // Used to compare exact value
    expect(component.error)
      .toBe('Email already exists');
  });

});