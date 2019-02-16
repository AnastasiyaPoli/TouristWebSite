namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _44 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BackRoutes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        DestinationPointId = c.Long(nullable: false),
                        LeavePointId = c.Long(nullable: false),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DestinationPoints", t => t.DestinationPointId, cascadeDelete: true)
                .ForeignKey("dbo.LeavePoints", t => t.LeavePointId, cascadeDelete: true)
                .Index(t => t.DestinationPointId)
                .Index(t => t.LeavePointId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BackRoutes", "LeavePointId", "dbo.LeavePoints");
            DropForeignKey("dbo.BackRoutes", "DestinationPointId", "dbo.DestinationPoints");
            DropIndex("dbo.BackRoutes", new[] { "LeavePointId" });
            DropIndex("dbo.BackRoutes", new[] { "DestinationPointId" });
            DropTable("dbo.BackRoutes");
        }
    }
}
