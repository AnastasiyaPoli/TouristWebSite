namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _66 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Excursions", "ConstructedTour_Id", c => c.Long());
            CreateIndex("dbo.Excursions", "ConstructedTour_Id");
            AddForeignKey("dbo.Excursions", "ConstructedTour_Id", "dbo.ConstructedTours", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Excursions", "ConstructedTour_Id", "dbo.ConstructedTours");
            DropIndex("dbo.Excursions", new[] { "ConstructedTour_Id" });
            DropColumn("dbo.Excursions", "ConstructedTour_Id");
        }
    }
}
