using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IntelligentCheckout.Backend.Setup
{
    internal static class Repositories
    {
        public static void Add(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(i => new Domain.Repositories.IntelligentCheckoutDB(configuration["ConnectionString"]));

            services.AddScoped<Domain.Repositories.IPessoasLeitura, Domain.Repositories.Pessoas>();
            services.AddScoped<Domain.Repositories.IPessoasEscrita, Domain.Repositories.Pessoas>();

            services.AddScoped<Domain.Repositories.IProdutosLeitura, Domain.Repositories.Produtos>();
            services.AddScoped<Domain.Repositories.IProdutosEscrita, Domain.Repositories.Produtos>();

            services.AddScoped<Domain.Repositories.ICarrinhosLeitura, Domain.Repositories.Carrinhos>();
            services.AddScoped<Domain.Repositories.ICarrinhosEscrita, Domain.Repositories.Carrinhos>();
            
            services.AddScoped<Domain.Repositories.IComprasLeitura, Domain.Repositories.Compras>();
            services.AddScoped<Domain.Repositories.IComprasEscrita, Domain.Repositories.Compras>();
        }
    }
}
