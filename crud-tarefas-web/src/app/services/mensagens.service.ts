import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable({ providedIn: 'root' })
export class MensagensService {
  constructor(private snackBar: MatSnackBar) {}

  sucesso(msg: string) {
    this.snackBar.open(msg, 'Fechar', { duration: 3000, panelClass: ['snackbar-sucesso'] });
  }

  erro(msg: string) {
    this.snackBar.open(msg, 'Fechar', { duration: 3000, panelClass: ['snackbar-erro'] });
  }

  alerta(msg: string) {
    this.snackBar.open(msg, 'Fechar', { duration: 3000, panelClass: ['snackbar-alerta'] });
  }
}
