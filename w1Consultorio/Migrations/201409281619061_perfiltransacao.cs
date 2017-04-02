namespace w1Consultorio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class perfiltransacao : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PerfilTransacao",
                c => new
                    {
                        CodPerfil = c.Int(nullable: false),
                        CodTransacao = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CodPerfil, t.CodTransacao })
                .ForeignKey("dbo.Perfil", t => t.CodPerfil, cascadeDelete: true)
                .ForeignKey("dbo.Transacao", t => t.CodTransacao, cascadeDelete: true)
                .Index(t => t.CodPerfil)
                .Index(t => t.CodTransacao);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.PerfilTransacao", new[] { "CodTransacao" });
            DropIndex("dbo.PerfilTransacao", new[] { "CodPerfil" });
            DropForeignKey("dbo.PerfilTransacao", "CodTransacao", "dbo.Transacao");
            DropForeignKey("dbo.PerfilTransacao", "CodPerfil", "dbo.Perfil");
            DropTable("dbo.PerfilTransacao");
        }
    }
}
