namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _55 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "DateOfBirth", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "DateOfBirth", c => c.DateTime(nullable: false));
        }
    }
}
