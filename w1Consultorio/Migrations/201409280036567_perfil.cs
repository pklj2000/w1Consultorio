namespace w1Consultorio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class perfil : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Perfil",
                c => new
                    {
                        CodPerfil = c.Int(nullable: false, identity: true),
                        Descricao = c.String(nullable: false),
                        Ativo = c.String(nullable: false),
                        CodModulo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CodPerfil)
                .ForeignKey("dbo.Moduloes", t => t.CodModulo, cascadeDelete: true)
                .Index(t => t.CodModulo);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Perfil", new[] { "CodModulo" });
            DropForeignKey("dbo.Perfil", "CodModulo", "dbo.Moduloes");
            DropTable("dbo.Perfil");
        }
    }
}
