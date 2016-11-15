namespace AppEjemploLayout.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migracion1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UsuarioProyectoes", "Usuario_correoElectronicoUsuario", "dbo.Usuarios");
            DropForeignKey("dbo.UsuarioProyectoes", "Proyecto_ProyectoId", "dbo.Proyectoes");
            DropIndex("dbo.UsuarioProyectoes", new[] { "Usuario_correoElectronicoUsuario" });
            DropIndex("dbo.UsuarioProyectoes", new[] { "Proyecto_ProyectoId" });
            CreateTable(
                "dbo.ProyectoUsuarioRelacions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        correoElectronicoUsuario = c.String(maxLength: 128),
                        ProyectoId = c.Int(nullable: false),
                        rolUsuario = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Proyectoes", t => t.ProyectoId, cascadeDelete: true)
                .ForeignKey("dbo.Usuarios", t => t.correoElectronicoUsuario)
                .Index(t => t.correoElectronicoUsuario)
                .Index(t => t.ProyectoId);
            
            DropTable("dbo.UsuarioProyectoes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UsuarioProyectoes",
                c => new
                    {
                        Usuario_correoElectronicoUsuario = c.String(nullable: false, maxLength: 128),
                        Proyecto_ProyectoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Usuario_correoElectronicoUsuario, t.Proyecto_ProyectoId });
            
            DropForeignKey("dbo.ProyectoUsuarioRelacions", "correoElectronicoUsuario", "dbo.Usuarios");
            DropForeignKey("dbo.ProyectoUsuarioRelacions", "ProyectoId", "dbo.Proyectoes");
            DropIndex("dbo.ProyectoUsuarioRelacions", new[] { "ProyectoId" });
            DropIndex("dbo.ProyectoUsuarioRelacions", new[] { "correoElectronicoUsuario" });
            DropTable("dbo.ProyectoUsuarioRelacions");
            CreateIndex("dbo.UsuarioProyectoes", "Proyecto_ProyectoId");
            CreateIndex("dbo.UsuarioProyectoes", "Usuario_correoElectronicoUsuario");
            AddForeignKey("dbo.UsuarioProyectoes", "Proyecto_ProyectoId", "dbo.Proyectoes", "ProyectoId", cascadeDelete: true);
            AddForeignKey("dbo.UsuarioProyectoes", "Usuario_correoElectronicoUsuario", "dbo.Usuarios", "correoElectronicoUsuario", cascadeDelete: true);
        }
    }
}
