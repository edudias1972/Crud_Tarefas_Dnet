import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private api = 'http://localhost:5000/api/auth';

  constructor(private http: HttpClient) {}

  login(email: string, senha: string) {
  return this.http.post<any>('http://localhost:5000/api/auth/login', {
    email: email,
    senha: senha
  });
}

  saveToken(token: string) {
    localStorage.setItem('token', token);
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  isLogged(): boolean {
    return !!this.getToken();
  }

  logout() {
    localStorage.removeItem('token');
  }

  getPayload(): any {
    const token = this.getToken();

    if (!token) return null;

    return JSON.parse(atob(token.split('.')[1]));
  }

  getUserEmail(): string {
    return this.getPayload()?.email ?? '';
  }

  getUserRole(): string {
    return this.getPayload()?.role ?? '';
  }
}
