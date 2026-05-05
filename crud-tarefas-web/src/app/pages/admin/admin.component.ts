import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';
import { MensagensService } from '../../services/mensagens.service';

@Component({
  selector: 'app-admin',
  standalone: true, // ✅ standalone component
  imports: [CommonModule],
  templateUrl: './admin.component.html'
})
export class AdminComponent {
  constructor(
    private auth: AuthService,
    private mensagens: MensagensService
  ) {}

  get role(): string {
    return this.auth.getUserRole();
  }

  logout(): void {
    this.auth.logout();
    this.mensagens.alerta('Sessão encerrada.');
  }
}

