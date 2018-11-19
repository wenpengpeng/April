namespace Domain.EntityFramework.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AuditLogs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(),
                        ServiceName = c.String(),
                        MethodName = c.String(),
                        Parameters = c.String(),
                        ExecutionTime = c.String(),
                        ExecutionDuration = c.Int(nullable: false),
                        ClientIpAddress = c.String(),
                        ClientName = c.String(),
                        BrowserInfo = c.String(),
                        Exception = c.String(),
                        ImpersonatorTenantId = c.Int(),
                        CustomData = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Members",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        MemberCode = c.String(),
                        CompanyName = c.String(),
                        CompayTel = c.String(),
                        BuyerAudit = c.Int(nullable: false),
                        IsSelfSupport = c.Boolean(),
                        SupplierAudit = c.Int(nullable: false),
                        UserType = c.Int(),
                        BidOpeningPassword = c.String(),
                        TransactionPassword = c.String(),
                        Remark = c.String(),
                        CreationTime = c.String(),
                        UserBase_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserBases", t => t.UserBase_Id)
                .Index(t => t.UserBase_Id);
            
            CreateTable(
                "dbo.UserBases",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserName = c.String(),
                        IsLockoutEnaled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(),
                        SecurityStamp = c.String(),
                        PasswordHash = c.String(),
                        IsEmailComfirmed = c.Boolean(nullable: false),
                        Email = c.String(),
                        PhoneNumber = c.String(),
                        IsPhoneNumberComfirmed = c.Boolean(nullable: false),
                        RealName = c.String(),
                        IsRelationExpert = c.Boolean(nullable: false),
                        CreationTime = c.String(),
                        BelongUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Managers",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        UserId = c.Long(nullable: false),
                        CreationTime = c.String(),
                        UserBase_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserBases", t => t.UserBase_Id)
                .Index(t => t.UserBase_Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                        Code = c.String(),
                        IsValid = c.Boolean(nullable: false),
                        DefaultRoleType = c.Int(),
                        IsSystem = c.Boolean(nullable: false),
                        CreationTime = c.String(),
                        BelongUserId = c.Long(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MenuAppAuthorizes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        MenuCode = c.String(),
                        AppCode = c.String(),
                        OperationCode = c.String(),
                        OperationDescription = c.String(),
                        MenuId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Menus", t => t.MenuId, cascadeDelete: true)
                .Index(t => t.MenuId);
            
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Icon = c.String(),
                        ParentId = c.Long(),
                        DisplayName = c.String(),
                        RequestUrl = c.String(),
                        Target = c.Int(),
                        IsMenu = c.Boolean(nullable: false),
                        IsExpand = c.Boolean(nullable: false),
                        IsPublic = c.Boolean(nullable: false),
                        IsInterface = c.Boolean(nullable: false),
                        Remark = c.String(),
                        IsValid = c.Boolean(nullable: false),
                        Code = c.String(),
                        Category = c.Int(),
                        Layer = c.String(),
                        HasLevel = c.Boolean(nullable: false),
                        Sort = c.Int(nullable: false),
                        CreationTime = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Menus", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        UserId = c.Long(nullable: false),
                        CreationTime = c.String(),
                        UserBase_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.UserBases", t => t.UserBase_Id)
                .Index(t => t.UserBase_Id);
            
            CreateTable(
                "dbo.MenuRoles",
                c => new
                    {
                        Menu_Id = c.Long(nullable: false),
                        Role_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Menu_Id, t.Role_Id })
                .ForeignKey("dbo.Menus", t => t.Menu_Id, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .Index(t => t.Menu_Id)
                .Index(t => t.Role_Id);
            
            CreateTable(
                "dbo.MenuAppAuthorizeRoles",
                c => new
                    {
                        MenuAppAuthorize_Id = c.Long(nullable: false),
                        Role_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.MenuAppAuthorize_Id, t.Role_Id })
                .ForeignKey("dbo.MenuAppAuthorizes", t => t.MenuAppAuthorize_Id, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .Index(t => t.MenuAppAuthorize_Id)
                .Index(t => t.Role_Id);
            
            CreateTable(
                "dbo.RoleUserBases",
                c => new
                    {
                        Role_Id = c.Long(nullable: false),
                        UserBase_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_Id, t.UserBase_Id })
                .ForeignKey("dbo.Roles", t => t.Role_Id, cascadeDelete: true)
                .ForeignKey("dbo.UserBases", t => t.UserBase_Id, cascadeDelete: true)
                .Index(t => t.Role_Id)
                .Index(t => t.UserBase_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserClaims", "UserBase_Id", "dbo.UserBases");
            DropForeignKey("dbo.RoleUserBases", "UserBase_Id", "dbo.UserBases");
            DropForeignKey("dbo.RoleUserBases", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.MenuAppAuthorizeRoles", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.MenuAppAuthorizeRoles", "MenuAppAuthorize_Id", "dbo.MenuAppAuthorizes");
            DropForeignKey("dbo.MenuRoles", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.MenuRoles", "Menu_Id", "dbo.Menus");
            DropForeignKey("dbo.MenuAppAuthorizes", "MenuId", "dbo.Menus");
            DropForeignKey("dbo.Menus", "ParentId", "dbo.Menus");
            DropForeignKey("dbo.Members", "UserBase_Id", "dbo.UserBases");
            DropForeignKey("dbo.Managers", "UserBase_Id", "dbo.UserBases");
            DropIndex("dbo.RoleUserBases", new[] { "UserBase_Id" });
            DropIndex("dbo.RoleUserBases", new[] { "Role_Id" });
            DropIndex("dbo.MenuAppAuthorizeRoles", new[] { "Role_Id" });
            DropIndex("dbo.MenuAppAuthorizeRoles", new[] { "MenuAppAuthorize_Id" });
            DropIndex("dbo.MenuRoles", new[] { "Role_Id" });
            DropIndex("dbo.MenuRoles", new[] { "Menu_Id" });
            DropIndex("dbo.UserClaims", new[] { "UserBase_Id" });
            DropIndex("dbo.Menus", new[] { "ParentId" });
            DropIndex("dbo.MenuAppAuthorizes", new[] { "MenuId" });
            DropIndex("dbo.Managers", new[] { "UserBase_Id" });
            DropIndex("dbo.Members", new[] { "UserBase_Id" });
            DropTable("dbo.RoleUserBases");
            DropTable("dbo.MenuAppAuthorizeRoles");
            DropTable("dbo.MenuRoles");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Menus");
            DropTable("dbo.MenuAppAuthorizes");
            DropTable("dbo.Roles");
            DropTable("dbo.Managers");
            DropTable("dbo.UserBases");
            DropTable("dbo.Members");
            DropTable("dbo.AuditLogs");
        }
    }
}
