import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-tarefas',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './tarefas.html'
})
export class TarefasComponent {

  tarefas = [
    { id: 1, titulo: 'Estudar Angular' },
    { id: 2, titulo: 'Integrar API .NET' }
  ];
}
