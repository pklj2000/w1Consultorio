namespace w1Consultorio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Empresa",
                c => new
                    {
                        CodEmpresa = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Cidade = c.String(),
                        Estado = c.String(),
                        Ativo = c.String(),
                    })
                .PrimaryKey(t => t.CodEmpresa);
            
            CreateTable(
                "dbo.Departamento",
                c => new
                    {
                        CodDepartamento = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false),
                        Responsavel = c.String(),
                        CodEmpresa = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CodDepartamento)
                .ForeignKey("dbo.Empresa", t => t.CodEmpresa, cascadeDelete: true)
                .Index(t => t.CodEmpresa);
            
            CreateTable(
                "dbo.TipoExame",
                c => new
                    {
                        CodTipoExame = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.CodTipoExame);
            
            CreateTable(
                "dbo.EstadoCivil",
                c => new
                    {
                        CodEstCivil = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.CodEstCivil);
            
            CreateTable(
                "dbo.Risco",
                c => new
                    {
                        CodRisco = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.CodRisco);
            
            CreateTable(
                "dbo.Cargo",
                c => new
                    {
                        CodCargo = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false),
                        codDepartamento = c.Int(nullable: false),
                        codPeriodicidade = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CodCargo)
                .ForeignKey("dbo.Departamento", t => t.codDepartamento, cascadeDelete: true)
                .ForeignKey("dbo.Periodicidades", t => t.codPeriodicidade, cascadeDelete: true)
                .Index(t => t.codDepartamento)
                .Index(t => t.codPeriodicidade);
            
            CreateTable(
                "dbo.Periodicidades",
                c => new
                    {
                        CodPeriodicidade = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 50),
                        Dias = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CodPeriodicidade);
            
            CreateTable(
                "dbo.Exames",
                c => new
                    {
                        CodExame = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false, maxLength: 100),
                        EmiteSolExame = c.String(),
                        Ativo = c.String(),
                    })
                .PrimaryKey(t => t.CodExame);
            
            CreateTable(
                "dbo.ExamePeriodicidade",
                c => new
                    {
                        codExame = c.Int(nullable: false),
                        codPeriodicidade = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.codExame, t.codPeriodicidade })
                .ForeignKey("dbo.Exames", t => t.codExame, cascadeDelete: true)
                .ForeignKey("dbo.Periodicidades", t => t.codPeriodicidade, cascadeDelete: true)
                .Index(t => t.codExame)
                .Index(t => t.codPeriodicidade);
            
            CreateTable(
                "dbo.CargoExame",
                c => new
                    {
                        codCargo = c.Int(nullable: false),
                        codExame = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.codCargo, t.codExame })
                .ForeignKey("dbo.Cargo", t => t.codCargo, cascadeDelete: true)
                .ForeignKey("dbo.Exames", t => t.codExame, cascadeDelete: true)
                .Index(t => t.codCargo)
                .Index(t => t.codExame);
            
            CreateTable(
                "dbo.CargoRisco",
                c => new
                    {
                        codCargo = c.Int(nullable: false),
                        codRisco = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.codCargo, t.codRisco })
                .ForeignKey("dbo.Cargo", t => t.codCargo, cascadeDelete: true)
                .ForeignKey("dbo.Risco", t => t.codRisco, cascadeDelete: true)
                .Index(t => t.codCargo)
                .Index(t => t.codRisco);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.CargoRisco", new[] { "codRisco" });
            DropIndex("dbo.CargoRisco", new[] { "codCargo" });
            DropIndex("dbo.CargoExame", new[] { "codExame" });
            DropIndex("dbo.CargoExame", new[] { "codCargo" });
            DropIndex("dbo.ExamePeriodicidade", new[] { "codPeriodicidade" });
            DropIndex("dbo.ExamePeriodicidade", new[] { "codExame" });
            DropIndex("dbo.Cargo", new[] { "codPeriodicidade" });
            DropIndex("dbo.Cargo", new[] { "codDepartamento" });
            DropIndex("dbo.Departamento", new[] { "CodEmpresa" });
            DropForeignKey("dbo.CargoRisco", "codRisco", "dbo.Risco");
            DropForeignKey("dbo.CargoRisco", "codCargo", "dbo.Cargo");
            DropForeignKey("dbo.CargoExame", "codExame", "dbo.Exames");
            DropForeignKey("dbo.CargoExame", "codCargo", "dbo.Cargo");
            DropForeignKey("dbo.ExamePeriodicidade", "codPeriodicidade", "dbo.Periodicidades");
            DropForeignKey("dbo.ExamePeriodicidade", "codExame", "dbo.Exames");
            DropForeignKey("dbo.Cargo", "codPeriodicidade", "dbo.Periodicidades");
            DropForeignKey("dbo.Cargo", "codDepartamento", "dbo.Departamento");
            DropForeignKey("dbo.Departamento", "CodEmpresa", "dbo.Empresa");
            DropTable("dbo.CargoRisco");
            DropTable("dbo.CargoExame");
            DropTable("dbo.ExamePeriodicidade");
            DropTable("dbo.Exames");
            DropTable("dbo.Periodicidades");
            DropTable("dbo.Cargo");
            DropTable("dbo.Risco");
            DropTable("dbo.EstadoCivil");
            DropTable("dbo.TipoExame");
            DropTable("dbo.Departamento");
            DropTable("dbo.Empresa");
        }
    }
}
