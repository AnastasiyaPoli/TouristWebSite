namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _65 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ConstructedTours", "Excursion1Id", "dbo.Excursions");
            DropForeignKey("dbo.ConstructedTours", "Excursion2Id", "dbo.Excursions");
            DropForeignKey("dbo.ConstructedTours", "Excursion3Id", "dbo.Excursions");
            DropForeignKey("dbo.ConstructedTours", "Excursion4Id", "dbo.Excursions");
            DropForeignKey("dbo.ConstructedTours", "Excursion5Id", "dbo.Excursions");
            DropIndex("dbo.ConstructedTours", new[] { "Excursion1Id" });
            DropIndex("dbo.ConstructedTours", new[] { "Excursion2Id" });
            DropIndex("dbo.ConstructedTours", new[] { "Excursion3Id" });
            DropIndex("dbo.ConstructedTours", new[] { "Excursion4Id" });
            DropIndex("dbo.ConstructedTours", new[] { "Excursion5Id" });
            DropColumn("dbo.ConstructedTours", "Excursion1Id");
            DropColumn("dbo.ConstructedTours", "Excursion2Id");
            DropColumn("dbo.ConstructedTours", "Excursion3Id");
            DropColumn("dbo.ConstructedTours", "Excursion4Id");
            DropColumn("dbo.ConstructedTours", "Excursion5Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ConstructedTours", "Excursion5Id", c => c.Long());
            AddColumn("dbo.ConstructedTours", "Excursion4Id", c => c.Long());
            AddColumn("dbo.ConstructedTours", "Excursion3Id", c => c.Long());
            AddColumn("dbo.ConstructedTours", "Excursion2Id", c => c.Long());
            AddColumn("dbo.ConstructedTours", "Excursion1Id", c => c.Long());
            CreateIndex("dbo.ConstructedTours", "Excursion5Id");
            CreateIndex("dbo.ConstructedTours", "Excursion4Id");
            CreateIndex("dbo.ConstructedTours", "Excursion3Id");
            CreateIndex("dbo.ConstructedTours", "Excursion2Id");
            CreateIndex("dbo.ConstructedTours", "Excursion1Id");
            AddForeignKey("dbo.ConstructedTours", "Excursion5Id", "dbo.Excursions", "Id");
            AddForeignKey("dbo.ConstructedTours", "Excursion4Id", "dbo.Excursions", "Id");
            AddForeignKey("dbo.ConstructedTours", "Excursion3Id", "dbo.Excursions", "Id");
            AddForeignKey("dbo.ConstructedTours", "Excursion2Id", "dbo.Excursions", "Id");
            AddForeignKey("dbo.ConstructedTours", "Excursion1Id", "dbo.Excursions", "Id");
        }
    }
}
