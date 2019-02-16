namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _51 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hotels", "PriceStandart", c => c.Long(nullable: false));
            DropColumn("dbo.Hotels", "PriceStandatr");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Hotels", "PriceStandatr", c => c.Long(nullable: false));
            DropColumn("dbo.Hotels", "PriceStandart");
        }
    }
}
