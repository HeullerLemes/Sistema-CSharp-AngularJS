using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aulaWebApi.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }    
        public string Username { get; set; }
        public string Senha { get; set; }
    }   
}