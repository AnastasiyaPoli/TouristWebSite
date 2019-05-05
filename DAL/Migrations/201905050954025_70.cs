namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _70 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Comments", "Mark");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "Mark", c => c.String());
        }
    }
}
