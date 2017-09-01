namespace aulaWebApi.Models
{
    public class ItemPedido
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
        public virtual Produto Produto { get; set; }
    }
}