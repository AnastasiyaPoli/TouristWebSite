namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _32_restore_leave_points : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LeavePoints",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        CityId = c.Long(nullable: false),
                        TransportId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .ForeignKey("dbo.Transports", t => t.TransportId, cascadeDelete: true)
                .Index(t => t.CityId)
                .Index(t => t.TransportId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LeavePoints", "TransportId", "dbo.Transports");
            DropForeignKey("dbo.LeavePoints", "CityId", "dbo.Cities");
            DropIndex("dbo.LeavePoints", new[] { "TransportId" });
            DropIndex("dbo.LeavePoints", new[] { "CityId" });
            DropTable("dbo.LeavePoints");
        }
    }
}
