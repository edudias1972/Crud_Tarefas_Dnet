
import { Routes } from '@angular/router';

import { LoginComponent } from './pages/login/login.component';
import { AdminComponent } from './pages/admin/admin.component';
import { TarefasComponent } from './pages/tarefas/tarefas.component';
import { ForbiddenComponent } from './pages/forbidden/forbidden.component';

// ✅ imports corretos (minúsculo + .guard)
import { AuthGuard } from './guards/auth-guard';
import { roleGuard } from './guards/role.guard';

export const routes: Routes = [
  // rota pública
  { path: 'login', component: LoginComponent },

  // 🔐 rota protegida para usuários logados (role User)
  {
    path: 'tarefas',
    component: TarefasComponent,
    canActivate: [AuthGuard, roleGuard],
    data: { roles: ['User'] }
  },

  // 🔐 rota protegida para administradores
  {
    path: 'admin',
    component: AdminComponent,
    canActivate: [AuthGuard, roleGuard],
    data: { roles: ['Admin'] }
  },

  // 🚨 rota de acesso negado
  { path: 'forbidden', component: ForbiddenComponent },

  // rota padrão → redireciona para login
  { path: '', redirectTo: '/login', pathMatch: 'full' },

  // fallback para rotas inválidas
  { path: '**', redirectTo: '/login' }
];

