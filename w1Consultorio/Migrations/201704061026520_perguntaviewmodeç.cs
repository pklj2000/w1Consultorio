namespace w1Consultorio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class perguntaviewmodeç : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Prontuarios", "Discriminator", c => c.String(nullable: false, maxLength: 128));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Prontuarios", "Discriminator");
        }
    }
}
