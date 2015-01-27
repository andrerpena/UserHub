namespace UserHub.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingSuggestionCategory : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tenancies", "CreatedBy_Id", "dbo.AspNetUsers");
            CreateTable(
                "dbo.SuggestionCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(nullable: false),
                        Tenancy_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Tenancies", t => t.Tenancy_Id, cascadeDelete: true)
                .Index(t => t.Tenancy_Id);
            
            AddForeignKey("dbo.Tenancies", "CreatedBy_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tenancies", "CreatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.SuggestionCategories", "Tenancy_Id", "dbo.Tenancies");
            DropIndex("dbo.SuggestionCategories", new[] { "Tenancy_Id" });
            DropTable("dbo.SuggestionCategories");
            AddForeignKey("dbo.Tenancies", "CreatedBy_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
