namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _67 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ConstructedTours", "ExcursionsCount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ConstructedTours", "ExcursionsCount", c => c.Long(nullable: false));
        }
    }
}
