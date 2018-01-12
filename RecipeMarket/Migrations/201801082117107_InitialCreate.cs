namespace RecipeMarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Kategorie",
                c => new
                    {
                        KategoriaID = c.Int(nullable: false, identity: true),
                        NazwaKategorii = c.String(nullable: false),
                        OpisKategorii = c.String(nullable: false),
                        StatusAkceptacji = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.KategoriaID);
            
            CreateTable(
                "dbo.Przepisy",
                c => new
                    {
                        PrzepisID = c.Int(nullable: false, identity: true),
                        NazwaPrzepisu = c.String(nullable: false),
                        TrescPrzepisu = c.String(nullable: false),
                        Ocena = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LiczbaOcen = c.Int(nullable: false),
                        CzasPrzygotowania = c.Time(nullable: false, precision: 7),
                        StopienTrudnosci = c.Int(nullable: false),
                        IsPremium = c.Boolean(nullable: false),
                        DataDodania = c.DateTime(nullable: false),
                        StatusAkceptacji = c.Int(nullable: false),
                        UzytkownikID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.PrzepisID)
                .ForeignKey("dbo.AspNetUsers", t => t.UzytkownikID)
                .Index(t => t.UzytkownikID);
            
            CreateTable(
                "dbo.Komentarze",
                c => new
                    {
                        KomentarzID = c.Int(nullable: false, identity: true),
                        TrescKomentarza = c.String(nullable: false),
                        DataDodania = c.DateTime(nullable: false),
                        CzasPrzygotowaniaOK = c.Boolean(nullable: false),
                        StopienTrudnosciOK = c.Boolean(nullable: false),
                        Ocena = c.Int(nullable: false),
                        PrzepisID = c.Int(nullable: false),
                        UzytkownikID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.KomentarzID)
                .ForeignKey("dbo.Przepisy", t => t.PrzepisID, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UzytkownikID)
                .Index(t => t.PrzepisID)
                .Index(t => t.UzytkownikID);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        PunktyWRankingu = c.Int(nullable: false),
                        Imie = c.String(),
                        Nazwisko = c.String(),
                        OstatniaAktywnosc = c.DateTime(nullable: false),
                        Ranga = c.Int(nullable: false),
                        StanKonta = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DataRejestracji = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Skladniki",
                c => new
                    {
                        PrzepisID = c.Int(nullable: false),
                        ProduktID = c.Int(nullable: false),
                        Ilosc = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => new { t.PrzepisID, t.ProduktID })
                .ForeignKey("dbo.Produkty", t => t.ProduktID, cascadeDelete: true)
                .ForeignKey("dbo.Przepisy", t => t.PrzepisID, cascadeDelete: true)
                .Index(t => t.PrzepisID)
                .Index(t => t.ProduktID);
            
            CreateTable(
                "dbo.Produkty",
                c => new
                    {
                        ProduktID = c.Int(nullable: false, identity: true),
                        NazwaProduktu = c.String(nullable: false),
                        Kalorie = c.Int(nullable: false),
                        Bialko = c.Int(nullable: false),
                        Weglowodany = c.Int(nullable: false),
                        Tluszcz = c.Int(nullable: false),
                        StatusAkceptacji = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ProduktID);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.PrzepisKategoria",
                c => new
                    {
                        Przepis_PrzepisID = c.Int(nullable: false),
                        Kategoria_KategoriaID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Przepis_PrzepisID, t.Kategoria_KategoriaID })
                .ForeignKey("dbo.Przepisy", t => t.Przepis_PrzepisID, cascadeDelete: true)
                .ForeignKey("dbo.Kategorie", t => t.Kategoria_KategoriaID, cascadeDelete: true)
                .Index(t => t.Przepis_PrzepisID)
                .Index(t => t.Kategoria_KategoriaID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Skladniki", "PrzepisID", "dbo.Przepisy");
            DropForeignKey("dbo.Skladniki", "ProduktID", "dbo.Produkty");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Przepisy", "UzytkownikID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Komentarze", "UzytkownikID", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Komentarze", "PrzepisID", "dbo.Przepisy");
            DropForeignKey("dbo.PrzepisKategoria", "Kategoria_KategoriaID", "dbo.Kategorie");
            DropForeignKey("dbo.PrzepisKategoria", "Przepis_PrzepisID", "dbo.Przepisy");
            DropIndex("dbo.PrzepisKategoria", new[] { "Kategoria_KategoriaID" });
            DropIndex("dbo.PrzepisKategoria", new[] { "Przepis_PrzepisID" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Skladniki", new[] { "ProduktID" });
            DropIndex("dbo.Skladniki", new[] { "PrzepisID" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Komentarze", new[] { "UzytkownikID" });
            DropIndex("dbo.Komentarze", new[] { "PrzepisID" });
            DropIndex("dbo.Przepisy", new[] { "UzytkownikID" });
            DropTable("dbo.PrzepisKategoria");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Produkty");
            DropTable("dbo.Skladniki");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Komentarze");
            DropTable("dbo.Przepisy");
            DropTable("dbo.Kategorie");
        }
    }
}
