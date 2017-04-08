namespace w1Consultorio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pertunta1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Respostas", "CodGrupo", c => c.String());
            AddColumn("dbo.Respostas", "GrupoSequencia", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Respostas", "GrupoSequencia");
            DropColumn("dbo.Respostas", "CodGrupo");
        }
    }
}
