import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PersonaService } from '../../shared/services/persona.service';
import { PersonaNatural } from '../../shared/models/persona-natural';
import { Telefono } from '../../shared/models/telefono';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-personas-naturales',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './personas-naturales.component.html',
  styleUrls: ['./personas-naturales.component.css'],
})
export class PersonasNaturalesComponent {
  filtro = '';
  loading = false;
  error = '';
  formVisible = false;

  lista: PersonaNatural[] = [];
  form: PersonaNatural = this.nuevoForm();
  telefonos: Telefono[] = [];

  tiposTelefono: string[] = ['Móvil', 'Fijo', 'WhatsApp', 'Trabajo'];

  constructor(private api: PersonaService) {}
  ngOnInit() { this.listar(); }

  listar() {
    this.loading = true; this.error = '';
    this.api.getNaturales(this.filtro).subscribe({
      next: (r) => { this.lista = r ?? []; },
      error: (e) => { this.error = 'No se pudo cargar la lista.'; console.error(e); },
      complete: () => this.loading = false
    });
  }

  nuevo() {
    this.form = this.nuevoForm();
    this.telefonos = [{ numero: '', tipoTelefono: 'Móvil' }];
    this.formVisible = true;
  }

  editar(p: PersonaNatural) {
    if (!p.personaId) return;
    this.form = { ...p };
    this.api.getNaturalById(p.personaId).subscribe({
      next: (det: PersonaNatural & { telefonos?: Telefono[] }) => {
        this.telefonos = (det.telefonos ?? []).map(t => ({
          numero: t.numero?.trim() ?? '',
          tipoTelefono: (t.tipoTelefono && t.tipoTelefono.trim()) || 'Móvil'
        }));
        this.formVisible = true;
      },
      error: (e) => { this.error = 'No se pudo cargar el detalle.'; console.error(e); }
    });
  }

  guardar() {
    this.error = '';
    const payload = this.sanitizarPayload();

    let req$: Observable<any>;
    if (this.form.personaId) {
      req$ = this.api.updateNatural(this.form.personaId, payload);
    } else {
      req$ = this.api.createNatural(payload);
    }

    req$.subscribe({
      next: () => { this.formVisible = false; this.listar(); },
      error: (e) => {
        console.error(e);
        this.error = 'No se pudo guardar.';
      }
    });
  }

  eliminar(id?: number) {
    if (!id) return;
    if (!confirm('¿Eliminar registro?')) return;
    this.api.deleteNatural(id).subscribe({
      next: () => this.listar(),
      error: (e) => { this.error = 'No se pudo eliminar.'; console.error(e); }
    });
  }

  cancelar() { this.formVisible = false; }

  addTelefono() { this.telefonos.push({ numero: '', tipoTelefono: 'Móvil' }); }
  removeTelefono(i: number) { this.telefonos.splice(i, 1); }

  trackByPersona = (_: number, it: PersonaNatural) => it.personaId ?? -1;

  private nuevoForm(): PersonaNatural {
    return {
      tipoDocumento: 'DNI',
      numeroDocumento: '',
      nombres: '',
      apellidoPaterno: '',
      apellidoMaterno: '',
      edad: 18,
      sexo: 'M',
      email: null,            // tu API acepta null para estos campos opcionales
      fechaNacimiento: null,  // idem
    };
  }

  private sanitizarPayload(): PersonaNatural {
    // Limpia/normaliza teléfonos:
    // - número siempre string sin espacios
    // - tipoTelefono: string o undefined (NUNCA null) para coincidir con la interfaz
    const telefonosLimpios: Telefono[] = (this.telefonos || [])
      .map(t => ({
        numero: (t.numero ?? '').trim(),
        tipoTelefono: (t.tipoTelefono && t.tipoTelefono.trim()) || undefined
      }))
      .filter(t => t.numero.length > 0);

    return {
      personaId: this.form.personaId,
      tipoDocumento: (this.form.tipoDocumento ?? '').trim(),
      numeroDocumento: (this.form.numeroDocumento ?? '').trim(),
      nombres: (this.form.nombres ?? '').trim(),
      apellidoPaterno: (this.form.apellidoPaterno ?? '').trim(),
      apellidoMaterno: (this.form.apellidoMaterno ?? '').trim(),
      edad: Number(this.form.edad ?? 0),
      sexo: (this.form.sexo ?? 'M'),
      // Mantén null para email/fechaNacimiento si están vacíos, según tu API
      email: (this.form.email && this.form.email.trim()) ? this.form.email.trim() : null,
      fechaNacimiento: (this.form.fechaNacimiento && `${this.form.fechaNacimiento}`.trim())
        ? this.form.fechaNacimiento
        : null,
      telefonos: telefonosLimpios.length ? telefonosLimpios : undefined,
    };
  }
}
