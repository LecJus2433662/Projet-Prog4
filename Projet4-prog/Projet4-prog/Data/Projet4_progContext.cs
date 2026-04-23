using Microsoft.EntityFrameworkCore;
using Projet4_prog.Data;
using Projet4_prog.Data.SeedConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projet4_prog.Data
{
    
}
public class Projet4_progContext(DbContextOptions<Projet4_progContext> options) :
DbContext(options)
{
    public DbSet<Produit> Produits { get; set; } = default!;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfiguration(new ProduitConfiguration());
    }
}