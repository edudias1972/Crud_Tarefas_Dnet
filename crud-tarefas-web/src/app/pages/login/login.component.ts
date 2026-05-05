import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';
import { MensagensService } from '../../services/mensagens.service';

// Angular Material
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';

@Component({
  selector: 'app-login',
  standalone: true,
  host: { class: 'login-container' },
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatCardModule
  ],
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  showPassword = false;
  loginForm: FormGroup;
  carregando = false;
  errorMessage: string | null = null;

  constructor(
    private fb: FormBuilder,
    private auth: AuthService,
    private mensagens: MensagensService,
    private router: Router,
  ) {
    this.loginForm = this.fb.group({
      email: ['', [Validators.required, Validators.email]],
      senha: ['', Validators.required],
    });
  }

  togglePassword() {
    this.showPassword = !this.showPassword;
  }

  onSubmit(): void {
    if (this.loginForm.invalid) {
      this.mensagens.alerta('Preencha todos os campos corretamente.');
      return;
    }

    this.carregando = true;
    const { email, senha } = this.loginForm.value;
    const senhaLimpa = senha.trim();

    this.auth.login({ email, senha: senhaLimpa }).subscribe({
      next: (res) => {
        this.auth.saveToken(res.token);
        this.router.navigate(['/tarefas']);
      },
      error: (err) => {
        this.carregando = false;
        if (err.status === 401) {
          this.errorMessage = 'Credenciais inválidas. Verifique email e senha.';
        } else {
          this.errorMessage = 'Erro inesperado ao tentar logar. Tente novamente.';
          console.error('Erro no login:', err);
        }
      },
      complete: () => {
        this.carregando = false;
      }
    });
  }
}
