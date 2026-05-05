import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../services/auth.service';


export const AuthGuard: CanActivateFn = (route) => {
  const auth = inject(AuthService);
  const router = inject(Router);
  const roles = route.data?.['roles'] as string[] | undefined;

  if (!auth.isLoggedIn()) {
    auth.logout();
    router.navigate(['/login']);
    return false;
  }

  if (roles && !roles.includes(auth.getUserRole())) {
    router.navigate(['/forbidden']);
    return false;
  }

  return true;
};
