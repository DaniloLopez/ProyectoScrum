namespace AppEjemploLayout.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _7_12_2016_234 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProyectoUsuarioRelacions", "rolUsuario", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProyectoUsuarioRelacions", "rolUsuario", c => c.String());
        }
    }
}
