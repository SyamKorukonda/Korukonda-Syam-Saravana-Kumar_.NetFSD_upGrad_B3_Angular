import { TestBed } from '@angular/core/testing';
import {
  HttpTestingController,
  provideHttpClientTesting
} from '@angular/common/http/testing';
import {
  HttpClient,
  provideHttpClient,
  withInterceptors
} from '@angular/common/http';
import { RouterTestingModule } from '@angular/router/testing';
import { authInterceptor } from '../interceptors/auth.interceptor';
import { AuthService } from '../services/auth.service';

describe('authInterceptor', () => {

  let http: HttpClient;
  let httpMock: HttpTestingController;
  let authService: AuthService;

  //  Setup 
  beforeEach(() => {

    // Configure testing module with interceptor + http testing
    TestBed.configureTestingModule({
      imports: [RouterTestingModule],
      providers: [
        AuthService,

        // Register HttpClient with interceptor
        provideHttpClient(
          withInterceptors([authInterceptor])
        ),

        // Enable HTTP testing
        provideHttpClientTesting()
      ]
    });

    // Inject dependencies
    http        = TestBed.inject(HttpClient);
    httpMock    = TestBed.inject(HttpTestingController);
    authService = TestBed.inject(AuthService);

    // Clear localStorage before each test
    localStorage.clear();
  });

  //  Cleanup 
  afterEach(() => {
    httpMock.verify();
    localStorage.clear();
  });

  //  Token Attachment 
  it('should attach Bearer token when token exists', () => {

    // Add token to localStorage
    localStorage.setItem('shopez_token', 'my-jwt');

    // Make API call
    http.get('/api/test').subscribe();

    // Intercept request
    const req = httpMock.expectOne('/api/test');

    // Check Authorization header
    expect(req.request.headers.has('Authorization'))
      .toBeTrue();

    expect(req.request.headers.get('Authorization'))
      .toBe('Bearer my-jwt');

    // Return mock response
    req.flush({});
  });

  //  No token scenario 
  it('should NOT attach header when no token', () => {

    // No token in localStorage
    http.get('/api/test').subscribe();

    const req = httpMock.expectOne('/api/test');

    // Header should NOT exist
    expect(req.request.headers.has('Authorization'))
      .toBeFalse();

    req.flush({});
  });

  //  POST request test 
  it('should attach token to POST requests', () => {

    localStorage.setItem('shopez_token', 'my-jwt');

    const body = { name: 'Test' };

    http.post('/api/products', body).subscribe();

    const req = httpMock.expectOne('/api/products');

    // Check method and header
    expect(req.request.method).toBe('POST');
    expect(req.request.headers.get('Authorization'))
      .toBe('Bearer my-jwt');

    // Check body
    expect(req.request.body).toEqual(body);

    req.flush({});
  });

  //  PUT request test 
  it('should attach token to PUT requests', () => {

    localStorage.setItem('shopez_token', 'my-jwt');

    http.put('/api/products/1', {}).subscribe();

    const req = httpMock.expectOne('/api/products/1');

    expect(req.request.method).toBe('PUT');
    expect(req.request.headers.get('Authorization'))
      .toBe('Bearer my-jwt');

    req.flush({});
  });

  //  DELETE request test 
  it('should attach token to DELETE requests', () => {

    localStorage.setItem('shopez_token', 'my-jwt');

    http.delete('/api/cart/1').subscribe();

    const req = httpMock.expectOne('/api/cart/1');

    expect(req.request.method).toBe('DELETE');
    expect(req.request.headers.get('Authorization'))
      .toBe('Bearer my-jwt');

    req.flush({});
  });

  //  URL should remain unchanged 
  it('should not change the request URL', () => {

    localStorage.setItem('shopez_token', 'my-jwt');

    http.get('/api/products').subscribe();

    const req = httpMock.expectOne('/api/products');

    // Ensure URL is same
    expect(req.request.url).toBe('/api/products');

    req.flush([]);
  });

  //  Handle 401 Unauthorized 
  it('should call authService.logout() on 401 error', () => {

    localStorage.setItem('shopez_token', 'expired-token');

    // Spy on logout method
    const logoutSpy = spyOn(authService, 'logout');

    http.get('/api/test').subscribe({
      error: () => {

        // Expect logout called
        expect(logoutSpy).toHaveBeenCalled();
      }
    });

    // Simulate 401 response
    httpMock.expectOne('/api/test').flush(
      { message: 'Unauthorized' },
      { status: 401, statusText: 'Unauthorized' }
    );
  });

  //  Handle 403 Forbidden 
  it('should propagate 403 error', () => {

    localStorage.setItem('shopez_token', 'customer-token');

    http.get('/api/admin').subscribe({
      error: err => {

        // Expect error to pass through
        expect(err.status).toBe(403);
      }
    });

    httpMock.expectOne('/api/admin').flush(
      { message: 'Forbidden' },
      { status: 403, statusText: 'Forbidden' }
    );
  });

});