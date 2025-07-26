using DesafioBTG.MS.Models;
using MongoDB.Driver;

namespace DesafioBTG.MS.Services
{
    public class MongoService
    {
        private readonly IMongoCollection<Pedido> _pedidoCollection;

        public MongoService(MongoDBSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _pedidoCollection = database.GetCollection<Pedido>(settings.CollectionName);
        }

        public void IncluirPedido(Pedido pedido)
        {
            try
            {
                _pedidoCollection.InsertOne(pedido);
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Pedido {pedido.CodigoPedido} salvo com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] Erro ao salvar pedido {pedido.CodigoPedido}: {ex.Message}");
            }
        }
    }
}
