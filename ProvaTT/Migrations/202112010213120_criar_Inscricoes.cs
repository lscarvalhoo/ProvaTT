namespace ProvaTT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class criar_Inscricoes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Inscricao",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false),
                        CPF = c.String(nullable: false),
                        Email = c.String(nullable: false),
                        Telefone = c.String(nullable: false),
                        CursoId = c.Int(nullable: false),
                        UsuarioId = c.Int(nullable: false),
                        DataInscricao = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Curso", t => t.CursoId, cascadeDelete: true)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.CursoId)
                .Index(t => t.UsuarioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Inscricao", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.Inscricao", "CursoId", "dbo.Curso");
            DropIndex("dbo.Inscricao", new[] { "UsuarioId" });
            DropIndex("dbo.Inscricao", new[] { "CursoId" });
            DropTable("dbo.Inscricao");
        }
    }
}
