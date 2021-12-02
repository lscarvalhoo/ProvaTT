namespace ProvaTT.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adicionando_Usuario : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT dbo.Usuario (Login, Senha, Nome) VALUES ('Leandro', '123', 'Leandro Cesar')");
        }
        
        public override void Down()
        {
        }
    }
}
