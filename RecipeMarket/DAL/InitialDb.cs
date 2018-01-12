using RecipeMarket.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Data.Entity.Migrations;
using RecipeMarket.Migrations;

namespace RecipeMarket.DAL
{
    public class InitialDb : MigrateDatabaseToLatestVersion<PrzepisyContext, Configuration>
    {
        public static void SeedPrzepisy(PrzepisyContext context)
        {
            var UserManager = new UserManager<Uzytkownik>(new UserStore<Uzytkownik>(context));
            var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - STWORZENIE ROLI 'ADMIN' - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            string roleNameAdmin = "Admin";

            if (!RoleManager.RoleExists(roleNameAdmin))
            {
                var roleresult = RoleManager.Create(new IdentityRole(roleNameAdmin));
            }

            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - STWORZENIE ROLI 'MOD' - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            string roleNameMod = "Mod";

            if (!RoleManager.RoleExists(roleNameMod))
            {
                var roleresult = RoleManager.Create(new IdentityRole(roleNameMod));
            }

            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - STWORZENIE ROLI 'USER' - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            string roleNameUser = "User";

            if (!RoleManager.RoleExists(roleNameUser))
            {
                var roleresult = RoleManager.Create(new IdentityRole(roleNameUser));
            }


            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - STWORZENIE UŻYTKOWNIKÓW 'ADMIN' - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            var user = new Uzytkownik() { UserName = "admin@przepisy.pl", Email = "graczz.mufc@gmail.com", DataRejestracji = DateTime.Now, OstatniaAktywnosc = DateTime.Now };

            if (UserManager.Create(user, "P@ssw0rd").Succeeded)
            {
                UserManager.AddToRole(user.Id, roleNameAdmin);
            }

            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - STWORZENIE UŻYTKOWNIKÓW 'MOD' - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            user = new Uzytkownik() { UserName = "mod@przepisy.pl", Email = "szewcu222@gmail.com", DataRejestracji = DateTime.Now, OstatniaAktywnosc = DateTime.Now };

            if (UserManager.Create(user, "P@ssw0rd").Succeeded)
            {
                UserManager.AddToRole(user.Id, roleNameMod);
            }

            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - STWORZENIE UŻYTKOWNIKÓW 'USER' - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - 
            List<Uzytkownik> listaUserow = new List<Uzytkownik>() {
                new Uzytkownik() { UserName = "4sobek4@gmail.com", Email = "4sobek4@gmail.com", Imie = "Sebastian", Nazwisko = "Szczepanski", DataRejestracji = DateTime.Now, OstatniaAktywnosc = DateTime.Now },
                new Uzytkownik() { UserName = "laroja_ns9@gmail.com", Email = "laroja_ns9@gmail.com", Imie = "Tomek", Nazwisko = "Miss", DataRejestracji = DateTime.Now, OstatniaAktywnosc = DateTime.Now },
                new Uzytkownik() { UserName = "cwel@gmail.com", Email = "cwel@gmail.com", Imie = "Lukasz", Nazwisko = "Gej", DataRejestracji = DateTime.Now, OstatniaAktywnosc = DateTime.Now },
                new Uzytkownik() { UserName = "didajdisapojntciu@gmail.com", Email = "didajdisapojntciu@gmail.com", Imie = "James", Nazwisko = "Blunt", DataRejestracji = DateTime.Now, OstatniaAktywnosc = DateTime.Now }
            };

            foreach (var us in listaUserow)
            {
                if (UserManager.Create(us, "P@ssw0rd").Succeeded)
                {
                    UserManager.AddToRole(us.Id, roleNameUser);
                }
            }

            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - STWORZENIE PRZYKŁADOWYCH KATEGORII - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            var kategorie = new List<Kategoria>()
            {
                new Kategoria() { NazwaKategorii = "Śniadanie", OpisKategorii = "Pierwszy i według wielu najważniejszy posiłek dnia!",StatusAkceptacji = StatusAkceptacji.Zaakceptowane },
                new Kategoria() { NazwaKategorii = "Obiad", OpisKategorii = "Setki pomysłów na obiad! Dania z makaronu, mięsa i wielu innych!",StatusAkceptacji = StatusAkceptacji.Zaakceptowane },
                new Kategoria() { NazwaKategorii = "Kolacja", OpisKategorii = "Romantyczna kolacja? Sprawdź jak pysznie zakończyć swój dzień!" ,StatusAkceptacji = StatusAkceptacji.Zaakceptowane}
            };
            kategorie.ForEach(k => context.Kategorie.AddOrUpdate(k));
            context.SaveChanges();

            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - STWORZENIE PRZYKŁADOWYCH PRODUKTÓW - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            var produkty = new List<Produkt>()
            {
                new Produkt() { NazwaProduktu = "Jajko", Bialko = 20, Weglowodany = 4, Tluszcz = 17, Kalorie = ((20 * 4) + (4 * 4) + (17 * 9)), StatusAkceptacji = StatusAkceptacji.Zaakceptowane  },
                new Produkt() { NazwaProduktu = "Szynka", Bialko = 18, Weglowodany = 15, Tluszcz = 21, Kalorie = ((18 * 4) + (15 * 4) + (21 * 9)), StatusAkceptacji = StatusAkceptacji.Oczekujace },
                new Produkt() { NazwaProduktu = "Chleb", Bialko = 2, Weglowodany = 38, Tluszcz = 2, Kalorie = ((2 * 4) + (38 * 4) + (2 * 9)), StatusAkceptacji = StatusAkceptacji.Zaakceptowane }

            };
            produkty.ForEach(u => context.Produkty.AddOrUpdate(u));
            context.SaveChanges();

            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - STWORZENIE PRZYKŁADOWYCH PRZEPISÓW - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            var przepisy = new List<Przepis>()// W razie bledu sprawdz dodawanie kategorii .ToList()
            {
                new Przepis() { UzytkownikID = context.Users.SingleOrDefault(u => u.UserName == "4sobek4@gmail.com").Id,NazwaPrzepisu = "Jajecznica", Kategorie = context.Kategorie.Where(k => k.KategoriaID == 1).ToList(), DataDodania = DateTime.Now, TrescPrzepisu = "Wbij jaja i usmaż.",
                    Skladniki = new List<Skladnik>{ new Skladnik{ ProduktID = 1, Ilosc = 4 },
                                                    new Skladnik{ ProduktID = 2, Ilosc = 2 },
                                                    new Skladnik{ ProduktID = 3, Ilosc = 3 }
                    } },
                new Przepis() { UzytkownikID = context.Users.SingleOrDefault(u => u.UserName == "laroja_ns9@gmail.com").Id, NazwaPrzepisu = "Woda", Kategorie = context.Kategorie.Where(k => k.KategoriaID == 2).ToList(), DataDodania = DateTime.Now, TrescPrzepisu = "Ugotuj wodę."},
                new Przepis() { UzytkownikID = context.Users.SingleOrDefault(u => u.UserName == "cwel@gmail.com").Id, NazwaPrzepisu = "Jajo na twardo", Kategorie = context.Kategorie.Where(k => k.KategoriaID == 3).ToList(), DataDodania = DateTime.Now, TrescPrzepisu = "Gotuj jajo przez 7 minut.",  Skladniki = new List<Skladnik>{ new Skladnik{ ProduktID = 1, Ilosc = 4 }}
            } };
            przepisy.ForEach(u => context.Przepisy.AddOrUpdate(u));
            context.SaveChanges();

            // - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - STWORZENIE PRZYKŁADOWYCH Komentarzy - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - - -
            var komentarze = new List<Komentarz>()
            {
                new Komentarz() {UzytkownikID = context.Users.SingleOrDefault(u => u.UserName == "4sobek4@gmail.com").Id, PrzepisID = 1, TrescKomentarza = "Smaczne fest1",DataDodania = DateTime.Now },
                new Komentarz() {UzytkownikID = context.Users.SingleOrDefault(u => u.UserName == "cwel@gmail.com").Id, PrzepisID = 1, TrescKomentarza = "Smaczne fest2" ,DataDodania = DateTime.Now},
                new Komentarz() {UzytkownikID = context.Users.SingleOrDefault(u => u.UserName == "4sobek4@gmail.com").Id, PrzepisID = 1, TrescKomentarza = "Smaczne fest3" ,DataDodania = DateTime.Now}
            };
            komentarze.ForEach(u => context.Komentarze.AddOrUpdate(u));
            context.SaveChanges();
        }
    }
}