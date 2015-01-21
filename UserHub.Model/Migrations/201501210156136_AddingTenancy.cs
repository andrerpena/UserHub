namespace UserHub.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingTenancy : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tenancies",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CreatedOn = c.DateTimeOffset(nullable: false, precision: 7),
                        CreatedBy_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy_Id, cascadeDelete: true)
                .Index(t => t.CreatedBy_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tenancies", "CreatedBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Tenancies", new[] { "CreatedBy_Id" });
            DropTable("dbo.Tenancies");
        }
    }
}
