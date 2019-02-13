namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _38 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DestinationPoints", "DestinationCityId", c => c.Long(nullable: false));
            AddColumn("dbo.DestinationPoints", "TransportId", c => c.Long(nullable: false));
            CreateIndex("dbo.DestinationPoints", "DestinationCityId");
            CreateIndex("dbo.DestinationPoints", "TransportId");
            AddForeignKey("dbo.DestinationPoints", "DestinationCityId", "dbo.DestinationCities", "Id", cascadeDelete: true);
            AddForeignKey("dbo.DestinationPoints", "TransportId", "dbo.Transports", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DestinationPoints", "TransportId", "dbo.Transports");
            DropForeignKey("dbo.DestinationPoints", "DestinationCityId", "dbo.DestinationCities");
            DropIndex("dbo.DestinationPoints", new[] { "TransportId" });
            DropIndex("dbo.DestinationPoints", new[] { "DestinationCityId" });
            DropColumn("dbo.DestinationPoints", "TransportId");
            DropColumn("dbo.DestinationPoints", "DestinationCityId");
        }
    }
}
