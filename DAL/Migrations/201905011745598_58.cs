namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _58 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConstructedTours", "Price", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ConstructedTours", "Price");
        }
    }
}
