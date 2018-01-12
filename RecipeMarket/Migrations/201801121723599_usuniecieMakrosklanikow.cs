namespace RecipeMarket.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usuniecieMakrosklanikow : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Produkty", "Makroskladniki");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Produkty", "Makroskladniki", c => c.Int(nullable: false));
        }
    }
}
