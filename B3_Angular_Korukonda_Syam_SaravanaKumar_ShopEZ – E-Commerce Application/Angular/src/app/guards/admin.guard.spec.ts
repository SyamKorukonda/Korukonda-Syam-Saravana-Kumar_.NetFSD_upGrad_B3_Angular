import { TestBed } from '@angular/core/testing';
import { Router } from '@angular/router';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { of, from } from 'rxjs';
import { adminGuard } from '../guards/admin.guard';
import { AuthService } from '../services/auth.service';

//  Mock AuthService 
function makeAuthService(role: string) {
  return {
    isLoggedIn: of(role !== ''),
    currentUser: of(role),
    getToken: () => role !== '' ? 'token' : null
  };
}

describe('adminGuard', () => {

  //  Setup 
  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        HttpClientTestingModule
      ],
      providers: [AuthService]
    });
  });

  //  Cleanup 
  afterEach(() => {
    localStorage.clear();
  });

  //  Allow Admin 
  it('should allow access when role is Admin', (done) => {

    TestBed.overrideProvider(AuthService, {
      useValue: makeAuthService('Admin')
    });

    TestBed.runInInjectionContext(() => {

      const result = adminGuard({} as any, {} as any);

      from(result as any).subscribe(res => {
        expect(res as boolean).toBeTrue();   // ✅ Admin → allowed
        done();
      });

    });

  });

  //  Block Customer 
  it('should block access when role is Customer', (done) => {

    TestBed.overrideProvider(AuthService, {
      useValue: makeAuthService('Customer')
    });

    TestBed.runInInjectionContext(() => {

      const result = adminGuard({} as any, {} as any);

      from(result as any).subscribe(res => {
        expect(res as boolean).toBeFalse();  // ✅ FIXED
        done();
      });

    });

  });

  //  Block empty role 
  it('should block access when role is empty', (done) => {

    TestBed.overrideProvider(AuthService, {
      useValue: makeAuthService('')
    });

    TestBed.runInInjectionContext(() => {

      const result = adminGuard({} as any, {} as any);

      from(result as any).subscribe(res => {
        expect(res as boolean).toBeFalse();  // ✅ FIXED
        done();
      });

    });

  });

  //  Redirect Customer 
  it('should redirect to /home for Customer role', (done) => {

    TestBed.overrideProvider(AuthService, {
      useValue: makeAuthService('Customer')
    });

    const router = TestBed.inject(Router);
    const navSpy = spyOn(router, 'navigate');

    TestBed.runInInjectionContext(() => {

      const result = adminGuard({} as any, {} as any);

      from(result as any).subscribe(() => {
        expect(navSpy).toHaveBeenCalledWith(['/home']); // ✅ FIXED
        done();
      });

    });

  });

  //  No redirect for Admin 
  it('should NOT redirect for Admin role', (done) => {

    TestBed.overrideProvider(AuthService, {
      useValue: makeAuthService('Admin')
    });

    const router = TestBed.inject(Router);
    const navSpy = spyOn(router, 'navigate');

    TestBed.runInInjectionContext(() => {

      const result = adminGuard({} as any, {} as any);

      from(result as any).subscribe(() => {
        expect(navSpy).not.toHaveBeenCalled(); // ✅ FIXED
        done();
      });

    });

  });

});