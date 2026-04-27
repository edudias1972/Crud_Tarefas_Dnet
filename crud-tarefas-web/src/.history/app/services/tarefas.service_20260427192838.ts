import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TarefasService {

  private api = 'http://localhost:5000/api/tarefas';

  constructor(private http: HttpClient) {}

  // 📖 READ
  getAll() {
    return this.http.get<any[]>(this.api);
  }

  // ➕ CREATE
  create(tarefa: any) {
    return this.http.post(this.api, tarefa);
  }

  // ❌ DELETE
  delete(id: number) {
    return this.http.delete(`${this.api}/${id}`);
  }

  // ✏ UPDATE
  update(id: number, tarefa: any) {
    return this.http.put(`${this.api}/${id}`, tarefa);
  }
}
