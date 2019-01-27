namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Tours", "MainName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tours", "MainName", c => c.String());
        }
    }
}
