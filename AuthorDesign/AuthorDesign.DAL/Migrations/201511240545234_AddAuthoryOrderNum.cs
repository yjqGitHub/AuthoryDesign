namespace AuthorDesign.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAuthoryOrderNum : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Authories", "OrderNum", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Authories", "OrderNum");
        }
    }
}
