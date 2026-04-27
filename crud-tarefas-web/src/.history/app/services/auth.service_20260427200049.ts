import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

interface LoginRequest {
  email: string;
  password: string;
}

interface LoginResponse {
  token: string;
}

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private api = 'http://localhost:5000/api/auth';
  private tokenKey = 'token';

  constructor(private http: HttpClient) {}

  // 🔐 LOGIN
  login(email: string, senha: string): Observable<LoginResponse> {
    const body: LoginRequest = {
      email,
      password: senha
    };

    return this.http.post<LoginResponse>(`${this.api}/login`, body);
  }

  // 💾 SALVAR TOKEN
  saveToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
  }

  // 📦 PEGAR TOKEN
  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  // 🚪 LOGOUT
  logout(): void {
    localStorage.removeItem(this.tokenKey);
  }

  // ✅ LOGADO?
  isLogged(): boolean {
    const token = this.getToken();

    if (!token) return false;

    if (this.isTokenExpired(token)) {
      this.logout();
      return false;
    }

    return true;
  }

  // 👤 EMAIL USUÁRIO
  getUserEmail(): string | null {
    const payload = this.getPayload();

    return (
      payload?.email ||
      payload?.unique_name ||
      payload?.sub ||
      null
    );
  }

  // 🛡 ROLE
  getRole(): string | null {
    const payload = this.getPayload();

    return (
      payload?.role ||
      payload?.roles ||
      null
    );
  }

  // ⏰ TOKEN EXPIRADO?
  isTokenExpired(token?: string): boolean {
    const currentToken = token || this.getToken();

    if (!currentToken) return true;

    try {
      const payload = this.decodeToken(currentToken);

      if (!payload.exp) return true;

      const expiry = payload.exp * 1000;
      return Date.now() > expiry;

    } catch {
      return true;
    }
  }

  // 📦 PAYLOAD JWT
  private getPayload(): any | null {
    const token = this.getToken();

    if (!token) return null;

    try {
      return this.decodeToken(token);
    } catch {
      return null;
    }
  }

  // 🔓 DECODIFICAR JWT
  private decodeToken(token: string): any {
    return JSON.parse(atob(token.split('.')[1]));
  }
}
