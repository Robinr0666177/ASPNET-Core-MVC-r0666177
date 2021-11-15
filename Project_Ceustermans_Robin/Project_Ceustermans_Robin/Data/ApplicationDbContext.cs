using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Project_Ceustermans_Robin.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Project_Ceustermans_Robin.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Land> Landen { get; set; }
        public DbSet<Merk> Merken { get; set; }
        public DbSet<Categorie> Categories { get; set; }
        public DbSet<VerzamelObject> VerzamelObjecten { get; set; }
        public DbSet<MedeEigenaar> MedeEigenaaren { get; set; }
        public DbSet<MedeEigenaarObject> MedeEigenaarObjecten { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Land>().ToTable("Land");
            modelBuilder.Entity<Land>().Property(p => p.Beschrijving).IsRequired();


        }

    }
}
