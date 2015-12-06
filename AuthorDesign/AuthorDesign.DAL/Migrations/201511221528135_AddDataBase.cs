namespace AuthorDesign.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDataBase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ActionToPages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PageId = c.Int(nullable: false),
                        ActionList = c.String(maxLength: 300, unicode: false),
                        IsDelete = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AdminLoginLogs",
                c => new
                    {
                        AdminLoginLogId = c.Int(nullable: false, identity: true),
                        AdminId = c.Int(nullable: false),
                        AdminLoginAddress = c.String(maxLength: 50),
                        AdminLoginIP = c.String(maxLength: 50, unicode: false),
                        AdminLoginTime = c.DateTime(nullable: false),
                        AdminLoginInfo = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.AdminLoginLogId);
            
            CreateTable(
                "dbo.AdminOperations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdminId = c.Int(nullable: false),
                        AuthoryId = c.Int(nullable: false),
                        Action = c.Int(nullable: false),
                        Title = c.String(maxLength: 15),
                        Content = c.String(maxLength: 200),
                        OperateIP = c.String(maxLength: 50, unicode: false),
                        OperateAddress = c.String(maxLength: 50),
                        OperateInfo = c.String(maxLength: 50, unicode: false),
                        CreateTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdminName = c.String(maxLength: 30, unicode: false),
                        Mobile = c.String(maxLength: 11, unicode: false),
                        Email = c.String(maxLength: 100, unicode: false),
                        Password = c.String(maxLength: 120, unicode: false),
                        Salt = c.String(maxLength: 10, unicode: false),
                        AuthoryId = c.Int(nullable: false),
                        CreateTime = c.DateTime(nullable: false),
                        IsSuperAdmin = c.Byte(nullable: false),
                        LastLoginTime = c.DateTime(nullable: false),
                        LastLoginAddress = c.String(maxLength: 50),
                        LastLoginInfo = c.String(maxLength: 50, unicode: false),
                        LastLoginIp = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AdminToPages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdminId = c.Int(nullable: false),
                        PageId = c.Int(nullable: false),
                        ActionList = c.String(maxLength: 300, unicode: false),
                        IsShow = c.Byte(nullable: false),
                        IsDelete = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Authories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 15, unicode: false),
                        Intro = c.String(maxLength: 30, unicode: false),
                        State = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AuthoryToPages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AuthoryId = c.Int(nullable: false),
                        PageId = c.Int(nullable: false),
                        ActionList = c.String(maxLength: 300, unicode: false),
                        IsShow = c.Byte(nullable: false),
                        IsDelete = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PageActions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 15, unicode: false),
                        ActionCode = c.String(maxLength: 35, unicode: false),
                        IsShow = c.Byte(nullable: false),
                        ActionLevel = c.Byte(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PageMenus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PId = c.Int(nullable: false),
                        Name = c.String(maxLength: 15, unicode: false),
                        PageUrl = c.String(maxLength: 50, unicode: false),
                        IsShow = c.Byte(nullable: false),
                        OrderNum = c.Int(nullable: false),
                        Ico = c.String(maxLength: 30, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PageMenus");
            DropTable("dbo.PageActions");
            DropTable("dbo.AuthoryToPages");
            DropTable("dbo.Authories");
            DropTable("dbo.AdminToPages");
            DropTable("dbo.Admins");
            DropTable("dbo.AdminOperations");
            DropTable("dbo.AdminLoginLogs");
            DropTable("dbo.ActionToPages");
        }
    }
}
