import { Component } from '@angular/core';
import { AuthService } from '../../services/auth';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.html'
})
export class LoginComponent {

  email = '';
  senha = '';

  constructor(
    private auth: AuthService,
    private router: Router
  ) {}

  entrar() {
    this.auth.login(this.email, this.senha)
      .subscribe(res => {
        this.auth.salvarToken(res.token);
        this.router.navigate(['/tarefas']);
      });
  }
}
