using Microsoft.Extensions.DependencyInjection;
using Tipi.Tools.Payments.Interfaces;

namespace Tipi.Tools.Payments.Config
{
    public static class ServiceRegistration
    {
        public static void ConfigureRecurrentePaymentGateway(this IServiceCollection services, string pKey, string sKey)
        {
            var authManager = new RecurrenteOptions(pKey, sKey);
            services.AddSingleton(authManager);
            services.AddTransient<IRecurrente, Recurrente>();
        }
    }
}
