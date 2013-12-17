namespace DutyPanel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class m1 : DbMigration
    {
        public override void Up()
        {
            MoveTable(name: "dbo.Users", newSchema: "visitor");
            MoveTable(name: "dbo.Statements", newSchema: "visitor");
        }
        
        public override void Down()
        {
            MoveTable(name: "visitor.Statements", newSchema: "dbo");
            MoveTable(name: "visitor.Users", newSchema: "dbo");
        }
    }
}
