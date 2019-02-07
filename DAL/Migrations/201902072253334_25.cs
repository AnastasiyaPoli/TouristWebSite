namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _25 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Favourites",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TourId = c.Long(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tours", t => t.TourId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.TourId)
                .Index(t => t.ApplicationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Favourites", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Favourites", "TourId", "dbo.Tours");
            DropIndex("dbo.Favourites", new[] { "ApplicationUserId" });
            DropIndex("dbo.Favourites", new[] { "TourId" });
            DropTable("dbo.Favourites");
        }
    }
}
