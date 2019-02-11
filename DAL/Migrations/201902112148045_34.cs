namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _34 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Routes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        LeavePointId = c.Long(nullable: false),
                        DestinationPointId = c.Long(nullable: false),
                        Start = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DestinationPoints", t => t.DestinationPointId, cascadeDelete: true)
                .ForeignKey("dbo.LeavePoints", t => t.LeavePointId, cascadeDelete: true)
                .Index(t => t.LeavePointId)
                .Index(t => t.DestinationPointId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Routes", "LeavePointId", "dbo.LeavePoints");
            DropForeignKey("dbo.Routes", "DestinationPointId", "dbo.DestinationPoints");
            DropIndex("dbo.Routes", new[] { "DestinationPointId" });
            DropIndex("dbo.Routes", new[] { "LeavePointId" });
            DropTable("dbo.Routes");
        }
    }
}
