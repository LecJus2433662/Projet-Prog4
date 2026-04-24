using Projet4_prog.DTO.ItemCommande;
using System.ComponentModel.DataAnnotations;

namespace Projet4_prog.DTO.Commande
{
    public class CommandeCreationDto
    {
        [Required]
        [MinLength(1, ErrorMessage = "La commande doit contenir au moins un item.")]
        public List<ItemCommandeCreationDto> ItemsCommande { get; set; } = new();
    }
}
