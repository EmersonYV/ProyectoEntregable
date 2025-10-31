import { Telefono } from './telefono';

export interface PersonaNatural {
  personaId?: number;
  tipoDocumento: string;
  numeroDocumento: string;
  nombres: string;
  apellidoPaterno: string;
  apellidoMaterno: string;
  edad: number;
  sexo: string;
  email?: string | null;
  fechaNacimiento?: string | null; 
  fechaRegistro?: string;
  telefonos?: Telefono[];       
}
