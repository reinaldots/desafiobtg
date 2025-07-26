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
            _pedidos = database.GetCollection<Pedido>(configuration["MongoDB:CollectionName"]);
        }

        public async Task IncluirPedido(Pedido pedido)
        {
            if (pedido == null)
                throw new ArgumentNullException(nameof(pedido));

            try
            {
                //incluir pedido direto no banco de dados
                await _pedidos.InsertOneAsync(pedido);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Erro ao incluir pedido no banco de dados.", ex);
            }
        }

        public async Task<List<Pedido>> ListarPedidosPorCliente(int codigoCliente)
        {
            return await _pedidos.Find(p => p.CodigoCliente == codigoCliente).ToListAsync();
        }

        public async Task<double> ObterValorTotalDoPedido(int codigoPedido)
        {
            var pedido = await _pedidos.Find(p => p.CodigoPedido == codigoPedido).FirstOrDefaultAsync();

            if (pedido == null || pedido.Itens == null || !pedido.Itens.Any())
                return 0;

            return Math.Round(pedido.Itens.Sum(i => i.Preco * i.Quantidade), 2);
        }

        public async Task<long> ObterQuantidadePedido(int codigoCliente)
        {
            return await _pedidos.CountDocumentsAsync(p => p.CodigoCliente == codigoCliente);
        }
    }
}
