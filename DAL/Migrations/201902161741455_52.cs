namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _52 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Hotels", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Hotels", "Price", c => c.Int(nullable: false));
        }
    }
}
