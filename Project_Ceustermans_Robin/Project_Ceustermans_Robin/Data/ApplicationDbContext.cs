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

            //Land
            modelBuilder.Entity<Land>().ToTable("Land");

            //Categorie
            modelBuilder.Entity<Categorie>().ToTable("Categorie");

            //Merk
            modelBuilder.Entity<Merk>().ToTable("Merk");

            //VerzamelObject
            modelBuilder.Entity<VerzamelObject>().ToTable("VerzamelObject");

            //MedeEigenaar
            modelBuilder.Entity<MedeEigenaar>().ToTable("Mede_Eigenaar");

            //MedeEigenaarObject
            modelBuilder.Entity<MedeEigenaarObject>()
                .HasKey(p => new { p.MedeEigenaarID, p.ObjectID });

            modelBuilder.Entity<MedeEigenaarObject>()
                .HasOne(m => m.MedeEigenaar)
                .WithMany(mo => mo.medeEigenaarObjecten)
                .HasForeignKey(m => m.MedeEigenaarID);

            modelBuilder.Entity<MedeEigenaarObject>()
                .HasOne(v => v.VerzamelObject)
                .WithMany(mo => mo.medeEigenaarObjecten)
                .HasForeignKey(v => v.ObjectID);
              

        }

    }
}
