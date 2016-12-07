namespace AppEjemploLayout.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migracionViweModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HistoriasUsuario", "Proyecto_ProyectoId", "dbo.Proyectos");
            DropIndex("dbo.HistoriasUsuario", new[] { "Proyecto_ProyectoId" });
            RenameColumn(table: "dbo.HistoriasUsuario", name: "Proyecto_ProyectoId", newName: "ProyectoId");
            AlterColumn("dbo.HistoriasUsuario", "ProyectoId", c => c.Int(nullable: false));
            CreateIndex("dbo.HistoriasUsuario", "ProyectoId");
            AddForeignKey("dbo.HistoriasUsuario", "ProyectoId", "dbo.Proyectos", "ProyectoId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HistoriasUsuario", "ProyectoId", "dbo.Proyectos");
            DropIndex("dbo.HistoriasUsuario", new[] { "ProyectoId" });
            AlterColumn("dbo.HistoriasUsuario", "ProyectoId", c => c.Int());
            RenameColumn(table: "dbo.HistoriasUsuario", name: "ProyectoId", newName: "Proyecto_ProyectoId");
            CreateIndex("dbo.HistoriasUsuario", "Proyecto_ProyectoId");
            AddForeignKey("dbo.HistoriasUsuario", "Proyecto_ProyectoId", "dbo.Proyectos", "ProyectoId");
        }
    }
}
