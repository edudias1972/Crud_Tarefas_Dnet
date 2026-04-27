import { Injectable } from '@angular/core';
l
ogin(email: string, senha: string) {
  return this.http.post<any>('http://localhost:5000/api/auth/login', {
    email,
    password: senha
  });

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  login() {
    localStorage.setItem('token', 'true');
  }

  logout() {
    localStorage.removeItem('token');
  }

  isLogged() {
    return !!localStorage.getItem('token');
  }
}
