import { Routes } from '@angular/router';
import { PersonasNaturalesComponent } from './features/personas-naturales/personas-naturales.component';
import { PersonasJuridicasComponent } from './features/personas-juridicas/personas-juridicas.component';

export const routes: Routes = [
  { path: '', pathMatch: 'full', redirectTo: 'personas-naturales' },
  { path: 'personas-naturales', component: PersonasNaturalesComponent },
  { path: 'personas-juridicas', component: PersonasJuridicasComponent },
  { path: '**', redirectTo: 'personas-naturales' }
];
