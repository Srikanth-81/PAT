namespace PAT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add1 : DbMigration
    {
        public override void Up()
        {
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
