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
using Newtonsoft.Json.Linq;

namespace aulaWebApi.Controllers
{
    public class PrestacaoServicoesController : ApiController
    {
        private aulaWebApiContext db = new aulaWebApiContext();

        // GET: api/PrestacaoServicoes
        public IQueryable<PrestacaoServico> GetPrestacaoServicos()
        {
            return db.PrestacaoServicos;
        }

        // GET: api/PrestacaoServicoes/5
        [ResponseType(typeof(PrestacaoServico))]
        public IHttpActionResult GetPrestacaoServico(int id)
        {
            PrestacaoServico prestacaoServico = db.PrestacaoServicos.Find(id);
            if (prestacaoServico == null)
            {
                return NotFound();
            }

            return Ok(prestacaoServico);
        }

        // PUT: api/PrestacaoServicoes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPrestacaoServico(int id, PrestacaoServico prestacaoServico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != prestacaoServico.Id)
            {
                return BadRequest();
            }

            db.Entry(prestacaoServico).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrestacaoServicoExists(id))
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

        // POST: api/PrestacaoServicoes
        [ResponseType(typeof(PrestacaoServico))]
        public IHttpActionResult PostPrestacaoServico(JObject prestacaoServico)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //db.PrestacaoServicos.Add(prestacaoServico);
            //db.SaveChanges();

            return null; //CreatedAtRoute("DefaultApi", new { id = prestacaoServico.Id }, prestacaoServico);
        }

        // DELETE: api/PrestacaoServicoes/5
        [ResponseType(typeof(PrestacaoServico))]
        public IHttpActionResult DeletePrestacaoServico(int id)
        {
            PrestacaoServico prestacaoServico = db.PrestacaoServicos.Find(id);
            if (prestacaoServico == null)
            {
                return NotFound();
            }

            db.PrestacaoServicos.Remove(prestacaoServico);
            db.SaveChanges();

            return Ok(prestacaoServico);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PrestacaoServicoExists(int id)
        {
            return db.PrestacaoServicos.Count(e => e.Id == id) > 0;
        }
    }
}