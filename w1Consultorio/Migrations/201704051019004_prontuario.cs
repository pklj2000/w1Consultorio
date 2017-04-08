namespace w1Consultorio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prontuario : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Prontuarios",
                c => new
                    {
                        CodProntuario = c.Int(nullable: false, identity: true),
                        CodTipoExame = c.Int(nullable: false),
                        CodEmpresa = c.Int(nullable: false),
                        CodDepartamento = c.Int(nullable: false),
                        CodCargo = c.Int(nullable: false),
                        CodFuncionario = c.Int(nullable: false),
                        dataExame = c.Int(nullable: false),
                        ExameClinico = c.Int(nullable: false),
                        ExameResultado = c.Int(nullable: false),
                        codProfissional = c.Int(nullable: false),
                        EstadoCivil = c.Int(nullable: false),
                        Aso = c.Int(nullable: false),
                        AsoObservacao = c.String(),
                        CodResultadoClinico = c.Int(nullable: false),
                        CodResultadoProntuario = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CodProntuario)
                .ForeignKey("dbo.Empresa", t => t.CodEmpresa)
                .ForeignKey("dbo.Departamento", t => t.CodDepartamento)
                .ForeignKey("dbo.Cargo", t => t.CodCargo)
                .ForeignKey("dbo.Funcionarios", t => t.CodFuncionario)
                .ForeignKey("dbo.Profissional", t => t.codProfissional)
                .Index(t => t.CodEmpresa)
                .Index(t => t.CodDepartamento)
                .Index(t => t.CodCargo)
                .Index(t => t.CodFuncionario)
                .Index(t => t.codProfissional);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Prontuarios", new[] { "codProfissional" });
            DropIndex("dbo.Prontuarios", new[] { "CodFuncionario" });
            DropIndex("dbo.Prontuarios", new[] { "CodCargo" });
            DropIndex("dbo.Prontuarios", new[] { "CodDepartamento" });
            DropIndex("dbo.Prontuarios", new[] { "CodEmpresa" });
            DropForeignKey("dbo.Prontuarios", "codProfissional", "dbo.Profissional");
            DropForeignKey("dbo.Prontuarios", "CodFuncionario", "dbo.Funcionarios");
            DropForeignKey("dbo.Prontuarios", "CodCargo", "dbo.Cargo");
            DropForeignKey("dbo.Prontuarios", "CodDepartamento", "dbo.Departamento");
            DropForeignKey("dbo.Prontuarios", "CodEmpresa", "dbo.Empresa");
            DropTable("dbo.Prontuarios");
        }
    }
}
