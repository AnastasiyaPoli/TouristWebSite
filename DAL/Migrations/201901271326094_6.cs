namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Discounts", "Link", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Discounts", "Link");
        }
    }
}
