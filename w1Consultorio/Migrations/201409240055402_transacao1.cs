namespace w1Consultorio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class transacao1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Transacao",
                c => new
                    {
                        CodTransacao = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false),
                        Janela = c.String(nullable: false),
                        Objeto = c.String(),
                        Ativo = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.CodTransacao);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Transacao");
        }
    }
}
