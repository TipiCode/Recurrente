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
                if (string.IsNullOrEmpty(client_id))
                    throw new ArgumentNullException("The Client Id cannot be null or empty.");
                if (string.IsNullOrEmpty(price_id))
                    throw new ArgumentNullException("The price Id cannot be null or empty.");
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
                if (string.IsNullOrEmpty(price_id))
                    throw new ArgumentNullException("The price Id cannot be null or empty.");
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
                if (string.IsNullOrEmpty(name))
                    throw new ArgumentNullException("The client name cannot be null or empty.");
                if (string.IsNullOrEmpty(email))
                    throw new ArgumentNullException("The client email cannot be null or empty.");
                using var requestHandler = new HttpRequestHandler(_headers);
                var response = await requestHandler.ExecuteAsync("POST",
                    $"/users",
                    JsonConvert.SerializeObject(new { email = email, full_name = name }));
                if (response.StatusCode != HttpStatusCode.Created)
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
                if (product == null)
                    throw new ArgumentNullException("The Product cannot be null.");
                using var requestHandler = new HttpRequestHandler(_headers);
                var response = await requestHandler.ExecuteAsync("POST",
                    $"/products",
                    JsonConvert.SerializeObject(new { product = CleanItem(product) }));
                if (response.StatusCode != HttpStatusCode.Created)
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
        /// <summary>
        /// This method creates a new recurring product taking by parameter an object of type <c>Subscription</c> representing the object you want to create, 
        /// <see href="https://docs.codingtipi.com/docs/toolkit/recurrente/methods#create-subscription-async">See More</see>.
        /// </summary>
        /// <remarks>
        /// Creates a new Product and returns a <c>Subscription</c> object with the properties Id, Status and Storefront Link filled.
        /// </remarks>
        /// <param name="subscription">Object of type <c>Subscription</c> representing the object you want to create</param>
        /// <returns>
        /// Object of type <c>Subscription</c> with the properties Id, Status and Storefront Link filled.
        /// </returns>
        public async Task<Subscription> CreateSubscriptionAsync(Subscription subscription)
        {
            try
            {
                if (subscription ==  null)
                    throw new ArgumentNullException("The Subscription cannot be null.");
                using var requestHandler = new HttpRequestHandler(_headers);
                var response = await requestHandler.ExecuteAsync("POST",
                    $"/products",
                    JsonConvert.SerializeObject(new { product = CleanItem(subscription) }));
                if (response.StatusCode != HttpStatusCode.Created)
                    throw new Exception($"An error ocurred with the API comunication, RESPONSE: {response.Body}");

                var createdSubscription = JsonConvert.DeserializeObject<dynamic>(response.Body);
                if (createdSubscription == null)
                    throw new NullReferenceException("API Call returnes a null Body.");
                subscription.Id = createdSubscription.id;
                subscription.Price.Id = createdSubscription.prices[0].id;
                subscription.StoreUrl = createdSubscription.storefront_link;
                subscription.Status = createdSubscription.status;
                return subscription;
            }
            catch (Exception e)
            {
                throw new ApplicationException("An error ocurred creating the client", e);
            }
        }
        /// <summary>
        /// Deletes a product or subscription, 
        /// <see href="https://docs.codingtipi.com/docs/toolkit/recurrente/methods#delete-item-async">See More</see>.
        /// </summary>
        /// <remarks>
        /// Deletes either a <c>Subscription</c> or a <c>Product</c>.
        /// </remarks>
        /// <param name="id">Id of the item you intend to delete</param>
        /// <returns>
        /// True if the object was correctly deleted
        /// </returns>
        public async Task<bool> DeleteItemAsync(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                    throw new ArgumentNullException("The Item Id cannot be null or empty.");
                using var requestHandler = new HttpRequestHandler(_headers);
                var response = await requestHandler.ExecuteAsync("DELETE",
                    $"/products/{id}");
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception($"An error ocurred with the API comunication, RESPONSE: {response.Body}");

                return true;
            }
            catch (Exception e)
            {
                throw new ApplicationException("An error ocurred creating the client", e);
            }
        }
        /// <summary>
        /// Gets the information about an active subscription, 
        /// <see href="https://docs.codingtipi.com/docs/toolkit/recurrente/methods#get-active-subscription-async">See More</see>.
        /// </summary>
        /// <remarks>
        /// Gets the information about an active subscription.
        /// </remarks>
        /// <param name="activeId">Id of the active subscription you want to retrieve</param>
        /// <returns>
        /// <c>ActiveSubscription</c> object containing the information of your subscription
        /// </returns>
        public async Task<ActiveSubscription> GetActiveSubscriptionAsync(string activeId)
        {
            try
            {
                if (string.IsNullOrEmpty(activeId))
                    throw new ArgumentNullException("The Item Id cannot be null or empty.");
                using var requestHandler = new HttpRequestHandler(_headers);
                var response = await requestHandler.ExecuteAsync("DELETE",
                    $"/subscriptions/{activeId}");
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception($"An error ocurred with the API comunication, RESPONSE: {response.Body}");

                var activeSubscription = JsonConvert.DeserializeObject<dynamic>(response.Body);
                if (activeSubscription == null)
                    throw new NullReferenceException("The Checkout Response is null.");
                return new ActiveSubscription()
                {
                    Id = activeSubscription.id,
                    Status = activeSubscription.status,
                    Started = new DateTime(activeSubscription.current_period_start),
                    NextBilling = new DateTime(activeSubscription.current_period_end),
                    UserId = activeSubscription.subscriber.id,
                    Email = activeSubscription.subscriber.email,
                    ProductId = activeSubscription.product.id,
                };
            }
            catch (Exception e)
            {
                throw new ApplicationException("An error ocurred creating the client", e);
            }
        }

        /// <summary>
        /// This method Gets an existing checkout, 
        /// <see href="https://docs.codingtipi.com/docs/toolkit/recurrente/methods#get-checkout-async">See More</see>.
        /// </summary>
        /// <remarks>
        /// Gets a checkout by its Id.
        /// </remarks>
        /// <param name="checkoutId">Checkout Id.</param>
        /// <returns>
        /// Returns an <c>Checkout</c> containing the Checkout URL and Checkout Id.
        /// </returns>
        public async Task<Checkout> GetCheckoutAsync(string checkoutId)
        {
            try
            {
                if (string.IsNullOrEmpty(checkoutId))
                    throw new ArgumentNullException("The checkout Id cannot be null or empty.");
                using var requestHandler = new HttpRequestHandler(_headers);
                var response = await requestHandler.ExecuteAsync("GET",
                    $"/checkouts/{checkoutId}");
                if (response.StatusCode != HttpStatusCode.OK)
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
        /// This method gets a product by its Id, 
        /// <see href="https://docs.codingtipi.com/docs/toolkit/recurrente/methods#get-product-async">See More</see>.
        /// </summary>
        /// <param name="productId">Id of the product you want to search</param>
        /// <returns>
        /// Object of type <c>Product</c>.
        /// </returns>
        public async Task<Product> GetProductAsync(string productId)
        {
            try
            {
                if (string.IsNullOrEmpty(productId))
                    throw new ArgumentNullException("The Product Id cannot be null or empty.");
                using var requestHandler = new HttpRequestHandler(_headers);
                var response = await requestHandler.ExecuteAsync("GET",
                    $"/products/{productId}");
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception($"An error ocurred with the API comunication, RESPONSE: {response.Body}");

                var product = JsonConvert.DeserializeObject<dynamic>(response.Body);
                if (product == null)
                    throw new NullReferenceException("API Call returnes a null Body.");

                return BuildProduct(product);
            }
            catch (Exception e)
            {
                throw new ApplicationException("An error ocurred creating the client", e);
            }
        }
        /// <summary>
        /// This method gets a Subscription by its Id, 
        /// <see href="https://docs.codingtipi.com/docs/toolkit/recurrente/methods#get-subscription-async">See More</see>.
        /// </summary>
        /// <param name="subscriptionId">Id of the subscription you want to search</param>
        /// <returns>
        /// Object of type <c>Subscription</c>.
        /// </returns>
        public async Task<Subscription> GetSubscriptionAsync(string subscriptionId)
        {
            try
            {
                if (string.IsNullOrEmpty(subscriptionId))
                    throw new ArgumentNullException("The Subscription Id cannot be null or empty.");
                using var requestHandler = new HttpRequestHandler(_headers);
                var response = await requestHandler.ExecuteAsync("GET",
                    $"/products/{subscriptionId}");
                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception($"An error ocurred with the API comunication, RESPONSE: {response.Body}");

                var product = JsonConvert.DeserializeObject<dynamic>(response.Body);
                if (product == null)
                    throw new NullReferenceException("API Call returnes a null Body.");

                return BuildProduct(product);
            }
            catch (Exception e)
            {
                throw new ApplicationException("An error ocurred creating the client", e);
            }
        }

        private Product BuildProduct(dynamic response)
        {
            return new Product(
                new SinglePrice()
                {
                    Id = response.prices[0].id,
                    Amount = double.Parse(response.prices[0].amount_in_cents),
                    Currency = Enum.Parse(typeof(Currency), response.prices[0].currency)
                })
            {
                Id = response.id,
                Name = response.Name,
                Description = response.description.body,
                PhoneRequirement = Enum.Parse(typeof(Requirements), response.phone_requirement),
                AddressRequirement = Enum.Parse(typeof(Requirements), response.address_requirement),
                BillingInfoRequirement = Enum.Parse(typeof(Requirements), response.billing_info_requirement),
                Status = response.status,
                CancelUrl = response.cancel_url,
                SuccessUrl = response.success_url,
                StoreUrl = response.storefront_link
            };
        }

        private Subscription BuildSubscription(dynamic response)
        {
            return new Subscription(
                new RecurringPrice()
                {
                    Id = response.prices[0].id,
                    Amount = double.Parse(response.prices[0].amount_in_cents),
                    Currency = Enum.Parse(typeof(Currency), response.prices[0].currency),
                    Interval = Enum.Parse(typeof(BillingInterval), response.prices[0].billing_interval, true),
                    IntervalCount = response.prices[0].billing_interval_count,
                    CancellationInterval = response.prices[0].periods_before_automatic_cancellation
                })
            {
                Id = response.id,
                Name = response.Name,
                Description = response.description.body,
                PhoneRequirement = Enum.Parse(typeof(Requirements), response.phone_requirement),
                AddressRequirement = Enum.Parse(typeof(Requirements), response.address_requirement),
                BillingInfoRequirement = Enum.Parse(typeof(Requirements), response.billing_info_requirement),
                Status = response.status,
                CancelUrl = response.cancel_url,
                SuccessUrl = response.success_url,
                StoreUrl = response.storefront_link
            };
        }

        private dynamic CleanItem(Product item)
        {
            var newProduct = new
            {
                name = item.Name,
                description = item.Description,
                phone_requirement = Enum.GetName(typeof(Requirements), item.PhoneRequirement) ?.ToLower(),
                address_requirement = Enum.GetName(typeof(Requirements), item.AddressRequirement) ?.ToLower(),
                billing_info_requirement = Enum.GetName(typeof(Requirements), item.BillingInfoRequirement) ?.ToLower(),
                image_url = item.Image,
                cancel_url = item.CancelUrl,
                cancelsuccess_url_url = item.SuccessUrl,
                prices_attributes = new List<dynamic>()
                    {
                        new
                        {
                            amount_as_decimal = item.Price.Amount.ToString(),
                            currency = Enum.GetName(typeof(Currency), item.Price.Currency),
                            charge_type = "one_time"
                        }
                    }
            };

            return newProduct;
        }

        private dynamic CleanItem(Subscription item)
        {
            var newProduct = new
            {
                name = item.Name,
                description = item.Description,
                phone_requirement = Enum.GetName(typeof(Requirements), item.PhoneRequirement)?.ToLower(),
                address_requirement = Enum.GetName(typeof(Requirements), item.AddressRequirement)?.ToLower(),
                billing_info_requirement = Enum.GetName(typeof(Requirements), item.BillingInfoRequirement)?.ToLower(),
                image_url = item.Image,
                cancel_url = item.CancelUrl,
                cancelsuccess_url_url = item.SuccessUrl,
                prices_attributes = new List<dynamic>()
                    {
                        new
                        {
                            amount_as_decimal = item.Price.Amount.ToString(),
                            currency = Enum.GetName(typeof(Currency), item.Price.Currency),
                            charge_type = "recurring",
                            billing_interval_count = item.Price.IntervalCount.ToString(),
                            billing_interval = Enum.GetName(typeof(BillingInterval), item.Price.Interval)?.ToLower(),
                            free_trial_interval_count = item.Price.FreeTrialIntervalCount.ToString(),
                            free_trial_interval = Enum.GetName(typeof(BillingInterval), item.Price.FreeTrialInterval)?.ToLower(),
                            periods_before_automatic_cancellation = item.Price.CancellationInterval
                        }
                    }
            };

            return newProduct;
        }
    }
}
