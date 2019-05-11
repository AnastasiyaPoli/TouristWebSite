namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _74 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "EmailGuid", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "EmailGuid");
        }
    }
}
