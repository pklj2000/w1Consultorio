namespace w1Consultorio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class funcionario5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Funcionarios", "EstadoCivil_CodEstCivil", "dbo.EstadoCivil");
            DropIndex("dbo.Funcionarios", new[] { "EstadoCivil_CodEstCivil" });
            RenameColumn(table: "dbo.Funcionarios", name: "EstadoCivil_CodEstCivil", newName: "CodEstCivil");
            AddForeignKey("dbo.Funcionarios", "CodEstCivil", "dbo.EstadoCivil", "CodEstCivil", cascadeDelete: true);
            CreateIndex("dbo.Funcionarios", "CodEstCivil");
            DropColumn("dbo.Funcionarios", "CodEstadoCivil");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Funcionarios", "CodEstadoCivil", c => c.Int(nullable: false));
            DropIndex("dbo.Funcionarios", new[] { "CodEstCivil" });
            DropForeignKey("dbo.Funcionarios", "CodEstCivil", "dbo.EstadoCivil");
            RenameColumn(table: "dbo.Funcionarios", name: "CodEstCivil", newName: "EstadoCivil_CodEstCivil");
            CreateIndex("dbo.Funcionarios", "EstadoCivil_CodEstCivil");
            AddForeignKey("dbo.Funcionarios", "EstadoCivil_CodEstCivil", "dbo.EstadoCivil", "CodEstCivil");
        }
    }
}
