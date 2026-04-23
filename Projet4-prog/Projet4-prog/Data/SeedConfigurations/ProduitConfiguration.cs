using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
                    image = "https://www.instagram.com/reel/DPHjzjGEixS/"},
                new Produit { 
                    Id = 2,
                    Nom = "Pudding chomeur",
                    Description = "c'est tellement bon pour mathis",
                    Prix = 16.67,
                    NbProduitRestant = 1,
                    image = "https://www.allrecipes.com/recipe/267358/pouding-chomeur/"
                },
                new Produit {
                    Id = 3,
                    Nom = "Tire d'érable",
                    Description = "Le sirop d'érable chauffé est versé dans la neige pour créer la tire. · Versez le sirop d'érable dans une neige compactée",
                    Prix = 1.67,
                    NbProduitRestant = 80,
                    image = "https://www.noovomoi.ca/cuisiner/trucs-et-inspirations/article.etapes-tire-erable-maison.1.618708.html"
                },
                 new Produit
                 {
                     Id = 4,
                     Nom = "3 Crèpes",
                     Description = "Des pitas mais sucrée",
                     Prix = 8.67,
                     NbProduitRestant = 8,
                     image = "https://www.noovomoi.ca/cuisiner/trucs-et-inspirations/article.etapes-tire-erable-maison.1.618708.html"
                 }


                );
        }
    }
}

