namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Discounts", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Discounts", "EndDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.News", "Date", c => c.DateTime(nullable: false));
            DropColumn("dbo.Discounts", "IsActive");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Discounts", "IsActive", c => c.Boolean(nullable: false));
            DropColumn("dbo.News", "Date");
            DropColumn("dbo.Discounts", "EndDate");
            DropColumn("dbo.Discounts", "StartDate");
        }
    }
}
