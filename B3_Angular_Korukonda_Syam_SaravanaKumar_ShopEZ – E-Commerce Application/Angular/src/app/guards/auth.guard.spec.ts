import { TestBed } from '@angular/core/testing';
import { Router } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { of, from } from 'rxjs';
import { authGuard, guestGuard } from '../guards/auth.guard';
import { AuthService } from '../services/auth.service';

//  Mock AuthService 
function makeAuthService(loggedIn: boolean, role: string) {
  return {
    isLoggedIn: of(loggedIn),
    currentUser: of(role),
    getToken: () => loggedIn ? 'fake-token' : null,
    logout: jasmine.createSpy('logout')
  };
}

//  authGuard Tests 
describe('authGuard', () => {

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        HttpClientTestingModule
      ],
      providers: [AuthService]
    });
  });

  afterEach(() => {
    localStorage.clear();
  });

  //  Allow access 
  it('should allow access when user is logged in', (done) => {

    TestBed.overrideProvider(AuthService, {
      useValue: makeAuthService(true, 'Customer')
    });

    TestBed.runInInjectionContext(() => {
      const result = authGuard({} as any, {} as any);

      from(result as any).subscribe(res => {
        expect(res as boolean).toBeTrue();
        done();
      });
    });

  });

  //  Block access 
  it('should block access when user is not logged in', (done) => {

    TestBed.overrideProvider(AuthService, {
      useValue: makeAuthService(false, '')
    });

    TestBed.runInInjectionContext(() => {
      const result = authGuard({} as any, {} as any);

      from(result as any).subscribe(res => {
        expect(res as boolean).toBeFalse();
        done();
      });
    });

  });

  //  Redirect to login 
  it('should redirect to /login when not logged in', (done) => {

    TestBed.overrideProvider(AuthService, {
      useValue: makeAuthService(false, '')
    });

    const router = TestBed.inject(Router);
    const navSpy = spyOn(router, 'navigate');

    TestBed.runInInjectionContext(() => {
      const result = authGuard({} as any, {} as any);

      from(result as any).subscribe(() => {
        expect(navSpy).toHaveBeenCalledWith(['/login']);
        done();
      });
    });

  });

  //  No redirect 
  it('should not redirect when user is logged in', (done) => {

    TestBed.overrideProvider(AuthService, {
      useValue: makeAuthService(true, 'Customer')
    });

    const router = TestBed.inject(Router);
    const navSpy = spyOn(router, 'navigate');

    TestBed.runInInjectionContext(() => {
      const result = authGuard({} as any, {} as any);

      from(result as any).subscribe(() => {
        expect(navSpy).not.toHaveBeenCalled();
        done();
      });
    });

  });

});


//  guestGuard Tests 
describe('guestGuard', () => {

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        HttpClientTestingModule
      ],
      providers: [AuthService]
    });
  });

  afterEach(() => {
    localStorage.clear();
  });

  //  Allow guest 
  it('should allow access when user is NOT logged in', (done) => {

    TestBed.overrideProvider(AuthService, {
      useValue: makeAuthService(false, '')
    });

    TestBed.runInInjectionContext(() => {
      const result = guestGuard({} as any, {} as any);

      from(result as any).subscribe(res => {
        expect(res as boolean).toBeTrue();
        done();
      });
    });

  });

  //  Block logged user 
  it('should block access when user IS logged in', (done) => {

    TestBed.overrideProvider(AuthService, {
      useValue: makeAuthService(true, 'Customer')
    });

    TestBed.runInInjectionContext(() => {
      const result = guestGuard({} as any, {} as any);

      from(result as any).subscribe(res => {
        expect(res as boolean).toBeFalse();
        done();
      });
    });

  });

  //  Redirect logged user 
  it('should redirect to /home when logged-in user visits login', (done) => {

    TestBed.overrideProvider(AuthService, {
      useValue: makeAuthService(true, 'Customer')
    });

    const router = TestBed.inject(Router);
    const navSpy = spyOn(router, 'navigate');

    TestBed.runInInjectionContext(() => {
      const result = guestGuard({} as any, {} as any);

      from(result as any).subscribe(() => {
        expect(navSpy).toHaveBeenCalledWith(['/home']);
        done();
      });
    });

  });

});