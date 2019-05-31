namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _78 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Excursions", "Link", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Excursions", "Link");
        }
    }
}
