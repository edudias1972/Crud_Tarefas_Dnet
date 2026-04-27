import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TarefasService } from '../../services/tarefas.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-tarefas',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './tarefas.html'
})
export class TarefasComponent {

  tarefas: any[] = [];
  usuario = '';

  constructor(
    private service: TarefasService,
    private auth: AuthService
  ) {}

  ngOnInit() {
    this.usuario = this.auth.getToken(); // ou getUserEmail()

    this.service.getAll().subscribe(data => {
      this.tarefas = data;
    });
  }

  logout() {
    this.auth.logout();
  }
}
