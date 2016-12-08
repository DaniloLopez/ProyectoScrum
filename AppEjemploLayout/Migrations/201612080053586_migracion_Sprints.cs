namespace AppEjemploLayout.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migracion_Sprints : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sprints",
                c => new
                    {
                        SprintId = c.Int(nullable: false, identity: true),
                        duracion = c.Int(nullable: false),
                        estado = c.String(),
                        ProyectoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SprintId)
                .ForeignKey("dbo.Proyectos", t => t.ProyectoId, cascadeDelete: true)
                .Index(t => t.ProyectoId);
            
            CreateTable(
                "dbo.TareaSprints",
                c => new
                    {
                        TareaSprintId = c.Int(nullable: false, identity: true),
                        asunto = c.String(),
                        descripcion = c.String(),
                        estado = c.String(),
                        estimacionHoras = c.Int(nullable: false),
                        SprintId = c.Int(nullable: false),
                        UsuarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TareaSprintId)
                .ForeignKey("dbo.Sprints", t => t.SprintId, cascadeDelete: true)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.SprintId)
                .Index(t => t.UsuarioId);
            
            AddColumn("dbo.HistoriasUsuario", "Sprint_SprintId", c => c.Int());
            CreateIndex("dbo.HistoriasUsuario", "Sprint_SprintId");
            AddForeignKey("dbo.HistoriasUsuario", "Sprint_SprintId", "dbo.Sprints", "SprintId");
            DropTable("dbo.Registroes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Registroes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        correoElectronicoUsuario = c.String(nullable: false, maxLength: 100),
                        nombresUsuario = c.String(nullable: false, maxLength: 60),
                        apellidosUsuario = c.String(nullable: false, maxLength: 60),
                        aliasUsuario = c.String(maxLength: 30),
                        contraseñaUsuario = c.String(nullable: false, maxLength: 30),
                        ComparePass = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.TareaSprints", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.TareaSprints", "SprintId", "dbo.Sprints");
            DropForeignKey("dbo.Sprints", "ProyectoId", "dbo.Proyectos");
            DropForeignKey("dbo.HistoriasUsuario", "Sprint_SprintId", "dbo.Sprints");
            DropIndex("dbo.TareaSprints", new[] { "UsuarioId" });
            DropIndex("dbo.TareaSprints", new[] { "SprintId" });
            DropIndex("dbo.Sprints", new[] { "ProyectoId" });
            DropIndex("dbo.HistoriasUsuario", new[] { "Sprint_SprintId" });
            DropColumn("dbo.HistoriasUsuario", "Sprint_SprintId");
            DropTable("dbo.TareaSprints");
            DropTable("dbo.Sprints");
        }
    }
}
