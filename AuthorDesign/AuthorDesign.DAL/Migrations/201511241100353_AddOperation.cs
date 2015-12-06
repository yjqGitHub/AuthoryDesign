namespace AuthorDesign.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOperation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AdminOperations", "IsSuperAdmin", c => c.Byte(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AdminOperations", "IsSuperAdmin");
        }
    }
}
