namespace CleanArchitecture.Application.Models.Identity
{
    public class JwtSettings
    {
        public string Key { get; set; } = string.Empty;
        
        // representa quien envia el token a los clientes
        public string Issuer{ get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty; //es la audiencia
        public double DurationInMinutes { get; set; }
    }
}
