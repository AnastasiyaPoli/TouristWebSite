namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _62 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ConstructedTours", "CountryId");
            DropColumn("dbo.ConstructedTours", "CityId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ConstructedTours", "CityId", c => c.Long(nullable: true));
            AddColumn("dbo.ConstructedTours", "CountryId", c => c.Long(nullable: true));
        }
    }
}
