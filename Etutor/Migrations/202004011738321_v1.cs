namespace Etutor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class v1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Staffs",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Assigns",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Staff_Id = c.Int(),
                        Tutor_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Staffs", t => t.Staff_Id)
                .ForeignKey("dbo.Students", t => t.Id)
                .ForeignKey("dbo.Tutors", t => t.Tutor_Id)
                .Index(t => t.Id)
                .Index(t => t.Staff_Id)
                .Index(t => t.Tutor_Id);
            
            CreateTable(
                "dbo.Documents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UploadTime = c.DateTime(nullable: false),
                        Type = c.String(),
                        Url = c.String(),
                        Assign_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Assigns", t => t.Assign_Id)
                .Index(t => t.Assign_Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StdId = c.Int(nullable: false),
                        TutId = c.Int(nullable: false),
                        Assign_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Assigns", t => t.Assign_Id)
                .Index(t => t.Assign_Id);
            
            CreateTable(
                "dbo.Records",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartTime = c.DateTime(nullable: false),
                        EndTime = c.DateTime(nullable: false),
                        Assign_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Assigns", t => t.Assign_Id)
                .Index(t => t.Assign_Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Tutors",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Name = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Accounts", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Assigns", "Tutor_Id", "dbo.Tutors");
            DropForeignKey("dbo.Tutors", "Id", "dbo.Accounts");
            DropForeignKey("dbo.Assigns", "Id", "dbo.Students");
            DropForeignKey("dbo.Students", "Id", "dbo.Accounts");
            DropForeignKey("dbo.Assigns", "Staff_Id", "dbo.Staffs");
            DropForeignKey("dbo.Records", "Assign_Id", "dbo.Assigns");
            DropForeignKey("dbo.Messages", "Assign_Id", "dbo.Assigns");
            DropForeignKey("dbo.Documents", "Assign_Id", "dbo.Assigns");
            DropForeignKey("dbo.Staffs", "Id", "dbo.Accounts");
            DropIndex("dbo.Tutors", new[] { "Id" });
            DropIndex("dbo.Students", new[] { "Id" });
            DropIndex("dbo.Records", new[] { "Assign_Id" });
            DropIndex("dbo.Messages", new[] { "Assign_Id" });
            DropIndex("dbo.Documents", new[] { "Assign_Id" });
            DropIndex("dbo.Assigns", new[] { "Tutor_Id" });
            DropIndex("dbo.Assigns", new[] { "Staff_Id" });
            DropIndex("dbo.Assigns", new[] { "Id" });
            DropIndex("dbo.Staffs", new[] { "Id" });
            DropTable("dbo.Tutors");
            DropTable("dbo.Students");
            DropTable("dbo.Records");
            DropTable("dbo.Messages");
            DropTable("dbo.Documents");
            DropTable("dbo.Assigns");
            DropTable("dbo.Staffs");
            DropTable("dbo.Accounts");
        }
    }
}
