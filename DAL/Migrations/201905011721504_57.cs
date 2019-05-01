namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _57 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ConstructedTours", "Comment", c => c.String());
            AddColumn("dbo.ConstructedTours", "Mark", c => c.String());
            AddColumn("dbo.ConstructedTours", "NumberMark", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ConstructedTours", "NumberMark");
            DropColumn("dbo.ConstructedTours", "Mark");
            DropColumn("dbo.ConstructedTours", "Comment");
        }
    }
}
