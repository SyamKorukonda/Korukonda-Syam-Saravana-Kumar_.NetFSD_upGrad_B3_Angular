/// <reference types="jasmine" />

import { ComponentFixture, TestBed } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { of } from 'rxjs';

import { AdminUsersComponent } from './admin-users.component';
import { AuthService } from '../../../services/auth.service';


// Mock user data
// Used instead of backend API response
const mockUsers = [

  {
    userId: 1,
    userName: 'Admin User',
    emailAddress: 'admin@shopez.com',
    role: 'Admin'
  },

  {
    userId: 2,
    userName: 'John Doe',
    emailAddress: 'john@shopez.com',
    role: 'Customer'
  },

  {
    userId: 3,
    userName: 'Jane Smith',
    emailAddress: 'jane@shopez.com',
    role: 'Customer'
  }
];


// Mock AuthService
// Used to avoid real backend calls
function makeAuthService() {

  return jasmine.createSpyObj(
    'AuthService',

    ['getAllUsers'],

    {

      // Fake login status
      isLoggedIn: of(true),

      // Fake current user role
      currentUser: of('Admin')
    }
  );
}


describe('AdminUsersComponent', () => {

  let component: AdminUsersComponent;

  // Used to access component HTML and TS
  let fixture: ComponentFixture<AdminUsersComponent>;

  let authSpy: jasmine.SpyObj<AuthService>;


  // Runs before every test case
  // Creates component and fake service
  beforeEach(async () => {

    // Create fake service
    authSpy = makeAuthService();

    // Fake API response
    authSpy.getAllUsers
      .and.returnValue(of(mockUsers));

    await TestBed.configureTestingModule({

      imports: [
        AdminUsersComponent,
        FormsModule
      ],

      // Replace real service with fake service
      providers: [
        {
          provide: AuthService,
          useValue: authSpy
        }
      ]

    }).compileComponents();


    // Create component
    fixture = TestBed.createComponent(AdminUsersComponent);

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
  // Checks whether getAllUsers() is called
  it('should call getAllUsers on init', () => {

    expect(authSpy.getAllUsers)
      .toHaveBeenCalled();
  });


  // Loading state test
  // Loading should stop after users load
  it('should stop loading after init', () => {

    expect(component.loading)
      .toBeFalse();
  });


  // allUsers test
  // Checks whether all users are loaded correctly
  it('should load all users', () => {

    expect(component.allUsers.length)
      .toBe(3);
  });


  // filteredUsers test
  // filteredUsers should match allUsers initially
  it('should initialize filteredUsers correctly', () => {

    expect(component.filteredUsers.length)
      .toBe(3);
  });


  // Template test
  // User names should appear in UI
  it('should display user names', async () => {

    await fixture.whenStable();

    const el = fixture.nativeElement;

    expect(el.textContent)
      .toContain('Admin User');

    expect(el.textContent)
      .toContain('John Doe');

    expect(el.textContent)
      .toContain('Jane Smith');
  });


  // Template test
  // User emails should appear in UI
  it('should display user emails', async () => {

    await fixture.whenStable();

    const el = fixture.nativeElement;

    expect(el.textContent)
      .toContain('admin@shopez.com');

    expect(el.textContent)
      .toContain('john@shopez.com');
  });


  // Template test
  // User roles should appear in UI
  it('should display user roles', async () => {

    await fixture.whenStable();

    const el = fixture.nativeElement;

    expect(el.textContent)
      .toContain('Admin');

    expect(el.textContent)
      .toContain('Customer');
  });


  // Search test
  // Users should filter by name
  it('should filter users by name', (done) => {

    component.onSearch('john');

    setTimeout(() => {

      expect(component.filteredUsers.length)
        .toBe(1);

      expect(component.filteredUsers[0].userName)
        .toBe('John Doe');

      done();

    }, 350);
  });


  // Search test
  // Users should filter by email
  it('should filter users by email', (done) => {

    component.onSearch('admin@shopez');

    setTimeout(() => {

      expect(component.filteredUsers.length)
        .toBe(1);

      expect(component.filteredUsers[0].role)
        .toBe('Admin');

      done();

    }, 350);
  });


  // clearSearch() test
  // All users should appear after clearing search
  it('should show all users after clearing search', (done) => {

    component.onSearch('john');

    setTimeout(() => {

      component.clearSearch();

      setTimeout(() => {

        expect(component.filteredUsers.length)
          .toBe(3);

        done();

      }, 350);

    }, 350);
  });


  // clearSearch() reset test
  // searchTerm should reset after clearSearch()
  it('should reset searchTerm after clearSearch()', () => {

    component.searchTerm = 'john';

    component.clearSearch();

    expect(component.searchTerm)
      .toBe('');
  });


  // No match search test
  // filteredUsers should become empty
  it('should show 0 users when no matches found', (done) => {

    component.onSearch('xyz_not_exist');

    setTimeout(() => {

      expect(component.filteredUsers.length)
        .toBe(0);

      done();

    }, 350);
  });

});