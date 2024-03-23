namespace PAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdminDetails",
                c => new
                    {
                        AdminID = c.String(nullable: false, maxLength: 225),
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 225),
                        LastName = c.String(nullable: false, maxLength: 225),
                        Age = c.Short(nullable: false),
                        Gender = c.Int(nullable: false),
                        ContectNumber = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 225),
                        isApproved = c.Boolean(nullable: false),
                        RoleID = c.Int(),
                    })
                .PrimaryKey(t => t.AdminID)
                .ForeignKey("dbo.Roles_", t => t.RoleID)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.Roles_",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        RoleID = c.Int(nullable: false),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.AdminLogins",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdminID = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ClerkLogins",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ClerkID = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ClerkDetails",
                c => new
                    {
                        ClerkID = c.String(nullable: false, maxLength: 225),
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 225),
                        LastName = c.String(nullable: false, maxLength: 225),
                        Age = c.Short(nullable: false),
                        Gender = c.Int(nullable: false),
                        ContectNumber = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 225),
                        isApproved = c.Boolean(nullable: false),
                        RoleID = c.Int(),
                    })
                .PrimaryKey(t => t.ClerkID)
                .ForeignKey("dbo.Roles_", t => t.RoleID)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.DietRecommendations",
                c => new
                    {
                        DietID = c.Int(nullable: false, identity: true),
                        PatientId = c.String(maxLength: 225),
                        DoctorId = c.String(maxLength: 225),
                        DietDuration = c.Int(nullable: false),
                        DietContent = c.Int(nullable: false),
                        RecommendedExercise = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.DietID)
                .ForeignKey("dbo.DoctorDetails", t => t.DoctorId)
                .ForeignKey("dbo.PatientDetails", t => t.PatientId)
                .Index(t => t.PatientId)
                .Index(t => t.DoctorId);
            
            CreateTable(
                "dbo.DoctorDetails",
                c => new
                    {
                        DoctorID = c.String(nullable: false, maxLength: 225),
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 225),
                        LastName = c.String(nullable: false, maxLength: 225),
                        Age = c.Short(nullable: false),
                        Gender = c.Int(nullable: false),
                        ContectNumber = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 225),
                        isApproved = c.Boolean(nullable: false),
                        RoleID = c.Int(),
                    })
                .PrimaryKey(t => t.DoctorID)
                .ForeignKey("dbo.Roles_", t => t.RoleID)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.PatientDetails",
                c => new
                    {
                        PatientID = c.String(nullable: false, maxLength: 225),
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 225),
                        LastName = c.String(nullable: false, maxLength: 225),
                        Age = c.Short(nullable: false),
                        Gender = c.Int(nullable: false),
                        ContectNumber = c.String(nullable: false),
                        Password = c.String(nullable: false, maxLength: 225),
                        isApproved = c.Boolean(nullable: false),
                        RoleID = c.Int(),
                        DoctorID = c.String(maxLength: 225),
                        TestRequest = c.Boolean(nullable: false),
                        Illnes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PatientID)
                .ForeignKey("dbo.DoctorDetails", t => t.DoctorID)
                .ForeignKey("dbo.Roles_", t => t.RoleID)
                .Index(t => t.PatientID, unique: true, name: "ad")
                .Index(t => t.RoleID)
                .Index(t => t.DoctorID);
            
            CreateTable(
                "dbo.TestDetails",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DoctorID = c.String(maxLength: 225),
                        PatientID = c.String(maxLength: 225),
                        TestPerformedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Test_TODO = c.Int(nullable: false),
                        TestResults = c.Int(nullable: false),
                        TestPrice = c.Int(nullable: false),
                        isActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PatientDetails", t => t.PatientID)
                .ForeignKey("dbo.DoctorDetails", t => t.DoctorID)
                .Index(t => t.DoctorID)
                .Index(t => t.PatientID);
            
            CreateTable(
                "dbo.DoctorLogins",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DoctorID = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PatientLogins",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PatientID = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DietRecommendations", "PatientId", "dbo.PatientDetails");
            DropForeignKey("dbo.TestDetails", "DoctorID", "dbo.DoctorDetails");
            DropForeignKey("dbo.TestDetails", "PatientID", "dbo.PatientDetails");
            DropForeignKey("dbo.PatientDetails", "RoleID", "dbo.Roles_");
            DropForeignKey("dbo.PatientDetails", "DoctorID", "dbo.DoctorDetails");
            DropForeignKey("dbo.DietRecommendations", "DoctorId", "dbo.DoctorDetails");
            DropForeignKey("dbo.DoctorDetails", "RoleID", "dbo.Roles_");
            DropForeignKey("dbo.ClerkDetails", "RoleID", "dbo.Roles_");
            DropForeignKey("dbo.AdminDetails", "RoleID", "dbo.Roles_");
            DropIndex("dbo.TestDetails", new[] { "PatientID" });
            DropIndex("dbo.TestDetails", new[] { "DoctorID" });
            DropIndex("dbo.PatientDetails", new[] { "DoctorID" });
            DropIndex("dbo.PatientDetails", new[] { "RoleID" });
            DropIndex("dbo.PatientDetails", "ad");
            DropIndex("dbo.DoctorDetails", new[] { "RoleID" });
            DropIndex("dbo.DietRecommendations", new[] { "DoctorId" });
            DropIndex("dbo.DietRecommendations", new[] { "PatientId" });
            DropIndex("dbo.ClerkDetails", new[] { "RoleID" });
            DropIndex("dbo.AdminDetails", new[] { "RoleID" });
            DropTable("dbo.PatientLogins");
            DropTable("dbo.DoctorLogins");
            DropTable("dbo.TestDetails");
            DropTable("dbo.PatientDetails");
            DropTable("dbo.DoctorDetails");
            DropTable("dbo.DietRecommendations");
            DropTable("dbo.ClerkDetails");
            DropTable("dbo.ClerkLogins");
            DropTable("dbo.AdminLogins");
            DropTable("dbo.Roles_");
            DropTable("dbo.AdminDetails");
        }
    }
}
