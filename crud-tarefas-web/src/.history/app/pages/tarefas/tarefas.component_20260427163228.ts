import { Component } from '@angular/core';

@Component({
  standalone: true,
  templateUrl: './tarefas.html'
})
export class TarefasComponent {
  tarefas = [
    { id: 1, titulo: 'Estudar Angular' },
    { id: 2, titulo: 'Integrar API .NET' }
  ];
}
