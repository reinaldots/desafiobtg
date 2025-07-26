using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace DesafioBTG.API.Models
{
    public class Pedido
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonIgnore]
        public string? Id { get; set; }
        public int CodigoPedido { get; set; }
        public int CodigoCliente { get; set; }
        public List<Item>? Itens { get; set; }
    }

    public class Item
    {
        public required string Produto { get; set; }
        public int Quantidade { get; set; }
        public double Preco { get; set; }
    }
}
