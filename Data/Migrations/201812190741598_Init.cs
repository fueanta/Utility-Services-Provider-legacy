namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Areas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CityId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cities", t => t.CityId)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CityId = c.Int(),
                        AreaId = c.Int(),
                        FakeId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Address = c.String(),
                        JoiningDate = c.DateTime(nullable: false),
                        Gender = c.Int(nullable: false),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Areas", t => t.AreaId)
                .ForeignKey("dbo.Cities", t => t.CityId)
                .Index(t => t.CityId)
                .Index(t => t.AreaId);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Salary = c.Double(nullable: false),
                        Commission = c.Double(nullable: false),
                        FakeId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Address = c.String(),
                        JoiningDate = c.DateTime(nullable: false),
                        Gender = c.Int(nullable: false),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LabourAssigneds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LabourId = c.Int(),
                        ServiceRequestId = c.Int(),
                        EmployeeId = c.Int(),
                        TimeAssigned = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .ForeignKey("dbo.Labours", t => t.LabourId)
                .ForeignKey("dbo.ServiceRequests", t => t.ServiceRequestId)
                .Index(t => t.LabourId)
                .Index(t => t.ServiceRequestId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.Labours",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Wallet = c.Double(nullable: false),
                        CityId = c.Int(),
                        AreaId = c.Int(),
                        FakeId = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Address = c.String(),
                        JoiningDate = c.DateTime(nullable: false),
                        Gender = c.Int(nullable: false),
                        ImagePath = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Areas", t => t.AreaId)
                .ForeignKey("dbo.Cities", t => t.CityId)
                .Index(t => t.CityId)
                .Index(t => t.AreaId);
            
            CreateTable(
                "dbo.ServiceRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(),
                        ServiceId = c.Int(),
                        ServiceTime = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        Helper = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId)
                .ForeignKey("dbo.Services", t => t.ServiceId)
                .Index(t => t.ClientId)
                .Index(t => t.ServiceId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ServiceName = c.String(),
                        ServiceCost = c.Double(nullable: false),
                        LabourCharge = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LabourServiceMaps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LabourId = c.Int(),
                        ServiceId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Labours", t => t.LabourId)
                .ForeignKey("dbo.Services", t => t.ServiceId)
                .Index(t => t.LabourId)
                .Index(t => t.ServiceId);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LabourId = c.Int(),
                        EmployeeId = c.Int(),
                        Amount = c.Double(nullable: false),
                        TransactionType = c.String(),
                        TransactionTime = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .ForeignKey("dbo.Labours", t => t.LabourId)
                .Index(t => t.LabourId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Phone = c.String(),
                        Password = c.String(),
                        UserType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transactions", "LabourId", "dbo.Labours");
            DropForeignKey("dbo.Transactions", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.LabourServiceMaps", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.LabourServiceMaps", "LabourId", "dbo.Labours");
            DropForeignKey("dbo.LabourAssigneds", "ServiceRequestId", "dbo.ServiceRequests");
            DropForeignKey("dbo.ServiceRequests", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.ServiceRequests", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.LabourAssigneds", "LabourId", "dbo.Labours");
            DropForeignKey("dbo.Labours", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Labours", "AreaId", "dbo.Areas");
            DropForeignKey("dbo.LabourAssigneds", "EmployeeId", "dbo.Employees");
            DropForeignKey("dbo.Clients", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Clients", "AreaId", "dbo.Areas");
            DropForeignKey("dbo.Areas", "CityId", "dbo.Cities");
            DropIndex("dbo.Transactions", new[] { "EmployeeId" });
            DropIndex("dbo.Transactions", new[] { "LabourId" });
            DropIndex("dbo.LabourServiceMaps", new[] { "ServiceId" });
            DropIndex("dbo.LabourServiceMaps", new[] { "LabourId" });
            DropIndex("dbo.ServiceRequests", new[] { "ServiceId" });
            DropIndex("dbo.ServiceRequests", new[] { "ClientId" });
            DropIndex("dbo.Labours", new[] { "AreaId" });
            DropIndex("dbo.Labours", new[] { "CityId" });
            DropIndex("dbo.LabourAssigneds", new[] { "EmployeeId" });
            DropIndex("dbo.LabourAssigneds", new[] { "ServiceRequestId" });
            DropIndex("dbo.LabourAssigneds", new[] { "LabourId" });
            DropIndex("dbo.Clients", new[] { "AreaId" });
            DropIndex("dbo.Clients", new[] { "CityId" });
            DropIndex("dbo.Areas", new[] { "CityId" });
            DropTable("dbo.UserLogins");
            DropTable("dbo.Transactions");
            DropTable("dbo.LabourServiceMaps");
            DropTable("dbo.Services");
            DropTable("dbo.ServiceRequests");
            DropTable("dbo.Labours");
            DropTable("dbo.LabourAssigneds");
            DropTable("dbo.Employees");
            DropTable("dbo.Clients");
            DropTable("dbo.Cities");
            DropTable("dbo.Areas");
        }
    }
}
