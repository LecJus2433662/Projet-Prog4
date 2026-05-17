using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Projet4_prog.Models;

namespace Projet4_prog.Data.SeedConfigurations
{
    public class ProduitConfiguration : IEntityTypeConfiguration<Produit>
    {
        public void Configure(EntityTypeBuilder<Produit> builder)
        {
            builder.HasData(

                new Produit
                {
                    Id = 1,
                    Nom = "Bacon halal au sirop d'érable",
                    Description = "bacon infusé au sirop d'érable cuisiner avec l'amour pour Jad",
                    Prix = 12.67,
                    NbProduitRestant = 67,
                    Image = "https://images.unsplash.com/photo-1742859052497-f8bbc8366a32?w=600&auto=format&fit=crop&q=60&ixlib=rb-4.1.0&ixid=M3wxMjA3fDB8MHxzZWFyY2h8Nnx8YmFjb258ZW58MHx8MHx8fDA%3D"
                },
                new Produit
                {
                    Id = 2,
                    Nom = "Pudding chomeur",
                    Description = "c'est tellement bon pour mathis",
                    Prix = 16.67,
                    NbProduitRestant = 67,
                    Image = "https://images.ricardocuisine.com/services/recipes/992x1340_8894.jpg"
                },
                new Produit
                {
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
                },
                new Produit
                {
                    Id = 5,
                    Nom = "Beurre d'érable",
                    Description = "Une tartinade sucrée et crémeuse faite 100% de sirop d'érable pur",
                    Prix = 9.99,
                    NbProduitRestant = 45,
                    Image = "https://images.squarespace-cdn.com/content/v1/5ff4c7c50debdf5d1ff004bc/93f4f384-792c-495c-97b3-fdca6c514eb9/Beurre+d%27%C3%A9rable.png?format=1500w"
                },
                new Produit
                {
                    Id = 6,
                    Nom = "Cornet à l'érable",
                    Description = "Crème glacée à la vanille recouverte de sirop d'érable chaud",
                    Prix = 6.49,
                    NbProduitRestant = 32,
                    Image = "https://cdn.pratico-pratiques.com/app/uploads/sites/3/2018/08/20185844/mini-cornets-a-l-erable.jpeg"
                },
                new Produit
                {
                    Id = 7,
                    Nom = "Biscuits à l'érable",
                    Description = "Biscuits croustillants fourrés avec une crème au sirop d'érable",
                    Prix = 5.99,
                    NbProduitRestant = 78,
                    Image = "https://erableduquebec.ca/uploads/2022/01/recette-biscuits-erable-1200x900-1-600x450.jpg"
                },
                new Produit
                {
                    Id = 8,
                    Nom = "Lait frappé à l'érable",
                    Description = "Un milkshake froid et sucré avec du vrai sirop d'érable du Québec",
                    Prix = 7.25,
                    NbProduitRestant = 21,
                    Image = "https://erableduquebec.ca/uploads/2022/07/recette-lait-frappe-erable-1200x900-1-600x450.jpg"
                }
            );
        }
    }
}

