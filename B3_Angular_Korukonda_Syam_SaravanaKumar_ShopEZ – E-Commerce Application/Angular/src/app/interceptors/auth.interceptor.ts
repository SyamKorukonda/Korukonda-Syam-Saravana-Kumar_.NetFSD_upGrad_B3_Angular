import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, throwError } from 'rxjs';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

// Functional HTTP Interceptor 
// Auto-attaches Bearer JWT token to every outgoing HTTP request

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);
  const router      = inject(Router);
  const token       = authService.getToken();

  // Clone request and inject Authorization header
  const authReq = token
    ? req.clone({ setHeaders: { Authorization: `Bearer ${token}` } })
    : req;

  return next(authReq).pipe(
    catchError((error: HttpErrorResponse) => {

      // 401 — token expired → logout and redirect to login
      
      if (error.status === 401) {
        authService.logout();
        router.navigate(['/login']);
      }
      // 403 — not Admin → redirect to home
      if (error.status === 403) {
        router.navigate(['/home']);
      }
      return throwError(() => error);
    })
  );
};
