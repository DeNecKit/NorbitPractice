namespace CRUD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MovieId = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DateAndTime = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SessionId = c.Int(nullable: false),
                        Row = c.Byte(nullable: false),
                        Seat = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sessions", t => t.SessionId, cascadeDelete: true)
                .Index(t => t.SessionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "SessionId", "dbo.Sessions");
            DropIndex("dbo.Tickets", new[] { "SessionId" });
            DropTable("dbo.Tickets");
            DropTable("dbo.Sessions");
        }
    }
}
