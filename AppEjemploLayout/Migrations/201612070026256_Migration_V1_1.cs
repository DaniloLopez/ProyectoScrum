namespace AppEjemploLayout.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Migration_V1_1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ProyectoUsuarioRelacions", "correoElectronicoUsuario", "dbo.Usuarios");
            DropIndex("dbo.ProyectoUsuarioRelacions", new[] { "correoElectronicoUsuario" });
            RenameColumn(table: "dbo.ProyectoUsuarioRelacions", name: "correoElectronicoUsuario", newName: "UsuarioID");
            DropPrimaryKey("dbo.Usuarios");
            AddColumn("dbo.Usuarios", "UsuarioId", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.ProyectoUsuarioRelacions", "UsuarioID", c => c.Int(nullable: false));
            AlterColumn("dbo.Usuarios", "correoElectronicoUsuario", c => c.String(nullable: false));
            AddPrimaryKey("dbo.Usuarios", "UsuarioId");
            CreateIndex("dbo.ProyectoUsuarioRelacions", "UsuarioID");
            AddForeignKey("dbo.ProyectoUsuarioRelacions", "UsuarioID", "dbo.Usuarios", "UsuarioId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProyectoUsuarioRelacions", "UsuarioID", "dbo.Usuarios");
            DropIndex("dbo.ProyectoUsuarioRelacions", new[] { "UsuarioID" });
            DropPrimaryKey("dbo.Usuarios");
            AlterColumn("dbo.Usuarios", "correoElectronicoUsuario", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.ProyectoUsuarioRelacions", "UsuarioID", c => c.String(maxLength: 128));
            DropColumn("dbo.Usuarios", "UsuarioId");
            AddPrimaryKey("dbo.Usuarios", "correoElectronicoUsuario");
            RenameColumn(table: "dbo.ProyectoUsuarioRelacions", name: "UsuarioID", newName: "correoElectronicoUsuario");
            CreateIndex("dbo.ProyectoUsuarioRelacions", "correoElectronicoUsuario");
            AddForeignKey("dbo.ProyectoUsuarioRelacions", "correoElectronicoUsuario", "dbo.Usuarios", "correoElectronicoUsuario");
        }
    }
}
