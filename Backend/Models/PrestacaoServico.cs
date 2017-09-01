using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aulaWebApi.Models
{
    public class PrestacaoServico
    {
        public int Id { get; set; }
        public DateTime DataPrestacao { get; set; }
        public Prestador prestador { get; set; }
        public Servico servico { get; set; }
    }
}