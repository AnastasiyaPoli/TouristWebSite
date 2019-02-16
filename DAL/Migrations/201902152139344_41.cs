namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _41 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hotels", "Price", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Hotels", "Price");
        }
    }
}
