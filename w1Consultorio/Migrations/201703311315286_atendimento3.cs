namespace w1Consultorio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class atendimento3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Atendimento", "Ativo", c => c.Boolean(nullable: false));
            AddColumn("dbo.Atendimento", "dataAtendimento", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Atendimento", "dataAtendimento");
            DropColumn("dbo.Atendimento", "Ativo");
        }
    }
}
