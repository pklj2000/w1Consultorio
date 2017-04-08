namespace w1Consultorio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class perguntas2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Respostas", "CodGrupo", c => c.Int(nullable: false));
            AlterColumn("dbo.Respostas", "GrupoSequencia", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Respostas", "GrupoSequencia", c => c.String());
            AlterColumn("dbo.Respostas", "CodGrupo", c => c.String());
        }
    }
}
