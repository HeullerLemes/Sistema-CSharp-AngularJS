namespace aulaWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alt4 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Prestadors", "servico_Id", "dbo.Servicoes");
            DropIndex("dbo.Prestadors", new[] { "servico_Id" });
            CreateTable(
                "dbo.PrestadorServicoes",
                c => new
                    {
                        Prestador_Id = c.Int(nullable: false),
                        Servico_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Prestador_Id, t.Servico_Id })
                .ForeignKey("dbo.Prestadors", t => t.Prestador_Id, cascadeDelete: true)
                .ForeignKey("dbo.Servicoes", t => t.Servico_Id, cascadeDelete: true)
                .Index(t => t.Prestador_Id)
                .Index(t => t.Servico_Id);
            
            DropColumn("dbo.Prestadors", "servico_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Prestadors", "servico_Id", c => c.Int());
            DropForeignKey("dbo.PrestadorServicoes", "Servico_Id", "dbo.Servicoes");
            DropForeignKey("dbo.PrestadorServicoes", "Prestador_Id", "dbo.Prestadors");
            DropIndex("dbo.PrestadorServicoes", new[] { "Servico_Id" });
            DropIndex("dbo.PrestadorServicoes", new[] { "Prestador_Id" });
            DropTable("dbo.PrestadorServicoes");
            CreateIndex("dbo.Prestadors", "servico_Id");
            AddForeignKey("dbo.Prestadors", "servico_Id", "dbo.Servicoes", "Id");
        }
    }
}
