namespace AppEjemploLayout.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class d_20112016_1034 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Proyectoes", newName: "Productos");
            CreateTable(
                "dbo.HistoriasUsuario",
                c => new
                    {
                        HistoriaUsuarioId = c.Int(nullable: false, identity: true),
                        nombre = c.String(nullable: false),
                        contexto = c.String(nullable: false),
                        descripcion = c.String(nullable: false),
                        prioridad = c.Int(nullable: false),
                        ezfuerzo = c.Int(nullable: false),
                        Proyecto_ProyectoId = c.Int(),
                    })
                .PrimaryKey(t => t.HistoriaUsuarioId)
                .ForeignKey("dbo.Productos", t => t.Proyecto_ProyectoId)
                .Index(t => t.Proyecto_ProyectoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HistoriasUsuario", "Proyecto_ProyectoId", "dbo.Productos");
            DropIndex("dbo.HistoriasUsuario", new[] { "Proyecto_ProyectoId" });
            DropTable("dbo.HistoriasUsuario");
            RenameTable(name: "dbo.Productos", newName: "Proyectoes");
        }
    }
}
