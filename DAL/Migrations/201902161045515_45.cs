namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _45 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BackRoutes", "DestinationPointId", "dbo.DestinationPoints");
            DropForeignKey("dbo.BackRoutes", "LeavePointId", "dbo.LeavePoints");
            DropIndex("dbo.BackRoutes", new[] { "DestinationPointId" });
            DropIndex("dbo.BackRoutes", new[] { "LeavePointId" });
            DropTable("dbo.BackRoutes");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.BackRoutes", "LeavePointId");
            CreateIndex("dbo.BackRoutes", "DestinationPointId");
            AddForeignKey("dbo.BackRoutes", "LeavePointId", "dbo.LeavePoints", "Id", cascadeDelete: true);
            AddForeignKey("dbo.BackRoutes", "DestinationPointId", "dbo.DestinationPoints", "Id", cascadeDelete: true);
        }
    }
}
