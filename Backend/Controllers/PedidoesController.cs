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
    public class PedidoesController : ApiController
    {
        private aulaWebApiContext db = new aulaWebApiContext();

        // GET: api/Pedidoes
        public IQueryable<Pedido> GetPedidos()
        {
            return db.Pedidos;
        }

        // GET: api/Pedidoes/5
        [ResponseType(typeof(Pedido))]
        public IHttpActionResult GetPedido(int id)
        {
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return NotFound();
            }

            return Ok(pedido);
        }

        // POST: api/Pedidoes
        [ResponseType(typeof(Pedido))]
        public IHttpActionResult PostPedido(JObject param)
        {
            Cliente c = null;
            try
            {
                int clienteId = (int)param["id"];
                c = db.Clientes.SingleOrDefault(x => x.Id == clienteId);
            }
            catch { }

            if (c == null)
                return BadRequest("Cliente não encontrado");
            JArray arr = (JArray)param["itens"];
            if (arr == null || arr.Count == 0)
                return BadRequest("Sem produtos no pedido");

            Pedido ped = new Pedido { Data = DateTime.Now, Cliente = c };

            foreach (JObject obj in arr)
            {
                Produto p = null;
                try
                {
                    int idProd = (int)obj["id"];
                    p = db.Produtoes.SingleOrDefault(x => x.Id == idProd);
                }
                catch { }

                if (p == null)
                    return BadRequest("Produto não encontrado");
                try
                {
                    int qtd = (int)obj["qtd"];
                    ItemPedido i = new ItemPedido { Produto = p, Quantidade = qtd };
                    ped.Itens.Add(i);
                }
                catch
                {
                    return BadRequest("Quantidade Inválida");
                }
            }

            db.Pedidos.Add(ped);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = ped.Id }, ped);
        }

        // DELETE: api/Pedidoes/5
        [ResponseType(typeof(Pedido))]
        public IHttpActionResult DeletePedido(int id)
        {
            Pedido pedido = db.Pedidos.Find(id);
            if (pedido == null)
            {
                return NotFound();
            }

            db.Pedidos.Remove(pedido);
            db.SaveChanges();

            return Ok(pedido);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PedidoExists(int id)
        {
            return db.Pedidos.Count(e => e.Id == id) > 0;
        }
    }
}