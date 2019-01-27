namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "Link", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.News", "Link");
        }
    }
}
