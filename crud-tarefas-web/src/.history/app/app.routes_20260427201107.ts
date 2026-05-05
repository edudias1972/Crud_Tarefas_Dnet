import { Routes } from '@angular/router';

import { LoginComponent } from './pages/login/login.component';
import { TarefasComponent } from './pages/tarefas/tarefas.component';

import { roleGuard } from './guards/role.guard';

export const routes: Routes = [

  { path: '', redirectTo: 'login', pathMatch: 'full' },

  { path: 'login', component: LoginComponent },

  {
    path: 'tarefas',
    component: TarefasComponent,
    canActivate: [roleGuard],
    data: { roles: ['Admin', 'User'] }
  },

  {
    path: 'admin',
    component: TarefasComponent,
    canActivate: [roleGuard],
    data: { roles: ['Admin'] }
  }
];
