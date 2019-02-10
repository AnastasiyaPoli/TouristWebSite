namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _28 : DbMigration
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
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.LeavePoints", t => t.LeavePointId, cascadeDelete: true)
                .Index(t => t.LeavePointId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Routes", "LeavePointId", "dbo.LeavePoints");
            DropIndex("dbo.Routes", new[] { "LeavePointId" });
            DropTable("dbo.Routes");
        }
    }
}
