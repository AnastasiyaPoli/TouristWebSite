namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _73 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookedTours", "AdditionalInfo", c => c.String());
            DropColumn("dbo.BookedTours", "Comment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.BookedTours", "Comment", c => c.String());
            DropColumn("dbo.BookedTours", "AdditionalInfo");
        }
    }
}
