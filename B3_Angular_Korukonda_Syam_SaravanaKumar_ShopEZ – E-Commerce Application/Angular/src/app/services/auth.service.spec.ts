import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { AuthService } from '../services/auth.service';
import { environment } from '../../environments/environments';

describe('AuthService', () => {

  let service: AuthService;
  let httpMock: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule],
      providers: [AuthService]
    });

    service = TestBed.inject(AuthService);
    httpMock = TestBed.inject(HttpTestingController);

    localStorage.clear();
  });

  afterEach(() => {
    httpMock.verify();
  });

  //  Creation 
  it('should be created', () => {
    expect(service).toBeTruthy();
  });

  //  Login 
  it('should login and store token', () => {

    service.login({ email: 'test@test.com', password: '123' })
      .subscribe(res => {
        expect(res).toBeTruthy();
        expect(localStorage.getItem('shopez_token')).toBe('token123');
      });

    const req = httpMock.expectOne(`${environment.gatewayUrl}/auth/login`);
    expect(req.request.method).toBe('POST');

    req.flush({
      success: true,
      data: { token: 'token123', role: 'Customer' }
    });
  });

  //  Register 
  it('should register user', () => {

    service.register({ name: 'test', email: 'test@test.com', password: '123', role: 'Customer' })
      .subscribe(res => {
        expect(res.success).toBeTrue();
      });

    const req = httpMock.expectOne(`${environment.gatewayUrl}/auth/register`);
    expect(req.request.method).toBe('POST');

    req.flush({ success: true });
  });

  //  Logout 
  it('should clear localStorage on logout', () => {

    localStorage.setItem('shopez_token', 'abc');

    service.logout();

    expect(localStorage.getItem('shopez_token')).toBeNull();
  });

});