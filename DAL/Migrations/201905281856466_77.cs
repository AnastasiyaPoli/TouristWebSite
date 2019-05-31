namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _77 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tours", "Hotel", c => c.String());
            AddColumn("dbo.Tours", "Excursions", c => c.String());
            AddColumn("dbo.Tours", "Routes", c => c.String());
            AddColumn("dbo.Tours", "BackRoutes", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tours", "BackRoutes");
            DropColumn("dbo.Tours", "Routes");
            DropColumn("dbo.Tours", "Excursions");
            DropColumn("dbo.Tours", "Hotel");
        }
    }
}
