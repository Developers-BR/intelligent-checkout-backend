using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace IntelligentCheckout.Backend.Setup
{
    public class Services
    {
        public static void Add(IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton(i =>
            {
                var chave = configuration["FaceAPI:Chave"];
                var endpoint = configuration["FaceAPI:Endpoint"];
                var personGroupId = configuration["FaceAPI:PersonGroupId"];
                var configuracao = new Domain.Services.Pessoas.Configuracoes(chave, endpoint, personGroupId);
                return configuracao;
            });

            services.AddSingleton(i =>
            {
                var tokenEndpoint = configuration["Blockchain:TokenEndpoint"];
                var tokenGrantType = configuration["Blockchain:TokenGrantType"];
                var tokenClientId = configuration["Blockchain:TokenClientId"];
                var tokenClientSecret = configuration["Blockchain:TokenClientSecret"];
                var tokenResource = configuration["Blockchain:TokenResource"];
                var contractEndpoint = configuration["Blockchain:ContractEndpoint"];
                var configuracao = new Domain.Services.Blockchain.Configuracoes(tokenEndpoint, tokenGrantType, tokenClientId, tokenClientSecret, tokenResource, contractEndpoint);
                return configuracao;
            });

            services.AddScoped<Domain.Services.IPessoas, Domain.Services.Pessoas>();
            services.AddScoped<Domain.Services.ICarrinhos, Domain.Services.Carrinhos>();
            services.AddScoped<Domain.Services.ICompras, Domain.Services.Compras>();
            services.AddScoped<Domain.Services.IBlockchain, Domain.Services.Blockchain>();
        }
    }
}
