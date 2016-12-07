namespace AppEjemploLayout.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reinstall : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Productos",
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
                .ForeignKey("dbo.Productos", t => t.ProyectoId, cascadeDelete: true)
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
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ProyectoUsuarioRelacions", "UsuarioID", "dbo.Usuarios");
            DropForeignKey("dbo.ProyectoUsuarioRelacions", "ProyectoId", "dbo.Productos");
            DropForeignKey("dbo.HistoriasUsuario", "Proyecto_ProyectoId", "dbo.Productos");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.ProyectoUsuarioRelacions", new[] { "ProyectoId" });
            DropIndex("dbo.ProyectoUsuarioRelacions", new[] { "UsuarioID" });
            DropIndex("dbo.HistoriasUsuario", new[] { "Proyecto_ProyectoId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Registroes");
            DropTable("dbo.Usuarios");
            DropTable("dbo.ProyectoUsuarioRelacions");
            DropTable("dbo.HistoriasUsuario");
            DropTable("dbo.Productos");
        }
    }
}
