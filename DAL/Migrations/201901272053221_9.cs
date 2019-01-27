namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _9 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tours", "MainName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tours", "MainName");
        }
    }
}
