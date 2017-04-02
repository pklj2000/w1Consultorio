namespace w1Consultorio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pergunta1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Perguntas",
                c => new
                    {
                        CodPergunta = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false),
                        Sequencia = c.Int(nullable: false),
                        Ativo = c.String(nullable: false, maxLength: 1),
                        CodPerguntaGrupo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CodPergunta)
                .ForeignKey("dbo.PerguntaGrupo", t => t.CodPerguntaGrupo, cascadeDelete: true)
                .Index(t => t.CodPerguntaGrupo);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Perguntas", new[] { "CodPerguntaGrupo" });
            DropForeignKey("dbo.Perguntas", "CodPerguntaGrupo", "dbo.PerguntaGrupo");
            DropTable("dbo.Perguntas");
        }
    }
}
