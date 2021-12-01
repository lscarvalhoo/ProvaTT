namespace ProvaTT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class criar_RegistroAcesso : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RegistroAcesso",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataAcesso = c.DateTime(nullable: false),
                        UsuarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.UsuarioId); 
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RegistroAcesso", "UsuarioId", "dbo.Usuario");
            DropIndex("dbo.RegistroAcesso", new[] { "UsuarioId" });
            DropTable("dbo.RegistroAcesso");
        }
    }
}
