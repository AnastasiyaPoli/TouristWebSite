namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _61 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ConstructedTours", "CityId", "dbo.Cities");
            DropForeignKey("dbo.ConstructedTours", "CountryId", "dbo.Countries");
            DropIndex("dbo.ConstructedTours", new[] { "CountryId" });
            DropIndex("dbo.ConstructedTours", new[] { "CityId" });
        }
        
        public override void Down()
        {
            CreateIndex("dbo.ConstructedTours", "CityId");
            CreateIndex("dbo.ConstructedTours", "CountryId");
            AddForeignKey("dbo.ConstructedTours", "CountryId", "dbo.Countries", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ConstructedTours", "CityId", "dbo.Cities", "Id", cascadeDelete: true);
        }
    }
}
