namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _71 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ConstructedTours", "Mark");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ConstructedTours", "Mark", c => c.String());
        }
    }
}
