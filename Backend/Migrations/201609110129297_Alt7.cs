namespace aulaWebApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Alt7 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Servicoes", "nome");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Servicoes", "nome", c => c.String());
        }
    }
}
