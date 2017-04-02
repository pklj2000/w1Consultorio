namespace w1Consultorio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sistema1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sistema", "CodRefSistema", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sistema", "CodRefSistema", c => c.String(nullable: false, maxLength: 1));
        }
    }
}
