import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';      
import { FormsModule } from '@angular/forms';       
import { PersonaService } from '../../shared/services/persona.service';
import { PersonaJuridica } from '../../shared/models/persona-juridica';

@Component({
  selector: 'app-personas-juridicas',
  standalone: true,
  templateUrl: './personas-juridicas.component.html',
  styleUrls: ['./personas-juridicas.component.css'],
  imports: [CommonModule, FormsModule]              
})
export class PersonasJuridicasComponent {
  filtro = '';
  lista: PersonaJuridica[] = [];
  loading = false;
  error = '';

  formVisible = false;
  form: PersonaJuridica = this.blank();

  constructor(private api: PersonaService) { this.listar(); }

  private dedupe(items: PersonaJuridica[]) {
    const seen = new Set<number>();
    return items.filter(x => (seen.has(x.personaJuridicaId) ? false : seen.add(x.personaJuridicaId)));
  }

  listar() {
    this.loading = true; this.error = '';
    this.api.getJuridicas(this.filtro).subscribe({
      next: r => this.lista = this.dedupe(r || []),
      error: () => this.error = 'No se pudo cargar la lista.',
      complete: () => this.loading = false
    });
  }

  trackByJuridica = (_: number, j: PersonaJuridica) => j.personaJuridicaId;

  nuevo() { this.form = this.blank(); this.formVisible = true; }
  editar(j: PersonaJuridica) { this.form = { ...j }; this.formVisible = true; }
  cancelar() { this.formVisible = false; }

  guardar() {
    if (this.form.personaJuridicaId) {
      this.api.updateJuridica(this.form.personaJuridicaId, this.form).subscribe(() => { this.formVisible = false; this.listar(); });
    } else {
      this.api.createJuridica(this.form).subscribe(() => { this.formVisible = false; this.listar(); });
    }
  }

  eliminar(id: number) {
    if (!confirm('¿Eliminar registro?')) return;
    this.api.deleteJuridica(id).subscribe(() => this.listar());
  }

  private blank(): PersonaJuridica {
    return {
      personaJuridicaId: 0,
      razonSocial: '',
      tipoDocumento: 'RUC',
      numeroDocumento: '',
      fechaRegistro: undefined as any
    };
  }
}
