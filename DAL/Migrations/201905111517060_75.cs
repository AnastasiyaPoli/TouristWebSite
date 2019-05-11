namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _75 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "EmailGuid", c => c.Guid(nullable: true));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "EmailGuid", c => c.String());
        }
    }
}
