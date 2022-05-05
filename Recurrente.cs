using Tipi.Tools.Payments.Config;
using Tipi.Tools.Payments.Interfaces;
using Tipi.Tools.Http;
using Newtonsoft.Json;
using System.Net;
using Tipi.Tools.Payments.Dto;

namespace Tipi.Tools.Payments
{
    /// <summary>
    /// Class <c>Recurrente</c> serves as an interace to interact with the Payment Gateway Recurrente,
    /// <see href="https://docs.codingtipi.com/docs/toolkit/recurrente">See More</see>
    /// </summary>
    /// <remarks>
    /// Exposes methods to interact with the payment gateway Recurernte.
    /// </remarks>
    public class Recurrente : IRecurrente
    {
        private readonly Dictionary<string, string> _headers;
        /// <summary>
        /// This constructor initializes a new Default <c>Recurrente</c> class taking by parameter an object of type <c>RecurrenteOptions</c>, 
        /// <see href="https://docs.codingtipi.com/docs/toolkit/recurrente/constructors#recurrente-options">See More</see>.
        /// <param name="options">Configuration needed to initialize the class, <see href="https://docs.codingtipi.com/docs/toolkit/recurrente/classes#recurrente-options">See More</see>.</param>
        /// </summary>
        public Recurrente(RecurrenteOptions options)
        {
            _headers = new Dictionary<string, string>();
            _headers.Add("X-PUBLIC-KEY", options.PublicKey);
            _headers.Add("X-SECRET-KEY", options.SecretKey);
        }
        /// <summary>
        /// This method creates a new Checkout taking the clientId and the PriceId of a product, 
        /// <see href="https://docs.codingtipi.com/docs/toolkit/recurrente/methods#create-checkout-async">See More</see>.
        /// </summary>
        /// <remarks>
        /// Creates a new Checkout and returns its Checkout URL.
        /// </remarks>
        /// <param name="client_id">Client Id of the client you want to bill.</param>
        /// <param name="price_id">Price Id of the product you want to bill.</param>
        /// <returns>
        /// Returns an <c>String</c> containing the Checkout URL.
        /// </returns>
        public async Task<string> CreateCheckoutAsync(string client_id, string price_id)
        {
            try
            {
                using var requestHandler = new HttpRequestHandler(_headers);
                var response = await requestHandler.ExecuteAsync("POST",
                    $"/checkouts",
                    JsonConvert.SerializeObject(new { items = new List<dynamic>() { new { price_id = price_id } }, user_id = client_id }));
                if (response.StatusCode != HttpStatusCode.Created)
                    throw new Exception($"An error ocurred with the API comunication, RESPONSE: {response.Body}");

                var checkout = JsonConvert.DeserializeObject<RecurrenteCheckout>(response.Body);
                return checkout == null ? "" : checkout.checkout_url;
            }
            catch (Exception e)
            {
                throw new ApplicationException("An error ocurred creating the checkout", e);
            }
        }
        /// <summary>
        /// This method creates a new Client taking the Full name and an Email, if the client already exists it will also return the client Id, 
        /// <see href="https://docs.codingtipi.com/docs/toolkit/recurrente/methods#create-client-async">See More</see>.
        /// </summary>
        /// <remarks>
        /// Creates a new Client and returns its client Id.
        /// </remarks>
        /// <param name="name">Full name of the new client.</param>
        /// <param name="email">Email address for the new client.</param>
        /// <returns>
        /// Returns an <c>String</c> containing the Client Id.
        /// </returns>
        public async Task<string> CreateClientAsync(string name, string email)
        {
            try
            {
                using var requestHandler = new HttpRequestHandler(_headers);
                var response = await requestHandler.ExecuteAsync("POST",
                    $"/users",
                    JsonConvert.SerializeObject(new { email = email, full_name = name }));
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception($"An error ocurred with the API comunication, RESPONSE: {response.Body}");

                var customer = JsonConvert.DeserializeObject<RecurrenteClient>(response.Body);
                return customer == null ? "" : customer.id;
            }
            catch (Exception e)
            {
                throw new ApplicationException("An error ocurred creating the client", e);
            }
        }
    }
}
