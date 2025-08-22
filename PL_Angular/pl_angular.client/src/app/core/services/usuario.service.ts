// src/app/core/services/usuario.service.ts
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map, tap } from 'rxjs';
import { Usuario } from '../models/usuario';

@Injectable({ providedIn: 'root' })
export class UsuarioService {
  private base = '/api/Usuario';

  constructor(private http: HttpClient) { }

  getAll(): Observable<Usuario[]> {
    return this.http.get(`${this.base}/GetAll`, { responseType: 'text' }).pipe(
      tap(txt => console.log('[UsuarioService] raw text:', txt.slice(0, 200))),
      map(txt => {
        // Si llega HTML, es problema de proxy/origen
        if (txt.trim().startsWith('<')) throw new Error('Llegó HTML, no JSON (proxy/origen).');

        // Intenta parsear JSON
        const body = JSON.parse(txt);

        // Si llega array directo
        if (Array.isArray(body)) return body as Usuario[];

        // Si llega envuelto
        const arr =
          body?.objects ?? body?.Objects ??
          body?.data ?? body?.Data ??
          body?.items ?? body?.Items ?? [];
        return Array.isArray(arr) ? (arr as Usuario[]) : [];
      })
    );
  }
}
