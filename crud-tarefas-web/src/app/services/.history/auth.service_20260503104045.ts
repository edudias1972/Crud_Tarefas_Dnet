import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { tap } from 'rxjs/operators';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  // 🔐 Login
  login(credentials: { email: string; senha: string }) {
    return this.http
      .post<{ token: string; role: string; email: string }>(
        `${this.apiUrl}/Auth/login`, // rota correta
        credentials
      )
      .pipe(
        tap((res) => {
          localStorage.setItem('token', res.token);
          localStorage.setItem('role', res.role);
          localStorage.setItem('email', res.email);
        })
      );
  }

  // 🔑 Token
  getToken(): string | null {
    return localStorage.getItem('token');
  }

  saveToken(token: string) {
    localStorage.setItem('token', token);
  }

  logout(): void {
    localStorage.removeItem('token');
    localStorage.removeItem('role');
    localStorage.removeItem('email');
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  // 📜 Decodificação JWT
  private decodeToken(): any {
    const token = this.getToken();
    if (!token) return null;
    try {
      return JSON.parse(atob(token.split('.')[1]));
    } catch {
      return null;
    }
  }

  // 👤 Role do usuário
  getUserRole(): string {
    // Primeiro tenta pegar do localStorage
    const role = localStorage.getItem('role');
    if (role) return role;

    // Se não tiver, tenta decodificar o JWT
    const payload = this.decodeToken();
    return (
      payload?.['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] ??
      ''
    );
  }

  // 📧 Email do usuário
  getUserEmail(): string {
    return localStorage.getItem('email') ?? '';
  }
}

