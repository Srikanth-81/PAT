namespace PAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TestResults",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TestPerformedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Test_Result = c.Int(),
                        TestPrice = c.Int(),
                        isActive = c.Boolean(nullable: false),
                        TestID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.TestDetails", t => t.TestID, cascadeDelete: true)
                .Index(t => t.TestID);
            
            DropColumn("dbo.TestDetails", "TestPerformedDate");
            DropColumn("dbo.TestDetails", "TestResults");
            DropColumn("dbo.TestDetails", "TestPrice");
            DropColumn("dbo.TestDetails", "isActive");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TestDetails", "isActive", c => c.Boolean(nullable: false));
            AddColumn("dbo.TestDetails", "TestPrice", c => c.Int());
            AddColumn("dbo.TestDetails", "TestResults", c => c.Int());
            AddColumn("dbo.TestDetails", "TestPerformedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            DropForeignKey("dbo.TestResults", "TestID", "dbo.TestDetails");
            DropIndex("dbo.TestResults", new[] { "TestID" });
            DropTable("dbo.TestResults");
        }
    }
}
