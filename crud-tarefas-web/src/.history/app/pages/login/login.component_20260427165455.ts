import { Component } from '@angular/core';
import { AuthService } from '../../services/auth';

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
