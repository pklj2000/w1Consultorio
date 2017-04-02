namespace w1Consultorio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usuarioperfil : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.UsuarioPerfil",
                c => new
                    {
                        CodUsuario = c.String(nullable: false, maxLength: 10),
                        CodPerfil = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CodUsuario, t.CodPerfil })
                .ForeignKey("dbo.Usuario", t => t.CodUsuario, cascadeDelete: true)
                .ForeignKey("dbo.Perfil", t => t.CodPerfil, cascadeDelete: true)
                .Index(t => t.CodUsuario)
                .Index(t => t.CodPerfil);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.UsuarioPerfil", new[] { "CodPerfil" });
            DropIndex("dbo.UsuarioPerfil", new[] { "CodUsuario" });
            DropForeignKey("dbo.UsuarioPerfil", "CodPerfil", "dbo.Perfil");
            DropForeignKey("dbo.UsuarioPerfil", "CodUsuario", "dbo.Usuario");
            DropTable("dbo.UsuarioPerfil");
        }
    }
}
