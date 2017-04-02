namespace w1Consultorio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class funcionario2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Funcionarios",
                c => new
                    {
                        CodFuncionario = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        CodCargo = c.Int(nullable: false),
                        CodEstadoCivil = c.Int(nullable: false),
                        CodSituacao = c.Int(nullable: false),
                        CodPeriodicidade = c.Int(nullable: false),
                        Rg = c.String(),
                        Sexo = c.String(),
                        CargoAnterior = c.String(),
                        Natural = c.String(),
                        Nacionalidade = c.String(),
                        DataNascimento = c.DateTime(),
                        DataAdmissao = c.DateTime(),
                        DataUltimoExame = c.DateTime(),
                        DataDemissao = c.DateTime(),
                        EstadoCivil_CodEstCivil = c.Int(),
                    })
                .PrimaryKey(t => t.CodFuncionario)
                .ForeignKey("dbo.Cargo", t => t.CodCargo)
                .ForeignKey("dbo.EstadoCivil", t => t.EstadoCivil_CodEstCivil)
                .ForeignKey("dbo.Periodicidades", t => t.CodPeriodicidade)
                .Index(t => t.CodCargo)
                .Index(t => t.EstadoCivil_CodEstCivil)
                .Index(t => t.CodPeriodicidade);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Funcionarios", new[] { "CodPeriodicidade" });
            DropIndex("dbo.Funcionarios", new[] { "EstadoCivil_CodEstCivil" });
            DropIndex("dbo.Funcionarios", new[] { "CodCargo" });
            DropForeignKey("dbo.Funcionarios", "CodPeriodicidade", "dbo.Periodicidades");
            DropForeignKey("dbo.Funcionarios", "EstadoCivil_CodEstCivil", "dbo.EstadoCivil");
            DropForeignKey("dbo.Funcionarios", "CodCargo", "dbo.Cargo");
            DropTable("dbo.Funcionarios");
        }
    }
}
