namespace Models
{
    public class PersonaJuridica
    {
        public int PersonaJuridicaId { get; set; }
        public string RazonSocial { get; set; } = string.Empty;
        public string TipoDocumento { get; set; } = string.Empty;
        public string NumeroDocumento { get; set; } = string.Empty; 
        public DateTime FechaRegistro { get; set; }
    }
}