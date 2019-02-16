namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _53 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Excursions", "Price", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Excursions", "Price");
        }
    }
}
