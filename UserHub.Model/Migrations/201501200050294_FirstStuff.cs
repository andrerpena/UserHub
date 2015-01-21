namespace UserHub.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstStuff : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Suggestions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        Title = c.String(nullable: false),
                        Description = c.String(nullable: false, unicode: false),
                        CreatedBy_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy_Id, cascadeDelete: true)
                .Index(t => t.CreatedBy_Id);
            
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        CreatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        CreatedBy_Id = c.String(nullable: false, maxLength: 128),
                        Suggestion_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy_Id, cascadeDelete: true)
                .ForeignKey("dbo.Suggestions", t => t.Suggestion_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.Suggestion_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Votes", "Suggestion_Id", "dbo.Suggestions");
            DropForeignKey("dbo.Votes", "CreatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Suggestions", "CreatedBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Votes", new[] { "Suggestion_Id" });
            DropIndex("dbo.Votes", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Suggestions", new[] { "CreatedBy_Id" });
            DropTable("dbo.Votes");
            DropTable("dbo.Suggestions");
        }
    }
}
