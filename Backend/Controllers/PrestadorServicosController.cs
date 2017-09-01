using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using aulaWebApi.Models;
using Newtonsoft.Json.Linq;

namespace aulaWebApi.Controllers
{
    public class PrestadorServicosController: ApiController
    {
        private aulaWebApiContext db = new aulaWebApiContext();
        [HttpPost]
        public IHttpActionResult GetPrestadores(JObject servico)
        {
            string Id = servico["Id"].ToString();
            
            List<Prestador> prestadores = new List<Prestador>();
            foreach (var p in db.Prestadors.ToList())
            {
                foreach (var s in p.Servicos)
                {
                    if (s.Id.ToString().Equals(Id))
                    {
                        
                        prestadores.Add(p);
                    }
                    
                }
                
            }
            
            /*
            var pretador = db.Prestadors.Where(d => d.Servicos.Contains(Id));
            */
            return Ok(prestadores);
        } 
    }
}