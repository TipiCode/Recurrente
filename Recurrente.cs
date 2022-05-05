using Tipi.Tools.Payments.Config;
using Tipi.Tools.Payments.Interfaces;
using Tipi.Tools.Http;
using Newtonsoft.Json;
using System.Net;
using Tipi.Tools.Payments.Dto;
using Tipi.Tools.Payments.Models;
using Tipi.Tools.Payments.Enums;

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
        /// This method creates a new Checkout, 
        /// <see href="https://docs.codingtipi.com/docs/toolkit/recurrente/methods#create-checkout-async">See More</see>.
        /// </summary>
        /// <remarks>
        /// Creates a new Checkout and returns its Checkout URL.
        /// </remarks>
        /// <param name="client_id">Client Id of the client you want to bill.</param>
        /// <param name="price_id">Price Id of the product you want to bill.</param>
        /// <returns>
        /// Returns an <c>Checkout</c> containing the Checkout URL and Checkout Id.
        /// </returns>
        public async Task<Checkout> CreateCheckoutAsync(string client_id, string price_id)
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
                if(checkout == null)
                    throw new NullReferenceException("The Checkout Response is null.");
                return new Checkout(checkout.id, checkout.checkout_url);
            }
            catch (Exception e)
            {
                throw new ApplicationException("An error ocurred creating the checkout", e);
            }
        }
        /// <summary>
        /// This method creates a new Checkout, 
        /// <see href="https://docs.codingtipi.com/docs/toolkit/recurrente/methods#create-checkout-async">See More</see>.
        /// </summary>
        /// <remarks>
        /// Creates a new Checkout and returns its Checkout URL.
        /// </remarks>
        /// <param name="price_id">Price Id of the product you want to bill.</param>
        /// <returns>
        /// Returns an <c>Checkout</c> containing the Checkout URL and Checkout Id.
        /// </returns>
        public async Task<Checkout> CreateCheckoutAsync(string price_id)
        {
            try
            {
                using var requestHandler = new HttpRequestHandler(_headers);
                var response = await requestHandler.ExecuteAsync("POST",
                    $"/checkouts",
                    JsonConvert.SerializeObject(new { items = new List<dynamic>() { new { price_id = price_id } } }));
                if (response.StatusCode != HttpStatusCode.Created)
                    throw new Exception($"An error ocurred with the API comunication, RESPONSE: {response.Body}");

                var checkout = JsonConvert.DeserializeObject<RecurrenteCheckout>(response.Body);
                if (checkout == null)
                    throw new NullReferenceException("The Checkout Response is null.");
                return new Checkout(checkout.id, checkout.checkout_url);
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

        /// <summary>
        /// This method creates a new product taking by parameter an object of type <c>Product</c> representing the object you want to create, 
        /// <see href="https://docs.codingtipi.com/docs/toolkit/recurrente/methods#create-product-async">See More</see>.
        /// </summary>
        /// <remarks>
        /// Creates a new Product and returns a <c>Product</c> object with the properties Id, Status and Storefront Link filled.
        /// </remarks>
        /// <param name="product">Object of type <c>Product</c> representing the object you want to create</param>
        /// <returns>
        /// Object of type <c>Product</c> with the properties Id, Status and Storefront Link filled.
        /// </returns>
        public async Task<Product> CreateProductAsync(Product product)
        {
            try
            {
                using var requestHandler = new HttpRequestHandler(_headers);
                var response = await requestHandler.ExecuteAsync("POST",
                    $"/products",
                    JsonConvert.SerializeObject(new { product = CleanProduct(product) }));
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception($"An error ocurred with the API comunication, RESPONSE: {response.Body}");

                var createdProduct = JsonConvert.DeserializeObject<dynamic>(response.Body);
                if (createdProduct == null)
                    throw new NullReferenceException("API Call returnes a null Body.");
                product.Id = createdProduct.id;
                product.Price.Id = createdProduct.prices[0].id;
                product.StoreUrl = createdProduct.storefront_link;
                product.Status = createdProduct.status;
                return product;
            }
            catch (Exception e)
            {
                throw new ApplicationException("An error ocurred creating the client", e);
            }
        }

        private dynamic CleanProduct(Product product)
        {
            var newProduct = new
            {
                name = product.Name,
                description = product.Description,
                phone_requirement = Enum.GetName(typeof(Requirements), product.PhoneRequirement) ?.ToLower(),
                address_requirement = Enum.GetName(typeof(Requirements), product.AddressRequirement) ?.ToLower(),
                billing_info_requirement = Enum.GetName(typeof(Requirements), product.BillingInfoRequirement) ?.ToLower(),
                image_url = product.Image,
                cancel_url = product.CancelUrl,
                cancelsuccess_url_url = product.SuccessUrl,
                prices_attributes = new List<dynamic>()
                    {
                        new
                        {
                            amount_as_decimal = product.Price.Amount.ToString(),
                            currency = Enum.GetName(typeof(Currency), product.Price.Currency),
                            charge_type = "one_time"
                        }
                    }
            };

            return newProduct;
        }
    }
}
