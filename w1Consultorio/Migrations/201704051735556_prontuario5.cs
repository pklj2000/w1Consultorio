namespace w1Consultorio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prontuario5 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Prontuarios", "dataExame", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Prontuarios", "dataExame", c => c.Int(nullable: false));
        }
    }
}
