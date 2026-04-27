import { CanActivateFn, Router } from '@angular/router';
import { inject } from '@angular/core';
import { AuthService } from '../services/auth';
import { Injectable } from '@angular/core';
i

export const authGuard: CanActivateFn = () => {
  return !!localStorage.getItem('token');
};
@Injectable({
  providedIn: 'root'
})

export class AuthService {

  login() {
    localStorage.setItem('token', 'true');
  }

  logout() {
    localStorage.removeItem('token');
  }

  isLogged() {
    return !!localStorage.getItem('token');
  }
}
