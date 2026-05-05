import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  // Login recebe objeto com email e senha
  login(credentials: { email: string; senha: string }) {
    return this.http.post<{ token: string; role: string; email: string }>(
      `${this.apiUrl}/auth/login`,
      {
        email: credentials.email,
        senha: credentials.senha   // ✅ corrigido: usar "senha"
      }
    );
  }

  // Salva token
  saveToken(token: string) {
    localStorage.setItem('token', token);
  }

  // Recupera token
  getToken(): string | null {
    return localStorage.getItem('token');
  }

  // Verifica se está logado
  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  // Faz logout
  logout(): void {
    localStorage.removeItem('token');
  }

  // Extrai payload do JWT
  private decodeToken(): any {
    const token = this.getToken();
    if (!token) return null;
    try {
      return JSON.parse(atob(token.split('.')[1]));
    } catch {
      return null;
    }
  }

  // Retorna role do usuário
  getUserRole(): string {
    const payload = this.decodeToken();
    return payload?.role ?? '';
  }

  // Retorna email do usuário
  getUserEmail(): string {
    const payload = this.decodeToken();
    return payload?.email ?? '';
  }
}
