namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _37 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DestinationCities",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        DestinationCountryId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DestinationCountries", t => t.DestinationCountryId, cascadeDelete: true)
                .Index(t => t.DestinationCountryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DestinationCities", "DestinationCountryId", "dbo.DestinationCountries");
            DropIndex("dbo.DestinationCities", new[] { "DestinationCountryId" });
            DropTable("dbo.DestinationCities");
        }
    }
}
