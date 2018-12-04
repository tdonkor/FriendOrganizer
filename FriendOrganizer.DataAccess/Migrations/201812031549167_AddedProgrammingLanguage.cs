namespace FriendOrganizer.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedProgrammingLanguage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProgrammingLanguage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Friend", "FavouriteLanguageId", c => c.Int());
            CreateIndex("dbo.Friend", "FavouriteLanguageId");
            AddForeignKey("dbo.Friend", "FavouriteLanguageId", "dbo.ProgrammingLanguage", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Friend", "FavouriteLanguageId", "dbo.ProgrammingLanguage");
            DropIndex("dbo.Friend", new[] { "FavouriteLanguageId" });
            DropColumn("dbo.Friend", "FavouriteLanguageId");
            DropTable("dbo.ProgrammingLanguage");
        }
    }
}
