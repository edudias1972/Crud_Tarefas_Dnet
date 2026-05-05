import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class TarefasService {
  private apiUrl = `${environment.apiUrl}/tarefas`;

  constructor(private http: HttpClient) {}

  // 📖 READ
  getAll(): Observable<any[]> {
    return this.http.get<any[]>(this.apiUrl);
  }

  // ➕ CREATE
  create(tarefa: { titulo: string; descricao?: string }): Observable<any> {
    return this.http.post<any>(this.apiUrl, tarefa);
  }

  // ❌ DELETE
  delete(id: number): Observable<any> {
    return this.http.delete<any>(`${this.apiUrl}/${id}`);
  }

  // ✏ UPDATE
  update(id: number, tarefa: { titulo: string; descricao?: string; concluida: boolean }): Observable<any> {
    return this.http.put<any>(`${this.apiUrl}/${id}`, tarefa);
  }
}
