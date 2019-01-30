namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tours", "NumberOfPhotos", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tours", "NumberOfPhotos");
        }
    }
}
