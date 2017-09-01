using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using aulaWebApi.Models;

namespace aulaWebApi.Controllers
{
    public class ServicoesController : ApiController
    {
        private aulaWebApiContext db = new aulaWebApiContext();

        // GET: api/Servicoes
        public IQueryable<Servico> GetServicoes()
        {
            return db.Servicoes;
        }

        // GET: api/Servicoes/5
        [ResponseType(typeof(Servico))]
        public IHttpActionResult GetServico(int id)
        {
            Servico servico = db.Servicoes.Find(id);
            if (servico == null)
            {
                return NotFound();
            }

            return Ok(servico);
        }

        // PUT: api/Servicoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutServico(int id, Servico servico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != servico.Id)
            {
                return BadRequest();
            }

            db.Entry(servico).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ServicoExists(id))
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

        // POST: api/Servicoes
        [ResponseType(typeof(Servico))]
        public IHttpActionResult PostServico(Servico servico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Servicoes.Add(servico);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = servico.Id }, servico);
        }

        // DELETE: api/Servicoes/5
        [ResponseType(typeof(Servico))]
        public IHttpActionResult DeleteServico(int id)
        {
            Servico servico = db.Servicoes.Find(id);
            if (servico == null)
            {
                return NotFound();
            }

            db.Servicoes.Remove(servico);
            db.SaveChanges();

            return Ok(servico);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ServicoExists(int id)
        {
            return db.Servicoes.Count(e => e.Id == id) > 0;
        }
    }
}