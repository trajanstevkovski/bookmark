namespace ReadLater.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Identity_Tables_2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Bookmarks", new[] { "User_Id" });
            DropColumn("dbo.Bookmarks", "UserId");
            RenameColumn(table: "dbo.Bookmarks", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.Bookmarks", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Bookmarks", "UserId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Bookmarks", new[] { "UserId" });
            AlterColumn("dbo.Bookmarks", "UserId", c => c.Guid(nullable: false));
            RenameColumn(table: "dbo.Bookmarks", name: "UserId", newName: "User_Id");
            AddColumn("dbo.Bookmarks", "UserId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Bookmarks", "User_Id");
        }
    }
}
