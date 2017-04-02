namespace w1Consultorio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class all : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Atendimento",
                c => new
                    {
                        codAtendimento = c.Int(nullable: false, identity: true),
                        codFuncionario = c.Int(nullable: false),
                        codTipoExame = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.codAtendimento);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Atendimento");
        }
    }
}
