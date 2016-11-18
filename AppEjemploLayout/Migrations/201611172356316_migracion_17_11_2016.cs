namespace AppEjemploLayout.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migracion_17_11_2016 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Registroes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        correoElectronicoUsuario = c.String(nullable: false),
                        nombresUsuario = c.String(nullable: false),
                        apellidosUsuario = c.String(nullable: false),
                        aliasUsuario = c.String(),
                        contraseñaUsuario = c.String(nullable: false, maxLength: 18),
                        ComparePass = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Registroes");
        }
    }
}
