namespace ProvaTT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class criar_Cursos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Curso",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QuantidadeVagas = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id); 
        }
        
        public override void Down()
        {
            DropTable("dbo.Curso");
        }
    }
}
