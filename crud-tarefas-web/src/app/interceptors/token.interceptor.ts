import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { Router } from '@angular/router';
import { catchError, throwError } from 'rxjs';
import { AuthService } from '../services/auth.service';

export const tokenInterceptor: HttpInterceptorFn = (req, next) => {
  const auth = inject(AuthService);
  const router = inject(Router);

  const token = auth.getToken();

  // 🔐 Adiciona token em todas as requisições, exceto login/register
  if (token && !req.url.includes('/Auth/login') && !req.url.includes('/Auth/register')) {
    req = req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`,
      },
    });
  }

  return next(req).pipe(
    catchError((error) => {
      switch (error.status) {
        case 401:
          // Token inválido ou expirado → desloga e volta para login
          auth.logout();
          router.navigate(['/login']);
          break;
        case 403:
          // Acesso negado → rota proibida
          router.navigate(['/forbidden']);
          break;
        case 0:
          // Erro de rede ou CORS
          console.error('Erro de rede ou CORS');
          break;
      }
      return throwError(() => error);
    })
  );
};
