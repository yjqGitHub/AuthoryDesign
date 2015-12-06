namespace AuthorDesign.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAdminOperation : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AdminOperations", "AdminOperationId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AdminOperations", "AdminOperationId", c => c.Int(nullable: false));
        }
    }
}
