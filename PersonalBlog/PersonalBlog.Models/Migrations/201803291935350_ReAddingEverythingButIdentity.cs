namespace PersonalBlog.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReAddingEverythingButIdentity : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        CategoryId = c.Int(nullable: false, identity: true),
                        CategoryName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.CategoryId);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostId = c.Int(nullable: false, identity: true),
                        PostTitle = c.String(nullable: false, maxLength: 100),
                        PostBody = c.String(),
                        IsApproved = c.Boolean(nullable: false),
                        CreatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        CategoryId = c.Int(),
                        ImageFileName = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.PostId)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        TagName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.TagId);
            
            CreateTable(
                "dbo.TagsPosts",
                c => new
                    {
                        Tags_TagId = c.Int(nullable: false),
                        Posts_PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tags_TagId, t.Posts_PostId })
                .ForeignKey("dbo.Tags", t => t.Tags_TagId, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.Posts_PostId, cascadeDelete: true)
                .Index(t => t.Tags_TagId)
                .Index(t => t.Posts_PostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagsPosts", "Posts_PostId", "dbo.Posts");
            DropForeignKey("dbo.TagsPosts", "Tags_TagId", "dbo.Tags");
            DropForeignKey("dbo.Posts", "CategoryId", "dbo.Categories");
            DropIndex("dbo.TagsPosts", new[] { "Posts_PostId" });
            DropIndex("dbo.TagsPosts", new[] { "Tags_TagId" });
            DropIndex("dbo.Posts", new[] { "CategoryId" });
            DropTable("dbo.TagsPosts");
            DropTable("dbo.Tags");
            DropTable("dbo.Posts");
            DropTable("dbo.Categories");
        }
    }
}
