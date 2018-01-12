namespace RecipeMarket.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using RecipeMarket.DAL;

    public sealed class Configuration : DbMigrationsConfiguration<RecipeMarket.DAL.PrzepisyContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "RecipeMarket.DAL.PrzepisyContext";
        }

        protected override void Seed(RecipeMarket.DAL.PrzepisyContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            InitialDb.SeedPrzepisy(context);
        }
    }
}
