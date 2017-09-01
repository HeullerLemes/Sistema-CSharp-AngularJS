namespace aulaWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alt2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Pedidoes", "Cliente_Id", "dbo.Clientes");
            DropForeignKey("dbo.ItemPedidoes", "Produto_Id", "dbo.Produtoes");
            DropForeignKey("dbo.ItemPedidoes", "Pedido_Id", "dbo.Pedidoes");
            DropIndex("dbo.Pedidoes", new[] { "Cliente_Id" });
            DropIndex("dbo.ItemPedidoes", new[] { "Produto_Id" });
            DropIndex("dbo.ItemPedidoes", new[] { "Pedido_Id" });
            AddColumn("dbo.Clientes", "Username", c => c.String());
            AddColumn("dbo.Clientes", "Senha", c => c.String());
            DropTable("dbo.Pedidoes");
            DropTable("dbo.ItemPedidoes");
            DropTable("dbo.Produtoes");
        }
        
        public override void Down()
        {
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
            
            CreateTable(
                "dbo.ItemPedidoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Quantidade = c.Int(nullable: false),
                        Produto_Id = c.Int(),
                        Pedido_Id = c.Int(),
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
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Clientes", "Senha");
            DropColumn("dbo.Clientes", "Username");
            CreateIndex("dbo.ItemPedidoes", "Pedido_Id");
            CreateIndex("dbo.ItemPedidoes", "Produto_Id");
            CreateIndex("dbo.Pedidoes", "Cliente_Id");
            AddForeignKey("dbo.ItemPedidoes", "Pedido_Id", "dbo.Pedidoes", "Id");
            AddForeignKey("dbo.ItemPedidoes", "Produto_Id", "dbo.Produtoes", "Id");
            AddForeignKey("dbo.Pedidoes", "Cliente_Id", "dbo.Clientes", "Id");
        }
    }
}
