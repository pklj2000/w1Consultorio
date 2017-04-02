namespace w1Consultorio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class transacao2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Transacao", "CodModulo", c => c.Int(nullable: false));
            AddForeignKey("dbo.Transacao", "CodModulo", "dbo.Moduloes", "CodModulo");
            CreateIndex("dbo.Transacao", "CodModulo");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Transacao", new[] { "CodModulo" });
            DropForeignKey("dbo.Transacao", "CodModulo", "dbo.Moduloes");
            DropColumn("dbo.Transacao", "CodModulo");
        }
    }
}
