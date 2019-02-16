namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _42 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Excursions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        HotelId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hotels", t => t.HotelId, cascadeDelete: true)
                .Index(t => t.HotelId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Excursions", "HotelId", "dbo.Hotels");
            DropIndex("dbo.Excursions", new[] { "HotelId" });
            DropTable("dbo.Excursions");
        }
    }
}
