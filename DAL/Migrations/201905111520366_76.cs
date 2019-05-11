namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _76 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "EmailGuid", c => c.Guid());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "EmailGuid", c => c.Guid(nullable: false));
        }
    }
}
