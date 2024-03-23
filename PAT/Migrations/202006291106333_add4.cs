namespace PAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TestResults", "TestID", "dbo.TestDetails");
            DropIndex("dbo.TestResults", new[] { "TestID" });
            AddColumn("dbo.TestDetails", "TestPerformedDate", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("dbo.TestDetails", "Test_Result", c => c.Int());
            AddColumn("dbo.TestDetails", "TestPrice", c => c.Int());
            AddColumn("dbo.TestDetails", "isActive", c => c.Boolean(nullable: false));
            DropTable("dbo.TestResults");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.ID);
            
            DropColumn("dbo.TestDetails", "isActive");
            DropColumn("dbo.TestDetails", "TestPrice");
            DropColumn("dbo.TestDetails", "Test_Result");
            DropColumn("dbo.TestDetails", "TestPerformedDate");
            CreateIndex("dbo.TestResults", "TestID");
            AddForeignKey("dbo.TestResults", "TestID", "dbo.TestDetails", "ID", cascadeDelete: true);
        }
    }
}
