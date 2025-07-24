using DesafioBTG.API.Models;
using MongoDB.Driver;

namespace DesafioBTG.API.Services
{
    public class PedidoService
    {
        private readonly IMongoCollection<Pedido> _pedidos;

        public PedidoService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["MongoDB:ConnectionString"]);
            var database = client.GetDatabase(configuration["MongoDB:DatabaseName"]);
            _pedidos = database.GetCollection<Pedido>("Pedidos");
        }
        public async Task InserirPedido(Pedido pedido)
        {
            await _pedidos.InsertOneAsync(pedido);
        }

        public async Task<List<Pedido>> ListarPedidosPorCliente(int codigoCliente)
        {
            return await _pedidos.Find(p => p.CodigoCliente == codigoCliente).ToListAsync();
        }
    }
}
