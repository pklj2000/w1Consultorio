namespace w1Consultorio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class empresa_chg1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Funcionarios", "CodEmpresa", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Funcionarios", "CodEmpresa");
        }
    }
}
