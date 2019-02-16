namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _48 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Routes", "PriceEconom", c => c.Long(nullable: false));
            AddColumn("dbo.Routes", "PriceBusiness", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Routes", "PriceBusiness");
            DropColumn("dbo.Routes", "PriceEconom");
        }
    }
}
