using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ejemplo.Model;

namespace ejemplo.Data
{   
    public class AppDbContext : IdentityDbContext<ApplicationUser>{
        public AppDbContext(DbContextOptions<AppDbContext> option)
            : base(option){
        }
        public DbSet<Maestro> Maestro { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            base.OnModelCreating(modelBuilder);
            //Maestro
            modelBuilder.Entity<Maestro>()
                .HasKey(maestro => maestro.Id);
        }

    }
}
