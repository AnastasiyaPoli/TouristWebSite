namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _59 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Recommendations",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ApplicationUserId = c.String(maxLength: 128),
                        ConstructedTourId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .ForeignKey("dbo.ConstructedTours", t => t.ConstructedTourId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.ConstructedTourId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Recommendations", "ConstructedTourId", "dbo.ConstructedTours");
            DropForeignKey("dbo.Recommendations", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Recommendations", new[] { "ConstructedTourId" });
            DropIndex("dbo.Recommendations", new[] { "ApplicationUserId" });
            DropTable("dbo.Recommendations");
        }
    }
}
