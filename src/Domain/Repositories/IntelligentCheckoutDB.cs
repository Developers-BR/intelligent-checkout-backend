using MongoDB.Driver;

namespace IntelligentCheckout.Backend.Domain.Repositories
{
    internal class IntelligentCheckoutDB
    {
        public IMongoCollection<Models.Pessoa> Pessoas { get; }
        public IMongoCollection<Models.Produto> Produtos { get; }
        public IMongoCollection<Models.Compra> Compras { get; }
        public IMongoCollection<Models.Carrinho> Carrinhos { get; }

        public IntelligentCheckoutDB(string connectionString)
        {
            var mongoClient = new MongoClient(connectionString);
            var mongoDatabase = mongoClient.GetDatabase("IntelligentCheckout");
            this.Pessoas = mongoDatabase.GetCollection<Models.Pessoa>("Pessoas");
            this.Produtos = mongoDatabase.GetCollection<Models.Produto>("Produtos");
            this.Compras = mongoDatabase.GetCollection<Models.Compra>("Compras");
            this.Carrinhos = mongoDatabase.GetCollection<Models.Carrinho>("Carrinhos");
        }
    }
}
