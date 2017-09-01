namespace aulaWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alt6 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PrestacaoServicoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataPrestacao = c.DateTime(nullable: false),
                        prestador_Id = c.Int(),
                        servico_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Prestadors", t => t.prestador_Id)
                .ForeignKey("dbo.Servicoes", t => t.servico_Id)
                .Index(t => t.prestador_Id)
                .Index(t => t.servico_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PrestacaoServicoes", "servico_Id", "dbo.Servicoes");
            DropForeignKey("dbo.PrestacaoServicoes", "prestador_Id", "dbo.Prestadors");
            DropIndex("dbo.PrestacaoServicoes", new[] { "servico_Id" });
            DropIndex("dbo.PrestacaoServicoes", new[] { "prestador_Id" });
            DropTable("dbo.PrestacaoServicoes");
        }
    }
}
