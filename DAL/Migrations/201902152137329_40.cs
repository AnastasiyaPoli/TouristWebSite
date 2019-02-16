namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _40 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Hotels",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        DestinationPointId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DestinationPoints", t => t.DestinationPointId, cascadeDelete: true)
                .Index(t => t.DestinationPointId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Hotels", "DestinationPointId", "dbo.DestinationPoints");
            DropIndex("dbo.Hotels", new[] { "DestinationPointId" });
            DropTable("dbo.Hotels");
        }
    }
}
