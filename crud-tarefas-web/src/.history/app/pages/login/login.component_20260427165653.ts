import { Component } from '@angular/core';
import { AuthService } from '../../services/auth';
import { CanActivateFn } from '@angular/router';

export const authGuard: CanActivateFn = () => {
  return !!localStorage.getItem('token');
};

@Component({
  selector: 'app-login',
  standalone: true,
  templateUrl: './login.html'
})
export class LoginComponent {

  constructor(private auth: Auth) {}

  entrar() {
    this.auth.login();
  }
}
