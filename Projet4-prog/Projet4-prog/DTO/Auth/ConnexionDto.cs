using System.ComponentModel.DataAnnotations;

namespace Projet4_prog.DTO.Auth
{
    public class ConnexionDto
    {
        [Required]
        public string NomUtilisateur { get; set; } = string.Empty;

        [Required]
        public string MotDePasse { get; set; } = string.Empty;
    }
}
