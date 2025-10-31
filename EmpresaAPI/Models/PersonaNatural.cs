namespace Models
{
    public class PersonaNatural
    {
        public int PersonaId { get; set; }
        public string TipoDocumento { get; set; } = string.Empty; 
        public string NumeroDocumento { get; set; } = string.Empty; 
        public string Nombres { get; set; } = string.Empty;
        public string ApellidoPaterno { get; set; } = string.Empty; 
        public string ApellidoMaterno { get; set; } = string.Empty; 
        public byte Edad { get; set; }
        public string Sexo { get; set; } = string.Empty; 
        public string? Email { get; set; } 
        public DateTime? FechaNacimiento { get; set; } 
        public DateTime FechaRegistro { get; set; }
    }
}