using Microsoft.Extensions.DependencyInjection;
using Tipi.Tools.Payments.Interfaces;

namespace Tipi.Tools.Payments.Config
{
    /// <summary>
    /// Static class to provide access to service registrations,
    /// <see href="https://docs.codingtipi.com/docs/toolkit/recurrente">See More</see>
    /// </summary>
    public static class ServiceRegistration
    {
        /// <summary>
        /// This method configures the <c>Recurrente</c> class for Dependency Injection inside .Net Core, 
        /// <see href="https://docs.codingtipi.com/docs/toolkit/recurrent">See More</see>.
        /// </summary>
        /// <remarks>
        /// Configure Dependency Ijection.
        /// </remarks>
        /// <param name="services">App Service collection.</param>
        /// <param name="pKey">Your Public Key provided by Recurrente.</param>
        /// <param name="sKey">Your Secret Key provided by Recurrente.</param>
        public static void ConfigureRecurrentePaymentGateway(this IServiceCollection services, string pKey, string sKey)
        {
            var authManager = new RecurrenteOptions(pKey, sKey);
            services.AddSingleton(authManager);
            services.AddTransient<IRecurrente, Recurrente>();
        }
    }
}
