import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';
import { map, take } from 'rxjs';

// adminGuard — allows only Admin role, redirects others to /home
export const adminGuard: CanActivateFn = () => {
  const auth   = inject(AuthService);
  const router = inject(Router);
  return auth.currentUser.pipe(
    take(1),
    map(role => {
      if (role === 'Admin') return true;
      router.navigate(['/home']);
      return false;
    })
  );
};
