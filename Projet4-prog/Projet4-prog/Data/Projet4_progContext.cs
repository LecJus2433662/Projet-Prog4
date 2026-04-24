using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Projet4_prog.Data.SeedConfigurations;
using Projet4_prog.Models;

namespace Projet4_prog.Data
{
    public class Projet4_progContext(DbContextOptions<Projet4_progContext> options) :
        IdentityDbContext<UtilisateurApplication>(options)
    {
        public DbSet<Produit> Produits { get; set; } = default!;
        public DbSet<Commande> Commandes { get; set; } = default!;
        public DbSet<ItemCommande> ItemsCommande { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new ProduitConfiguration());

            // Commande
            modelBuilder.Entity<Commande>(entity =>
            {
                entity.Property(c => c.Statut)
                      .HasConversion<string>();

                entity.HasOne(c => c.Utilisateur)
                      .WithMany(u => u.Commandes)
                      .HasForeignKey(c => c.UtilisateurId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // ItemCommande
            modelBuilder.Entity<ItemCommande>(entity =>
            {
                entity.Property(i => i.PrixUnitaire)
                      .HasColumnType("decimal(18,2)");

                entity.HasOne(i => i.Commande)
                      .WithMany(c => c.ItemsCommande)
                      .HasForeignKey(i => i.CommandeId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(i => i.Produit)
                      .WithMany(p => p.ItemsCommande)
                      .HasForeignKey(i => i.ProduitId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}