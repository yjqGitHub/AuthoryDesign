namespace AuthorDesign.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAdminIsLogin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdminOperations", "AdminOperationId", c => c.Int(nullable: false));
            AddColumn("dbo.Admins", "IsLogin", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Admins", "IsLogin");
            DropColumn("dbo.AdminOperations", "AdminOperationId");
        }
    }
}
