import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

import { AuthService } from '../../services/auth.service';
import { TarefasService } from '../../services/tarefas.service';

@Component({
  selector: 'app-tarefas',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './tarefas.html',
  styleUrl: './tarefas.css'
})
export class TarefasComponent {

  tarefas: any[] = [];
  usuario = '';
  role = '';

  constructor(
    private auth: AuthService,
    private router: Router,
    private service: TarefasService
  ) {}

  ngOnInit() {

    this.usuario = this.auth.getUserEmail();
    this.role = this.auth.getUserRole();

    this.service.getAll().subscribe(data => {
      this.tarefas = data;
    });
  }

  logout() {
    this.auth.logout();
    this.router.navigate(['/login']);
  }
}
