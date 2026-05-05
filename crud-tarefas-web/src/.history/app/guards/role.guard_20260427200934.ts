import { CanActivateFn, ActivatedRouteSnapshot, Router } from '@angular/router';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';

export const roleGuard: CanActivateFn = (route: ActivatedRouteSnapshot) => {
  const auth = inject(AuthService);
  const router = inject(Router);

  const expectedRoles = route.data['roles'] as string[];

  if (!auth.isLogged()) {
    router.navigate(['/login']);
    return false;
  }

  const userRole = auth.getUserRole();

  if (expectedRoles.includes(userRole)) {
    return true;
  }

  router.navigate(['/login']);
  return false;
};
