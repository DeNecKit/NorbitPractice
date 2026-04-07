namespace CRUD.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddConstraints : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Tickets", new[] { "SessionId" });
            CreateIndex("dbo.Tickets", new[] { "SessionId", "Row", "Seat" }, unique: true);
        }
        
        public override void Down()
        {
            DropIndex("dbo.Tickets", new[] { "SessionId", "Row", "Seat" });
            CreateIndex("dbo.Tickets", "SessionId");
        }
    }
}
