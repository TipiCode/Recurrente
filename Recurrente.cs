using Recurrente.Config;
using Tipi.Tools.Interfaces;
using HttpRequestHandler.Utils;
using Newtonsoft.Json;
using System.Net;
using Recurrente.Models;

namespace Tipi.Tools
{
    internal class Recurrente : IRecurrente
    {
        private readonly RecurrenteOptions _options;
        private readonly Dictionary<string, string> _headers;
        public Recurrente(RecurrenteOptions options)
        {
            _options = options;
            _headers = new Dictionary<string, string>();
            _headers.Add("X-PUBLIC-KEY", options.PublicKey);
            _headers.Add("X-SECRET-KEY", options.SecretKey);
        }

        public async Task<string> CreateCheckoutAsync(string client_id, string price_id)
        {
            try
            {
                using var requestHandler = new HttpRequestHandler(headers:_headers);
                var response = await requestHandler.ExecuteAsync(HttpMethods.Post,
                    $"/checkouts",
                    JsonConvert.SerializeObject(new { items = new List<dynamic>() { new { price_id = price_id } }, user_id = client_id }));
                if (response.StatusCode != HttpStatusCode.Created)
                    throw new Exception($"An error ocurred with the API comunication, RESPONSE: {response.Body}");

                var checkout = JsonConvert.DeserializeObject<RecurrenteCheckout>(response.Body);
                return checkout == null ? "" : checkout.checkout_url;
            }
            catch (Exception e)
            {
                throw new ApplicationException("An error ocurred creating the client", e);
            }
        }

        public async Task<string> CreateCustomerAsync(string name, string email)
        {
            try
            {
                using var requestHandler = new HttpRequestHandler(headers: _headers);
                var response = await requestHandler.ExecuteAsync(HttpMethods.Post,
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
