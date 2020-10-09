namespace BookDiary.DAL.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class CreateUsersTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nickname = c.String(nullable: false, maxLength: 30, unicode: false),
                        Fullname = c.String(maxLength: 225),
                        Email = c.String(nullable: false, maxLength: 255, unicode: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Nickname, unique: true)
                .Index(t => t.Email, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Users", new[] { "Email" });
            DropIndex("dbo.Users", new[] { "Nickname" });
            DropTable("dbo.Users");
        }
    }
}
