namespace Projet4_prog.DTO.Auth
{
    public class TokenReponseDto
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
    }
}
