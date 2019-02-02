namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _16 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookedTours",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        TourId = c.Long(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                        PeopleCount = c.Int(nullable: false),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tours", t => t.TourId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.TourId)
                .Index(t => t.ApplicationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookedTours", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.BookedTours", "TourId", "dbo.Tours");
            DropIndex("dbo.BookedTours", new[] { "ApplicationUserId" });
            DropIndex("dbo.BookedTours", new[] { "TourId" });
            DropTable("dbo.BookedTours");
        }
    }
}
