namespace w1Consultorio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modulo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Moduloes",
                c => new
                    {
                        CodModulo = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        Descricao = c.String(),
                        CodSistema = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CodModulo)
                .ForeignKey("dbo.Sistema", t => t.CodSistema, cascadeDelete: true)
                .Index(t => t.CodSistema);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Moduloes", new[] { "CodSistema" });
            DropForeignKey("dbo.Moduloes", "CodSistema", "dbo.Sistema");
            DropTable("dbo.Moduloes");
        }
    }
}
