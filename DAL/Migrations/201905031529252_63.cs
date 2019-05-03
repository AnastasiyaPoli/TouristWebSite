namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _63 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConstructedTours", "CountryId", c => c.Long(nullable: true));
            AddColumn("dbo.ConstructedTours", "CityId", c => c.Long(nullable: true));
            CreateIndex("dbo.ConstructedTours", "CountryId");
            CreateIndex("dbo.ConstructedTours", "CityId");
            AddForeignKey("dbo.ConstructedTours", "CityId", "dbo.Cities", "Id", cascadeDelete: false);
            AddForeignKey("dbo.ConstructedTours", "CountryId", "dbo.Countries", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ConstructedTours", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.ConstructedTours", "CityId", "dbo.Cities");
            DropIndex("dbo.ConstructedTours", new[] { "CityId" });
            DropIndex("dbo.ConstructedTours", new[] { "CountryId" });
            DropColumn("dbo.ConstructedTours", "CityId");
            DropColumn("dbo.ConstructedTours", "CountryId");
        }
    }
}
