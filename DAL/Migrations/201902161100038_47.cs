namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _47 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConstructedTours", "BackRouteId", c => c.Long(nullable: false));
            AddColumn("dbo.ConstructedTours", "BackClass", c => c.String());
            AddColumn("dbo.ConstructedTours", "ApplicationUserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.ConstructedTours", "BackRouteId");
            CreateIndex("dbo.ConstructedTours", "ApplicationUserId");
            AddForeignKey("dbo.ConstructedTours", "BackRouteId", "dbo.BackRoutes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ConstructedTours", "ApplicationUserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ConstructedTours", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ConstructedTours", "BackRouteId", "dbo.BackRoutes");
            DropIndex("dbo.ConstructedTours", new[] { "ApplicationUserId" });
            DropIndex("dbo.ConstructedTours", new[] { "BackRouteId" });
            DropColumn("dbo.ConstructedTours", "ApplicationUserId");
            DropColumn("dbo.ConstructedTours", "BackClass");
            DropColumn("dbo.ConstructedTours", "BackRouteId");
        }
    }
}
