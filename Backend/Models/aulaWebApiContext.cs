using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace aulaWebApi.Models
{
    public class aulaWebApiContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public aulaWebApiContext() : base("name=aulaWebApiContext")
        {
        }
        public DbSet<Cliente> Clientes { get; set; }

        public System.Data.Entity.DbSet<aulaWebApi.Models.Prestador> Prestadors { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Prestador>()
                        .HasMany<Servico>(s => s.Servicos)
                        .WithMany()
                        .Map(cs =>
                        {
                        });

        }
        
        public System.Data.Entity.DbSet<aulaWebApi.Models.Servico> Servicoes { get; set; }
        public System.Data.Entity.DbSet<aulaWebApi.Models.PrestacaoServico> PrestacaoServicos { get; set; }
    }
}
