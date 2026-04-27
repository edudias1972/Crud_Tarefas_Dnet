import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { TarefasService } from '../../services/tarefas.service';

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
    private auth: AuthService,
    private router: Router
  ) {}

  ngOnInit() {
    this.usuario = this.auth.getToken();

    this.service.getAll().subscribe(data => {
      this.tarefas = data;
    });
  }

  logout() {
    this.auth.logout();        // remove token
    this.router.navigate(['/login']); // redireciona
  }
}
