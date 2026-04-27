import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.html'
})
export class LoginComponent {

  email = '';
  senha = '';

  constructor(private auth: AuthService) {}
   this.auth.login(this.email, this.senha)
  .subscribe(res => {
    this.auth.saveToken(res.token);
    console.log('Login OK');
  });
  entrar() {
    console.log(this.email, this.senha);
    this.auth.login();
  }
}
