namespace AppEjemploLayout.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class exito : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TareaSprints", "UsuarioId", "dbo.Usuarios");
            DropIndex("dbo.TareaSprints", new[] { "UsuarioId" });
            RenameColumn(table: "dbo.HistoriasUsuario", name: "Sprint_SprintId", newName: "SprintId");
            RenameIndex(table: "dbo.HistoriasUsuario", name: "IX_Sprint_SprintId", newName: "IX_SprintId");
            AlterColumn("dbo.TareaSprints", "UsuarioId", c => c.Int());
            CreateIndex("dbo.TareaSprints", "UsuarioId");
            AddForeignKey("dbo.TareaSprints", "UsuarioId", "dbo.Usuarios", "UsuarioId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TareaSprints", "UsuarioId", "dbo.Usuarios");
            DropIndex("dbo.TareaSprints", new[] { "UsuarioId" });
            AlterColumn("dbo.TareaSprints", "UsuarioId", c => c.Int(nullable: false));
            RenameIndex(table: "dbo.HistoriasUsuario", name: "IX_SprintId", newName: "IX_Sprint_SprintId");
            RenameColumn(table: "dbo.HistoriasUsuario", name: "SprintId", newName: "Sprint_SprintId");
            CreateIndex("dbo.TareaSprints", "UsuarioId");
            AddForeignKey("dbo.TareaSprints", "UsuarioId", "dbo.Usuarios", "UsuarioId", cascadeDelete: true);
        }
    }
}
