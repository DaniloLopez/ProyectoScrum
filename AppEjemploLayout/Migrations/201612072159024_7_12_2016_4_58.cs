namespace AppEjemploLayout.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _7_12_2016_4_58 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Proyectos", newName: "Productos");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Productos", newName: "Proyectos");
        }
    }
}
