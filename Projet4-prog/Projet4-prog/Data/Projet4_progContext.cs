using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Projet4_prog.Data
{
    public class Projet4_progContext : DbContext
    {
        public Projet4_progContext (DbContextOptions<Projet4_progContext> options)
            : base(options)
        {
        }

        public DbSet<Projet4_prog.Data.Produit> Produit { get; set; } = default!;
    }
}
