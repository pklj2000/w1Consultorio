namespace w1Consultorio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class perguntagrupo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PerguntaGrupo",
                c => new
                    {
                        CodPerguntaGrupo = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false),
                        Sequencia = c.Int(nullable: false),
                        Ativo = c.String(nullable: false, maxLength: 1),
                    })
                .PrimaryKey(t => t.CodPerguntaGrupo);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PerguntaGrupo");
        }
    }
}
