namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _12 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tours", "RouteLink", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tours", "RouteLink");
        }
    }
}
