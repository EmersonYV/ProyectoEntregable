import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PersonaNatural } from '../models/persona-natural';
import { PersonaJuridica } from '../models/persona-juridica';

const API_URL = 'http://localhost:5130/api';

@Injectable({ providedIn: 'root' })
export class PersonaService {
  private http = inject(HttpClient);

  // ===== NATURALES =====
  getNaturales(filtro = ''): Observable<PersonaNatural[]> {
    const url = `${API_URL}/PersonaNaturales${filtro ? `?filtro=${encodeURIComponent(filtro)}` : ''}`;
    return this.http.get<PersonaNatural[]>(url);
    // Endpoint esperado: GET /api/PersonaNaturales?filtro=
  }

  getNaturalById(id: number): Observable<PersonaNatural> {
    const url = `${API_URL}/PersonaNaturales/${id}`;
    return this.http.get<PersonaNatural>(url);
    // Endpoint esperado: GET /api/PersonaNaturales/{id}
  }

  createNatural(body: PersonaNatural): Observable<number> {
    const url = `${API_URL}/PersonaNaturales`;
    return this.http.post<number>(url, body);
    // Endpoint esperado: POST /api/PersonaNaturales -> devuelve ID
  }

  updateNatural(id: number, body: PersonaNatural): Observable<void> {
    const url = `${API_URL}/PersonaNaturales/${id}`;
    return this.http.put<void>(url, body);
    // Endpoint esperado: PUT /api/PersonaNaturales/{id}
  }

  deleteNatural(id: number): Observable<void> {
    const url = `${API_URL}/PersonaNaturales/${id}`;
    return this.http.delete<void>(url);
  }

  // ===== JURÍDICAS (por si lo necesitas igualado) =====
  getJuridicas(filtro = ''): Observable<PersonaJuridica[]> {
    const url = `${API_URL}/PersonaJuridicas${filtro ? `?filtro=${encodeURIComponent(filtro)}` : ''}`;
    return this.http.get<PersonaJuridica[]>(url);
  }
  getJuridicaById(id: number): Observable<PersonaJuridica> {
    return this.http.get<PersonaJuridica>(`${API_URL}/PersonaJuridicas/${id}`);
  }
  createJuridica(body: PersonaJuridica): Observable<number> {
    return this.http.post<number>(`${API_URL}/PersonaJuridicas`, body);
  }
  updateJuridica(id: number, body: PersonaJuridica): Observable<void> {
    return this.http.put<void>(`${API_URL}/PersonaJuridicas/${id}`, body);
  }
  deleteJuridica(id: number): Observable<void> {
    return this.http.delete<void>(`${API_URL}/PersonaJuridicas/${id}`);
  }
}
