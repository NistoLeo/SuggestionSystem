using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SuggestionSystem.Application.Services;

namespace SuggestionSystem.Application
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection RegisterBusinessServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<NewIban>();
            services.AddSingleton(sp =>
            {
                var config = sp.GetRequiredService<IConfiguration>();
                var options = new AccountOptions
                {
                    InitialBalance = config.GetValue("AccountOptions:InitialBalance", 0)
                };
                return options;
            });
            return services;
        }
    }
}
