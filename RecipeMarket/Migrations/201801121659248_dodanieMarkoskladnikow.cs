namespace RecipeMarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dodanieMarkoskladnikow : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Produkty", "Makroskladniki", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Produkty", "Makroskladniki");
        }
    }
}
