import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment';
import { tap } from 'rxjs/operators';
import { isPlatformBrowser } from '@angular/common';
import { inject, PLATFORM_ID } from '@angular/core';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private apiUrl = environment.apiUrl;
  private platformId = inject(PLATFORM_ID);

  constructor(private http: HttpClient) {}

  // 🔐 Login
  login(credentials: { email: string; senha: string }) {
    return this.http
      .post<{ token: string; role: string; email: string }>(
        `${this.apiUrl}/Auth/login`,
        credentials
      )
      .pipe(
        tap((res) => {
          if (isPlatformBrowser(this.platformId)) {
            localStorage.setItem('token', res.token);
            localStorage.setItem('role', res.role);
            localStorage.setItem('email', res.email);
          }
        })
      );
  }

  // 🔑 Token
  getToken(): string | null {
    if (isPlatformBrowser(this.platformId)) {
      return localStorage.getItem('token');
    }
    return null;
  }

  saveToken(token: string) {
    if (isPlatformBrowser(this.platformId)) {
      localStorage.setItem('token', token);
    }
  }

  logout(): void {
    if (isPlatformBrowser(this.platformId)) {
      localStorage.removeItem('token');
      localStorage.removeItem('role');
      localStorage.removeItem('email');
    }
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
    if (isPlatformBrowser(this.platformId)) {
      const role = localStorage.getItem('role');
      if (role) return role;
    }

    const payload = this.decodeToken();

    return (
      payload?.['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'] ??
      ''
    );
  }

  // 📧 Email do usuário
  getUserEmail(): string {
    if (isPlatformBrowser(this.platformId)) {
      return localStorage.getItem('email') ?? '';
    }
    return '';
  }
}
