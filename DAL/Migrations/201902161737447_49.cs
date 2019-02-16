namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _49 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BackRoutes", "PriceEconom", c => c.Long(nullable: false));
            AddColumn("dbo.BackRoutes", "PriceBusiness", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.BackRoutes", "PriceBusiness");
            DropColumn("dbo.BackRoutes", "PriceEconom");
        }
    }
}
