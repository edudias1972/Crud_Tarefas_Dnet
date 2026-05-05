import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';

import { AuthService } from '../../services/auth.service';
import { TarefasService } from '../../services/tarefas.service';

import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-tarefas',
  standalone: true,
  templateUrl: './tarefas.component.html',
  styleUrls: ['./tarefas.component.css'],
  imports: [
    CommonModule,
    FormsModule,
    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
  ],
})
export class TarefasComponent {
  tarefas: any[] = [];

  usuario = '';
  role = '';

  filtroData = '';

  tarefaForm = {
    id: null as number | null,
    titulo: '',
    descricao: '',
    concluida: false,
    dataHora: '',
  };

  editando = false;

  constructor(
    private auth: AuthService,
    private router: Router,
    private service: TarefasService
  ) {}

  ngOnInit() {
    this.usuario = this.auth.getUserEmail();
    this.role = this.auth.getUserRole();
    this.carregarTarefas();
  }

  // 📥 CARREGAR + ORDENAR
  carregarTarefas() {
    this.service.getAll().subscribe({
      next: (data) => {
        this.tarefas = data.sort(
          (a: any, b: any) =>
            new Date(b.criadaEm).getTime() -
            new Date(a.criadaEm).getTime()
        );
      },
      error: () => {
        this.auth.logout();
        this.router.navigate(['/login']);
      },
    });
  }

  // ➕ CRIAR
  adicionarTarefa() {
    if (!this.tarefaForm.titulo) return;

    const nova = {
      ...this.tarefaForm,
      concluida: false,
      criadaEm: new Date().toISOString(),
    };

    this.service.create(nova).subscribe({
      next: () => {
        this.resetForm();
        this.carregarTarefas();
      },
    });
  }

  // ✏️ EDITAR (ABRIR)
  editarTarefa(t: any) {
    this.tarefaForm = { ...t };
    this.editando = true;
  }

  // 💾 SALVAR EDIÇÃO
  salvarEdicao() {
    if (!this.tarefaForm.titulo) return;

    this.service.update(this.tarefaForm.id!, this.tarefaForm).subscribe({
      next: () => {
        this.resetForm();
        this.carregarTarefas();
      },
    });
  }

  cancelarEdicao() {
    this.resetForm();
  }

  // ✔ CONCLUIR
  concluirTarefa(t: any) {
    const atualizada = {
      ...t,
      concluida: true,
      concluidaEm: new Date().toISOString(),
    };

    this.service.update(t.id, atualizada).subscribe({
      next: () => this.carregarTarefas(),
    });
  }

  // 🗑 DELETE
  deletarTarefa(id: number) {
    this.service.delete(id).subscribe({
      next: () => this.carregarTarefas(),
    });
  }

  // 📅 FILTRO POR DATA
  filtrarPorData() {
    this.service.getAll().subscribe((data) => {
      this.tarefas = data.filter((t: any) => {
        if (!this.filtroData) return true;

        return (
          new Date(t.criadaEm)
            .toISOString()
            .split('T')[0] === this.filtroData
        );
      });
    });
  }

  // 📊 KANBAN HELPERS
  tarefasPendentes() {
    return this.tarefas.filter((t) => !t.concluida);
  }

  tarefasConcluidas() {
    return this.tarefas.filter((t) => t.concluida);
  }

  logout() {
    this.auth.logout();
    this.router.navigate(['/login']);
  }

  private resetForm() {
    this.tarefaForm = {
      id: null,
      titulo: '',
      descricao: '',
      concluida: false,
      dataHora: '',
    };
    this.editando = false;
  }
}
