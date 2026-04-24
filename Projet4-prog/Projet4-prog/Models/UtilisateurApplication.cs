using Microsoft.AspNetCore.Identity;

namespace Projet4_prog.Models
{
    public class UtilisateurApplication : IdentityUser
    {
        public ICollection<Commande> Commandes { get; set; } = new List<Commande>();
    }
}