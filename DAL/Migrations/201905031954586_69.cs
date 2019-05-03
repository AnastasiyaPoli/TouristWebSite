namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _69 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TourExcursions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ExcursionId = c.Long(nullable: false),
                        ConstructedTourId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ConstructedTours", t => t.ConstructedTourId, cascadeDelete: false)
                .ForeignKey("dbo.Excursions", t => t.ExcursionId, cascadeDelete: false)
                .Index(t => t.ExcursionId)
                .Index(t => t.ConstructedTourId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TourExcursions", "ExcursionId", "dbo.Excursions");
            DropForeignKey("dbo.TourExcursions", "ConstructedTourId", "dbo.ConstructedTours");
            DropIndex("dbo.TourExcursions", new[] { "ConstructedTourId" });
            DropIndex("dbo.TourExcursions", new[] { "ExcursionId" });
            DropTable("dbo.TourExcursions");
        }
    }
}
