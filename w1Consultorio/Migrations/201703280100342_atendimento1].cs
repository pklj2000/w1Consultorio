namespace w1Consultorio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class atendimento1 : DbMigration
    {
        public override void Up()
        {
            AddForeignKey("dbo.Atendimento", "codFuncionario", "dbo.Funcionarios", "CodFuncionario", cascadeDelete: true);
            AddForeignKey("dbo.Atendimento", "codTipoExame", "dbo.TipoExame", "CodTipoExame", cascadeDelete: true);
            CreateIndex("dbo.Atendimento", "codFuncionario");
            CreateIndex("dbo.Atendimento", "codTipoExame");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Atendimento", new[] { "codTipoExame" });
            DropIndex("dbo.Atendimento", new[] { "codFuncionario" });
            DropForeignKey("dbo.Atendimento", "codTipoExame", "dbo.TipoExame");
            DropForeignKey("dbo.Atendimento", "codFuncionario", "dbo.Funcionarios");
        }
    }
}
