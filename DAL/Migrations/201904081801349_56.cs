namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _56 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Hotels", "DestinationPointId", "dbo.DestinationPoints");
            DropIndex("dbo.Hotels", new[] { "DestinationPointId" });
            AddColumn("dbo.Hotels", "DestinationCityId", c => c.Long(nullable: false));
            CreateIndex("dbo.Hotels", "DestinationCityId");
            AddForeignKey("dbo.Hotels", "DestinationCityId", "dbo.DestinationCities", "Id", cascadeDelete: true);
            DropColumn("dbo.Hotels", "DestinationPointId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Hotels", "DestinationPointId", c => c.Long(nullable: false));
            DropForeignKey("dbo.Hotels", "DestinationCityId", "dbo.DestinationCities");
            DropIndex("dbo.Hotels", new[] { "DestinationCityId" });
            DropColumn("dbo.Hotels", "DestinationCityId");
            CreateIndex("dbo.Hotels", "DestinationPointId");
            AddForeignKey("dbo.Hotels", "DestinationPointId", "dbo.DestinationPoints", "Id", cascadeDelete: true);
        }
    }
}
