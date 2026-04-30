using Projet4_prog.DTO.Auth;

namespace Projet4_prog.Services
{
    public interface IAuthService
    {
        Task<TokenReponseDto?> ConnexionAsync(ConnexionDto dto);
        Task<bool> InscriptionAsync(InscriptionDto dto);
    }
}