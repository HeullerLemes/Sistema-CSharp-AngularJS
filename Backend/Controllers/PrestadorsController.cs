using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using aulaWebApi.Authorization;
using aulaWebApi.Models;
using Newtonsoft.Json.Linq;

namespace aulaWebApi.Controllers
{
    public class PrestadorsController : ApiController
    {
        private aulaWebApiContext db = new aulaWebApiContext();

        // GET: api/Prestadors
        public IHttpActionResult GetPrestadors()
        {

            return Ok(db.Prestadors.ToList());
        }

        // GET: api/Prestadors/5
        [ResponseType(typeof(Prestador))]
        public IHttpActionResult GetPrestador(int id)
        {
            Prestador prestador = db.Prestadors.Find(id);
            if (prestador == null)
            {
                return NotFound();
            }

            return Ok(prestador);
        }

        // PUT: api/Prestadors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPrestador(int id, Prestador prestador)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != prestador.Id)
            {
                return BadRequest();
            }

            db.Entry(prestador).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrestadorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Prestadors
        [ResponseType(typeof(Prestador))]
        public IHttpActionResult PostPrestador(JObject prestador)
        {
            /*
            if (prestador.Equals("none"))
            {
                return BadRequest(ModelState);
            }
            */

            Prestador Prestador = new Prestador();
            Prestador.Nome = prestador["nomeCompleto"].ToString();
            Prestador.Username = prestador["username"].ToString();
            Prestador.Senha = prestador["senha"].ToString();
            JArray array = (JArray) prestador["selecionados"];
            foreach (JObject i in array)
            {
                Servico Servico = db.Servicoes.Find(int.Parse(i["Id"].ToString()));
                
                Prestador.Servicos.Add(Servico);
                
            }

            db.Prestadors.Add(Prestador);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Prestador.Id }, prestador);
        }

        // DELETE: api/Prestadors/5
        [ResponseType(typeof(Prestador))]
        public IHttpActionResult DeletePrestador(int id)
        {
            Prestador prestador = db.Prestadors.Find(id);
            if (prestador == null)
            {
                return NotFound();
            }

            db.Prestadors.Remove(prestador);
            db.SaveChanges();

            return Ok(prestador);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PrestadorExists(int id)
        {
            return db.Prestadors.Count(e => e.Id == id) > 0;
        }
    }
}