namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _43 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ConstructedTours",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CountryId = c.Long(nullable: false),
                        CityId = c.Long(nullable: false),
                        TransportId = c.Long(nullable: false),
                        LeavePointId = c.Long(nullable: false),
                        DestinationCountryId = c.Long(nullable: false),
                        DestinationCityId = c.Long(nullable: false),
                        DestinationPointId = c.Long(nullable: false),
                        RouteId = c.Long(nullable: false),
                        Class = c.String(),
                        HotelId = c.Long(nullable: false),
                        PeopleCount = c.Long(nullable: false),
                        HotelClass = c.String(),
                        ExcursionsCount = c.Long(nullable: false),
                        Excursion1Id = c.Long(),
                        Excursion2Id = c.Long(),
                        Excursion3Id = c.Long(),
                        Excursion4Id = c.Long(),
                        Excursion5Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Countries", t => t.CityId, cascadeDelete: false)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: false)
                .ForeignKey("dbo.DestinationCities", t => t.DestinationCityId, cascadeDelete: false)
                .ForeignKey("dbo.DestinationCountries", t => t.DestinationCountryId, cascadeDelete: false)
                .ForeignKey("dbo.DestinationPoints", t => t.DestinationPointId, cascadeDelete: false)
                .ForeignKey("dbo.Excursions", t => t.Excursion1Id)
                .ForeignKey("dbo.Excursions", t => t.Excursion2Id)
                .ForeignKey("dbo.Excursions", t => t.Excursion3Id)
                .ForeignKey("dbo.Excursions", t => t.Excursion4Id)
                .ForeignKey("dbo.Excursions", t => t.Excursion5Id)
                .ForeignKey("dbo.Hotels", t => t.HotelId, cascadeDelete: false)
                .ForeignKey("dbo.LeavePoints", t => t.LeavePointId, cascadeDelete: false)
                .ForeignKey("dbo.Routes", t => t.RouteId, cascadeDelete: false)
                .ForeignKey("dbo.Transports", t => t.TransportId, cascadeDelete: false)
                .Index(t => t.CountryId)
                .Index(t => t.CityId)
                .Index(t => t.TransportId)
                .Index(t => t.LeavePointId)
                .Index(t => t.DestinationCountryId)
                .Index(t => t.DestinationCityId)
                .Index(t => t.DestinationPointId)
                .Index(t => t.RouteId)
                .Index(t => t.HotelId)
                .Index(t => t.Excursion1Id)
                .Index(t => t.Excursion2Id)
                .Index(t => t.Excursion3Id)
                .Index(t => t.Excursion4Id)
                .Index(t => t.Excursion5Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ConstructedTours", "TransportId", "dbo.Transports");
            DropForeignKey("dbo.ConstructedTours", "RouteId", "dbo.Routes");
            DropForeignKey("dbo.ConstructedTours", "LeavePointId", "dbo.LeavePoints");
            DropForeignKey("dbo.ConstructedTours", "HotelId", "dbo.Hotels");
            DropForeignKey("dbo.ConstructedTours", "Excursion5Id", "dbo.Excursions");
            DropForeignKey("dbo.ConstructedTours", "Excursion4Id", "dbo.Excursions");
            DropForeignKey("dbo.ConstructedTours", "Excursion3Id", "dbo.Excursions");
            DropForeignKey("dbo.ConstructedTours", "Excursion2Id", "dbo.Excursions");
            DropForeignKey("dbo.ConstructedTours", "Excursion1Id", "dbo.Excursions");
            DropForeignKey("dbo.ConstructedTours", "DestinationPointId", "dbo.DestinationPoints");
            DropForeignKey("dbo.ConstructedTours", "DestinationCountryId", "dbo.DestinationCountries");
            DropForeignKey("dbo.ConstructedTours", "DestinationCityId", "dbo.DestinationCities");
            DropForeignKey("dbo.ConstructedTours", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.ConstructedTours", "CityId", "dbo.Countries");
            DropIndex("dbo.ConstructedTours", new[] { "Excursion5Id" });
            DropIndex("dbo.ConstructedTours", new[] { "Excursion4Id" });
            DropIndex("dbo.ConstructedTours", new[] { "Excursion3Id" });
            DropIndex("dbo.ConstructedTours", new[] { "Excursion2Id" });
            DropIndex("dbo.ConstructedTours", new[] { "Excursion1Id" });
            DropIndex("dbo.ConstructedTours", new[] { "HotelId" });
            DropIndex("dbo.ConstructedTours", new[] { "RouteId" });
            DropIndex("dbo.ConstructedTours", new[] { "DestinationPointId" });
            DropIndex("dbo.ConstructedTours", new[] { "DestinationCityId" });
            DropIndex("dbo.ConstructedTours", new[] { "DestinationCountryId" });
            DropIndex("dbo.ConstructedTours", new[] { "LeavePointId" });
            DropIndex("dbo.ConstructedTours", new[] { "TransportId" });
            DropIndex("dbo.ConstructedTours", new[] { "CityId" });
            DropIndex("dbo.ConstructedTours", new[] { "CountryId" });
            DropTable("dbo.ConstructedTours");
        }
    }
}
