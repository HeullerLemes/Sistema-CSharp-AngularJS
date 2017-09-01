namespace aulaWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alt5 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Servicoes", "DataServico", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Servicoes", "DataServico");
        }
    }
}
