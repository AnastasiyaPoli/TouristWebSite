namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _31 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LeavePoints", "CityId", "dbo.Cities");
            DropForeignKey("dbo.LeavePoints", "TransportId", "dbo.Transports");
            DropForeignKey("dbo.Routes", "DestinationPointId", "dbo.DestinationPoints");
            DropForeignKey("dbo.Routes", "LeavePointId", "dbo.LeavePoints");
            DropIndex("dbo.LeavePoints", new[] { "CityId" });
            DropIndex("dbo.LeavePoints", new[] { "TransportId" });
            DropIndex("dbo.Routes", new[] { "LeavePointId" });
            DropIndex("dbo.Routes", new[] { "DestinationPointId" });
            DropTable("dbo.LeavePoints");
            DropTable("dbo.Routes");
            DropTable("dbo.DestinationPoints");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.DestinationPoints",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Routes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        LeavePointId = c.Long(nullable: false),
                        DestinationPointId = c.Long(nullable: false),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LeavePoints",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        CityId = c.Long(nullable: false),
                        TransportId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Routes", "DestinationPointId");
            CreateIndex("dbo.Routes", "LeavePointId");
            CreateIndex("dbo.LeavePoints", "TransportId");
            CreateIndex("dbo.LeavePoints", "CityId");
            AddForeignKey("dbo.Routes", "LeavePointId", "dbo.LeavePoints", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Routes", "DestinationPointId", "dbo.DestinationPoints", "Id", cascadeDelete: true);
            AddForeignKey("dbo.LeavePoints", "TransportId", "dbo.Transports", "Id", cascadeDelete: true);
            AddForeignKey("dbo.LeavePoints", "CityId", "dbo.Cities", "Id", cascadeDelete: true);
        }
    }
}
