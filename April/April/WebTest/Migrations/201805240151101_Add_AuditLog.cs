namespace WebTest.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_AuditLog : DbMigration
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
                        ExecutionTime = c.DateTime(nullable: false),
                        ExecutionDuration = c.Int(nullable: false),
                        ClientIpAddress = c.String(),
                        ClientName = c.String(),
                        BrowserInfo = c.String(),
                        Exception = c.String(),
                        ImpersonatorTenantId = c.Int(),
                        CustomData = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AuditLogs");
        }
    }
}
