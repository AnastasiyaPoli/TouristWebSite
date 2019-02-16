namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _50 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hotels", "PriceStandatr", c => c.Long(nullable: false));
            AddColumn("dbo.Hotels", "PriceLux", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Hotels", "PriceLux");
            DropColumn("dbo.Hotels", "PriceStandatr");
        }
    }
}
