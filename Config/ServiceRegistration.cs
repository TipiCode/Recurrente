using Microsoft.Extensions.DependencyInjection;
using Tipi.Tools.Interfaces;

namespace Recurrente.Config
{
    public static class ServiceRegistration
    {
        public static void ConfigureRecurrente(this IServiceCollection services, string pKey, string sKey)
        {
            var authManager = new RecurrenteOptions(pKey, sKey);
            services.AddSingleton(authManager);
            services.AddTransient<IRecurrente, Tipi.Tools.Recurrente>();
        }
    }
}
