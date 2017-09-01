namespace aulaWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pedidoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Data = c.DateTime(nullable: false),
                        Cliente_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clientes", t => t.Cliente_Id)
                .Index(t => t.Cliente_Id);
            
            CreateTable(
                "dbo.ItemPedidoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantidade = c.Int(nullable: false),
                        Produto_Id = c.Int(),
                        Pedido_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Produtoes", t => t.Produto_Id)
                .ForeignKey("dbo.Pedidoes", t => t.Pedido_Id)
                .Index(t => t.Produto_Id)
                .Index(t => t.Pedido_Id);
            
            CreateTable(
                "dbo.Produtoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Descricao = c.String(),
                        Preco = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ItemPedidoes", "Pedido_Id", "dbo.Pedidoes");
            DropForeignKey("dbo.ItemPedidoes", "Produto_Id", "dbo.Produtoes");
            DropForeignKey("dbo.Pedidoes", "Cliente_Id", "dbo.Clientes");
            DropIndex("dbo.ItemPedidoes", new[] { "Pedido_Id" });
            DropIndex("dbo.ItemPedidoes", new[] { "Produto_Id" });
            DropIndex("dbo.Pedidoes", new[] { "Cliente_Id" });
            DropTable("dbo.Produtoes");
            DropTable("dbo.ItemPedidoes");
            DropTable("dbo.Pedidoes");
            DropTable("dbo.Clientes");
        }
    }
}
