using System.ComponentModel.DataAnnotations;

namespace Projet4_prog.DTO.Auth
{
    public class InscriptionDto
    {
        [Required]
        [MaxLength(50)]
        public string NomUtilisateur { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Courriel { get; set; } = string.Empty;

        [Required]
        [MinLength(6)]
        public string MotDePasse { get; set; } = string.Empty;
    }
}
