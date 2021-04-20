namespace Etutor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "BodyText", c => c.String());
            AddColumn("dbo.Messages", "From", c => c.Int(nullable: false));
            DropColumn("dbo.Messages", "StdId");
            DropColumn("dbo.Messages", "TutId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Messages", "TutId", c => c.Int(nullable: false));
            AddColumn("dbo.Messages", "StdId", c => c.Int(nullable: false));
            DropColumn("dbo.Messages", "From");
            DropColumn("dbo.Messages", "BodyText");
        }
    }
}
