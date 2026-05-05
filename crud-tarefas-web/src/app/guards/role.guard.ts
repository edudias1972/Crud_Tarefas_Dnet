import { CanActivateFn, ActivatedRouteSnapshot, Router } from '@angular/router';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth.service';

export const roleGuard: CanActivateFn = (route: ActivatedRouteSnapshot) => {
  const auth = inject(AuthService);
  const router = inject(Router);

  const roles = route.data?.['roles'] as string[] | undefined;

  // 🔐 verifica se está logado
  if (!auth.isLoggedIn()) {
    router.navigate(['/login']);
    return false;
  }

  const userRole = auth.getUserRole();

  // 🚨 verifica permissão
  if (roles && !roles.includes(userRole)) {
    router.navigate(['/forbidden']);
    return false;
  }

  return true;
};
