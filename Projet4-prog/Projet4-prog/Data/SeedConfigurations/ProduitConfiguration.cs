using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projet4_prog.Models;

namespace Projet4_prog.Data.SeedConfigurations
{
    public class ProduitConfiguration: IEntityTypeConfiguration<Produit>
    {
        public void Configure(EntityTypeBuilder<Produit> builder)
        {
            builder.HasData(

                new Produit { 
                    Id = 1,
                    Nom = "Bacon halal au sirop d'érable",
                    Description= "bacon infusé au sirop d'érable cuisiner avec l'amour pour Jad",
                    Prix = 12.67 ,
                    NbProduitRestant = 67,
                    Image = "https://images.unsplash.com/photo-1742859052497-f8bbc8366a32?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Nnx8YmFjb258ZW58MHx8MHx8fDA%3D"
                },
                new Produit { 
                    Id = 2,
                    Nom = "Pudding chomeur",
                    Description = "c'est tellement bon pour mathis",
                    Prix = 16.67,
                    NbProduitRestant = 67,
                    Image = "https://images.ricardocuisine.com/services/recipes/992x1340_8894.jpg"
                },
                new Produit {
                    Id = 3,
                    Nom = "Tire d'érable",
                    Description = "Le sirop d'érable chauffé est versé dans la neige pour créer la tire. · Versez le sirop d'érable dans une neige compactée",
                    Prix = 1.67,
                    NbProduitRestant = 67,
                    Image = "https://images.radio-canada.ca/q_auto,w_844/v1/alimentation/recette/16x9/2892-tire-erable.jpg"
                },
                new Produit
                {
                    Id = 4,
                    Nom = "3 Crèpes",
                    Description = "Des pitas mais sucrée",
                    Prix = 8.67,
                    NbProduitRestant = 67,
                    Image = "https://img.fourchette-et-bikini.fr/1200x900/2025/03/07/i50846-crepes-sucrees-sans-gluten-et-faciles.webp"
                }


            );
        }
    }
}

