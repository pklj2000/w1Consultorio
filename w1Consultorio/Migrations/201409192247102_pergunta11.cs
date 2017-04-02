namespace w1Consultorio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pergunta11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Perguntas", "CodTipoResposta", c => c.Int(nullable: false));
            AddColumn("dbo.Perguntas", "RespostaObrigatoria", c => c.Int(nullable: false));
            AddColumn("dbo.Perguntas", "RespostaItem", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Perguntas", "RespostaItem");
            DropColumn("dbo.Perguntas", "RespostaObrigatoria");
            DropColumn("dbo.Perguntas", "CodTipoResposta");
        }
    }
}
