namespace PAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add2 : DbMigration
    {
        public override void Up()
        {
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


            AlterColumn("dbo.TestDetails", "TestPerformedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("dbo.TestDetails", "Test_TODO", c => c.Int());
            AlterColumn("dbo.TestDetails", "TestResults", c => c.Int());
            AlterColumn("dbo.TestDetails", "TestPrice", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TestDetails", "TestPrice", c => c.Int(nullable: false));
            AlterColumn("dbo.TestDetails", "TestResults", c => c.Int(nullable: false));
            AlterColumn("dbo.TestDetails", "Test_TODO", c => c.Int(nullable: false));
            AlterColumn("dbo.TestDetails", "TestPerformedDate", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
        }
    }
}
