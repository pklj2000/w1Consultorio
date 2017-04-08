namespace w1Consultorio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prontuario4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prontuarios", "CodAtendimento", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Prontuarios", "CodAtendimento");
        }
    }
}
