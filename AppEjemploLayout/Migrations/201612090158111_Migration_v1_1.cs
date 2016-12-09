namespace AppEjemploLayout.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration_v1_1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TareaSprints", "asunto", c => c.String(nullable: false));
            AlterColumn("dbo.TareaSprints", "descripcion", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TareaSprints", "descripcion", c => c.String());
            AlterColumn("dbo.TareaSprints", "asunto", c => c.String());
        }
    }
}
