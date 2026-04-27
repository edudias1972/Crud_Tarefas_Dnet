import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
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
      .subscribe({
        next: (res) => {
          this.auth.saveToken(res.token);
          this.router.navigate(['/tarefas']); // 👉 AQUI O FLUXO FECHA
        },
        error: () => {
          alert('Login inválido');
        }
      });
  }
}
