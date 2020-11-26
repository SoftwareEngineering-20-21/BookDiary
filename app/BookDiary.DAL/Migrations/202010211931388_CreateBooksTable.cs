namespace BookDiary.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateBooksTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 225, unicode: false),
                        Author = c.String(nullable: false, maxLength: 225, unicode: false),
                        TotalPages = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Statistics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Day = c.DateTimeOffset(nullable: false, precision: 7),
                        OldPages = c.Int(nullable: false),
                        NewPages = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.BookId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Statistics", "BookId", "dbo.Books");
            DropForeignKey("dbo.Books", "UserId", "dbo.Users");
            DropIndex("dbo.Statistics", new[] { "BookId" });
            DropIndex("dbo.Books", new[] { "UserId" });
            DropTable("dbo.Statistics");
            DropTable("dbo.Books");
        }
    }
}
