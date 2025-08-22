import { Component, OnInit } from '@angular/core';
import { CommonModule, DatePipe, JsonPipe } from '@angular/common';
import { UsuarioService } from '../../../core/services/usuario.service';
import { Usuario } from '../../../core/models/usuario';

@Component({
  selector: 'app-usuarios-list',
  standalone: true,
  imports: [CommonModule, DatePipe, JsonPipe],   // 👈 IMPORTANTE
  templateUrl: './usuarios-list.component.html',
  styleUrls: ['./usuarios-list.component.scss']
})
export class UsuariosListComponent implements OnInit {
  usuarios: Usuario[] = [];
  loading = true;
  error?: string;

  constructor(private usuariosSvc: UsuarioService) { }

  ngOnInit(): void {
    this.usuariosSvc.getAll().subscribe({
      next: data => {
        console.log('[UsuariosList] mapped data:', data);
        this.usuarios = data;
        this.loading = false;
      },
      error: err => {
        console.error('[UsuariosList] subscribe error:', err);
        this.error = err?.message ?? 'No se pudo cargar Usuarios';
        this.loading = false;
      }
    });
  }
}
