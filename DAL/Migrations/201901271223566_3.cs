namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Discounts", "IsActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.News", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.News", "IsActive");
            DropColumn("dbo.Discounts", "IsActive");
        }
    }
}
