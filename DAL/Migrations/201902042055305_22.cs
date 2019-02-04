namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _22 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ApplicationUserId = c.String(maxLength: 128),
                        Text = c.String(),
                        Answer = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Questions", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Questions", new[] { "ApplicationUserId" });
            DropTable("dbo.Questions");
        }
    }
}
