import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { map, take } from 'rxjs';

// authGuard — protects routes that require login
export const authGuard: CanActivateFn = () => {
  const auth   = inject(AuthService);
  const router = inject(Router);
  return auth.isLoggedIn.pipe(
    take(1),
    map(loggedIn => {
      if (loggedIn) return true;
      router.navigate(['/login']);
      return false;
    })
  );
};

// guestGuard — redirects logged-in users away from login/register
export const guestGuard: CanActivateFn = () => {
  const auth   = inject(AuthService);
  const router = inject(Router);
  return auth.isLoggedIn.pipe(
    take(1),
    map(loggedIn => {
      if (!loggedIn) return true;
      router.navigate(['/home']);
      return false;
    })
  );
};
