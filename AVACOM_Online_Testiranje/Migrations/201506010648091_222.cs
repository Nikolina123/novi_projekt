namespace AVACOM_Online_Testiranje.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _222 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TestPitanje", "PitanjeId", "dbo.Pitanje");
            DropForeignKey("dbo.TestPitanje", "TestId", "dbo.Test");
            DropIndex("dbo.TestPitanje", new[] { "TestId" });
            DropIndex("dbo.TestPitanje", new[] { "PitanjeId" });
            CreateTable(
                "dbo.KorisnikOdgovor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OdgovorId = c.Int(nullable: false),
                        TestOdgovorId = c.Int(nullable: false),
                        TestOdgovor_TestId = c.Int(),
                        TestOdgovor_PitanjeId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TestOdgovor", t => new { t.TestOdgovor_TestId, t.TestOdgovor_PitanjeId })
                .Index(t => new { t.TestOdgovor_TestId, t.TestOdgovor_PitanjeId });
            
            CreateTable(
                "dbo.TestOdgovor",
                c => new
                    {
                        TestId = c.Int(nullable: false),
                        PitanjeId = c.Int(nullable: false),
                        Id = c.Int(nullable: false),
                        OdgovorTacan = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => new { t.TestId, t.PitanjeId })
                .ForeignKey("dbo.Pitanje", t => t.PitanjeId)
                .ForeignKey("dbo.Test", t => t.TestId)
                .Index(t => t.TestId)
                .Index(t => t.PitanjeId);
            
            DropTable("dbo.TestPitanje");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TestPitanje",
                c => new
                    {
                        TestId = c.Int(nullable: false),
                        PitanjeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TestId, t.PitanjeId });
            
            DropForeignKey("dbo.KorisnikOdgovor", new[] { "TestOdgovor_TestId", "TestOdgovor_PitanjeId" }, "dbo.TestOdgovor");
            DropForeignKey("dbo.TestOdgovor", "TestId", "dbo.Test");
            DropForeignKey("dbo.TestOdgovor", "PitanjeId", "dbo.Pitanje");
            DropIndex("dbo.TestOdgovor", new[] { "PitanjeId" });
            DropIndex("dbo.TestOdgovor", new[] { "TestId" });
            DropIndex("dbo.KorisnikOdgovor", new[] { "TestOdgovor_TestId", "TestOdgovor_PitanjeId" });
            DropTable("dbo.TestOdgovor");
            DropTable("dbo.KorisnikOdgovor");
            CreateIndex("dbo.TestPitanje", "PitanjeId");
            CreateIndex("dbo.TestPitanje", "TestId");
            AddForeignKey("dbo.TestPitanje", "TestId", "dbo.Test", "Id");
            AddForeignKey("dbo.TestPitanje", "PitanjeId", "dbo.Pitanje", "Id");
        }
    }
}
