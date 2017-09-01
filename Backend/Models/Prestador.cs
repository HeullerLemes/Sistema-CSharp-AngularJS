using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace aulaWebApi.Models
{
    public class Prestador
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Username { get; set; }
        public string Senha { get; set; }
        public virtual List<Servico> Servicos { get; set; }
        public Prestador()
        {
            Servicos = new List<Servico>();
        }
        
    }
}