using Microsoft.AspNet.Identity.EntityFramework;
using RecipeMarket.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace RecipeMarket.DAL
{
    public class PrzepisyContext : IdentityDbContext<Uzytkownik>
    {
        public PrzepisyContext() : base("PrzepisyContext")
        {
        }

        public DbSet<Kategoria> Kategorie { get; set; }
        public DbSet<Komentarz> Komentarze { get; set; }
        public DbSet<Produkt> Produkty { get; set; }
        public DbSet<Przepis> Przepisy { get; set; }
        public DbSet<Skladnik> Skladniki { get; set; }

        public static PrzepisyContext Create()
        {
            return new PrzepisyContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //using System.Data.Entity.ModelConfiguration.Conventionsl
            //Wyłącza konwencje, ktora automatycznie tworzy liczbe mnoga dla nazw tabel w bazie danych
            //Zamiast Kategorie mielibysmy Kategories
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}