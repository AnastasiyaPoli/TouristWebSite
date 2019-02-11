namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _29 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DestinationPoints",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Routes", "DestinationPointId", c => c.Long(nullable: false));
            CreateIndex("dbo.Routes", "DestinationPointId");
            AddForeignKey("dbo.Routes", "DestinationPointId", "dbo.DestinationPoints", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Routes", "DestinationPointId", "dbo.DestinationPoints");
            DropIndex("dbo.Routes", new[] { "DestinationPointId" });
            DropColumn("dbo.Routes", "DestinationPointId");
            DropTable("dbo.DestinationPoints");
        }
    }
}
