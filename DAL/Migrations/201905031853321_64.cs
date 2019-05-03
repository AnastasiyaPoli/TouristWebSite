namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _64 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ConstructedTours", "CityId", "dbo.Cities");
            DropForeignKey("dbo.ConstructedTours", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.ConstructedTours", "DestinationCityId", "dbo.DestinationCities");
            DropForeignKey("dbo.ConstructedTours", "DestinationCountryId", "dbo.DestinationCountries");
            DropForeignKey("dbo.ConstructedTours", "DestinationPointId", "dbo.DestinationPoints");
            DropForeignKey("dbo.ConstructedTours", "LeavePointId", "dbo.LeavePoints");
            DropForeignKey("dbo.ConstructedTours", "TransportId", "dbo.Transports");
            DropIndex("dbo.ConstructedTours", new[] { "CountryId" });
            DropIndex("dbo.ConstructedTours", new[] { "CityId" });
            DropIndex("dbo.ConstructedTours", new[] { "TransportId" });
            DropIndex("dbo.ConstructedTours", new[] { "LeavePointId" });
            DropIndex("dbo.ConstructedTours", new[] { "DestinationCountryId" });
            DropIndex("dbo.ConstructedTours", new[] { "DestinationCityId" });
            DropIndex("dbo.ConstructedTours", new[] { "DestinationPointId" });
            DropColumn("dbo.ConstructedTours", "CountryId");
            DropColumn("dbo.ConstructedTours", "CityId");
            DropColumn("dbo.ConstructedTours", "TransportId");
            DropColumn("dbo.ConstructedTours", "LeavePointId");
            DropColumn("dbo.ConstructedTours", "DestinationCountryId");
            DropColumn("dbo.ConstructedTours", "DestinationCityId");
            DropColumn("dbo.ConstructedTours", "DestinationPointId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ConstructedTours", "DestinationPointId", c => c.Long(nullable: false));
            AddColumn("dbo.ConstructedTours", "DestinationCityId", c => c.Long(nullable: false));
            AddColumn("dbo.ConstructedTours", "DestinationCountryId", c => c.Long(nullable: false));
            AddColumn("dbo.ConstructedTours", "LeavePointId", c => c.Long(nullable: false));
            AddColumn("dbo.ConstructedTours", "TransportId", c => c.Long(nullable: false));
            AddColumn("dbo.ConstructedTours", "CityId", c => c.Long(nullable: false));
            AddColumn("dbo.ConstructedTours", "CountryId", c => c.Long(nullable: false));
            CreateIndex("dbo.ConstructedTours", "DestinationPointId");
            CreateIndex("dbo.ConstructedTours", "DestinationCityId");
            CreateIndex("dbo.ConstructedTours", "DestinationCountryId");
            CreateIndex("dbo.ConstructedTours", "LeavePointId");
            CreateIndex("dbo.ConstructedTours", "TransportId");
            CreateIndex("dbo.ConstructedTours", "CityId");
            CreateIndex("dbo.ConstructedTours", "CountryId");
            AddForeignKey("dbo.ConstructedTours", "TransportId", "dbo.Transports", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ConstructedTours", "LeavePointId", "dbo.LeavePoints", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ConstructedTours", "DestinationPointId", "dbo.DestinationPoints", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ConstructedTours", "DestinationCountryId", "dbo.DestinationCountries", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ConstructedTours", "DestinationCityId", "dbo.DestinationCities", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ConstructedTours", "CountryId", "dbo.Countries", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ConstructedTours", "CityId", "dbo.Cities", "Id", cascadeDelete: true);
        }
    }
}
