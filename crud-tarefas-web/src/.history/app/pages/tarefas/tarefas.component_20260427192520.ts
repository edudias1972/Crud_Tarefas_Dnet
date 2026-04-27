import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TarefasService } from '../../services/tarefas.service';

@Component({
  selector: 'app-tarefas',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './tarefas.html',
})
export class TarefasComponent {
  tarefas: any[] = [];

  constructor(private service: TarefasService) {}
  usuario = '';

  ngOnInit() {
    this.usuario = this.auth.getUserEmail();

    this.service.getAll().subscribe((data) => {
      this.tarefas = data;
    });
  }
}
