import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TarefasService {

  private api = 'http://localhost:5000/api/tarefas';

  constructor(private http: HttpClient) {}

  getAll() {
    return this.http.get<any[]>(this.api);
  }

  create(tarefa: any) {
    return this.http.post(this.api, tarefa);
  }

  delete(id: number) {
    return this.http.delete(`${this.api}/${id}`);
  }
}
