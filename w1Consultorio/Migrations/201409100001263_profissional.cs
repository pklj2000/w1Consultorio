namespace w1Consultorio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class profissional : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Profissional",
                c => new
                    {
                        CodProfissional = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        CRM = c.String(),
                        Telefone = c.String(),
                        Ativo = c.String(nullable: false),
                        Observacao = c.String(),
                    })
                .PrimaryKey(t => t.CodProfissional);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Profissional");
        }
    }
}
