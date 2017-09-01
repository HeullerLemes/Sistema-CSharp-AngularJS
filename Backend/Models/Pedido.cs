using System;
using System.Collections.Generic;

namespace aulaWebApi.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public virtual Cliente Cliente { get; set; }
        public DateTime Data { get; set; }
        public List<ItemPedido> Itens { get; set; }

        public Pedido()
        {
            Itens = new List<ItemPedido>();
        }
    }
}