namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _14 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Gender", c => c.String());
            AlterColumn("dbo.AspNetUsers", "MaritalStatus", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Country", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Country", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "MaritalStatus", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Gender", c => c.Int(nullable: false));
        }
    }
}
