namespace w1Consultorio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class atendimento2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Atendimento", "codEmpresa", c => c.Int(nullable: false));
            AddForeignKey("dbo.Atendimento", "codEmpresa", "dbo.Empresa", "CodEmpresa", cascadeDelete: true);
            CreateIndex("dbo.Atendimento", "codEmpresa");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Atendimento", new[] { "codEmpresa" });
            DropForeignKey("dbo.Atendimento", "codEmpresa", "dbo.Empresa");
            DropColumn("dbo.Atendimento", "codEmpresa");
        }
    }
}
