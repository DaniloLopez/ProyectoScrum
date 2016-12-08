namespace AppEjemploLayout.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class exito : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HistoriasUsuario",
                c => new
                    {
                        HistoriaUsuarioId = c.Int(nullable: false, identity: true),
                        SprintId = c.Int(),
                        ProyectoId = c.Int(nullable: false),
                        nombre = c.String(nullable: false),
                        contexto = c.String(nullable: false),
                        descripcion = c.String(nullable: false),
                        prioridad = c.Int(nullable: false),
                        ezfuerzo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.HistoriaUsuarioId)
                .ForeignKey("dbo.Proyectos", t => t.ProyectoId, cascadeDelete: true)
                .ForeignKey("dbo.Sprints", t => t.SprintId)
                .Index(t => t.SprintId)
                .Index(t => t.ProyectoId);
            
            CreateTable(
                "dbo.Proyectos",
                c => new
                    {
                        ProyectoId = c.Int(nullable: false, identity: true),
                        nombreProyecto = c.String(nullable: false),
                        descripcionProyecto = c.String(),
                        fechaInicioProyecto = c.DateTime(nullable: false),
                        fechaFinalizacionProyecto = c.DateTime(nullable: false),
                        estadoProyecto = c.String(),
                    })
                .PrimaryKey(t => t.ProyectoId);
            
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
                "dbo.ProyectoUsuarioRelacions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UsuarioID = c.Int(nullable: false),
                        ProyectoId = c.Int(nullable: false),
                        rolUsuario = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Proyectos", t => t.ProyectoId, cascadeDelete: true)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioID, cascadeDelete: true)
                .Index(t => t.UsuarioID)
                .Index(t => t.ProyectoId);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        UsuarioId = c.Int(nullable: false, identity: true),
                        correoElectronicoUsuario = c.String(nullable: false),
                        nombresUsuario = c.String(nullable: false),
                        apellidosUsuario = c.String(nullable: false),
                        aliasUsuario = c.String(),
                        contraseñaUsuario = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.UsuarioId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
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
                        UsuarioId = c.Int(),
                    })
                .PrimaryKey(t => t.TareaSprintId)
                .ForeignKey("dbo.Sprints", t => t.SprintId, cascadeDelete: true)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioId)
                .Index(t => t.SprintId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.TareaSprints", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.TareaSprints", "SprintId", "dbo.Sprints");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ProyectoUsuarioRelacions", "UsuarioID", "dbo.Usuarios");
            DropForeignKey("dbo.ProyectoUsuarioRelacions", "ProyectoId", "dbo.Proyectos");
            DropForeignKey("dbo.Sprints", "ProyectoId", "dbo.Proyectos");
            DropForeignKey("dbo.HistoriasUsuario", "SprintId", "dbo.Sprints");
            DropForeignKey("dbo.HistoriasUsuario", "ProyectoId", "dbo.Proyectos");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.TareaSprints", new[] { "UsuarioId" });
            DropIndex("dbo.TareaSprints", new[] { "SprintId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ProyectoUsuarioRelacions", new[] { "ProyectoId" });
            DropIndex("dbo.ProyectoUsuarioRelacions", new[] { "UsuarioID" });
            DropIndex("dbo.Sprints", new[] { "ProyectoId" });
            DropIndex("dbo.HistoriasUsuario", new[] { "ProyectoId" });
            DropIndex("dbo.HistoriasUsuario", new[] { "SprintId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.TareaSprints");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Usuarios");
            DropTable("dbo.ProyectoUsuarioRelacions");
            DropTable("dbo.Sprints");
            DropTable("dbo.Proyectos");
            DropTable("dbo.HistoriasUsuario");
        }
    }
}
