namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _79 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Hotels", "Link", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Hotels", "Link");
        }
    }
}
