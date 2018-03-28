namespace PersonalBlog.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedIdentity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetRoles", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.AspNetUsers", "Role");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Role", c => c.String());
            DropColumn("dbo.AspNetRoles", "Discriminator");
        }
    }
}
