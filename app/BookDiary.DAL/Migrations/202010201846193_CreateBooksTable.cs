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
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Books");
        }
    }
}
