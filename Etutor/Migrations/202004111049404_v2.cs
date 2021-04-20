namespace Etutor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class v2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Documents", "Name", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.Documents", "Name");
        }
    }
}
