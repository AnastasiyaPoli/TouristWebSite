namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _18 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "TypeOfStudies", c => c.String());
            AddColumn("dbo.AspNetUsers", "StudiesDescription", c => c.String());
            AddColumn("dbo.AspNetUsers", "TypeOfWork", c => c.String());
            AddColumn("dbo.AspNetUsers", "WorkDescription", c => c.String());
            AddColumn("dbo.AspNetUsers", "SportsDescription", c => c.String());
            AddColumn("dbo.AspNetUsers", "MusicDescription", c => c.String());
            AddColumn("dbo.AspNetUsers", "FilmsDescription", c => c.String());
            AddColumn("dbo.AspNetUsers", "BooksDescription", c => c.String());
            AddColumn("dbo.AspNetUsers", "HobbiesDescription", c => c.String());
            AddColumn("dbo.AspNetUsers", "OtherInfo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "OtherInfo");
            DropColumn("dbo.AspNetUsers", "HobbiesDescription");
            DropColumn("dbo.AspNetUsers", "BooksDescription");
            DropColumn("dbo.AspNetUsers", "FilmsDescription");
            DropColumn("dbo.AspNetUsers", "MusicDescription");
            DropColumn("dbo.AspNetUsers", "SportsDescription");
            DropColumn("dbo.AspNetUsers", "WorkDescription");
            DropColumn("dbo.AspNetUsers", "TypeOfWork");
            DropColumn("dbo.AspNetUsers", "StudiesDescription");
            DropColumn("dbo.AspNetUsers", "TypeOfStudies");
        }
    }
}
