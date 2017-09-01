namespace aulaWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alt3 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Prestadors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Username = c.String(),
                        Senha = c.String(),
                        servico_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Servicoes", t => t.servico_Id)
                .Index(t => t.servico_Id);
            
            CreateTable(
                "dbo.Servicoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tipo = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Prestadors", "servico_Id", "dbo.Servicoes");
            DropIndex("dbo.Prestadors", new[] { "servico_Id" });
            DropTable("dbo.Servicoes");
            DropTable("dbo.Prestadors");
        }
    }
}
