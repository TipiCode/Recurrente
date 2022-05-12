using Tipi.Tools.Payments.Models;

namespace Tipi.Tools.Payments.Interfaces
{
    /// <summary>
    /// Interface <c>IRecurrente</c> serves as an interace to implement the Class <c>Recurrente</c>,
    /// <see href="https://docs.codingtipi.com/docs/toolkit/recurrente">See More</see>
    /// </summary>
    /// <remarks>
    /// Exposes methods to interact with the payment gateway Recurernte.
    /// </remarks>
    public interface IRecurrente
    {
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
        Task<string> CreateClientAsync(string name, string email);
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
        Task<Checkout> CreateCheckoutAsync(string client_id, string price_id);
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
        Task<Checkout> CreateCheckoutAsync(string price_id);
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
        Task<Checkout> GetCheckoutAsync(string checkoutId);
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
        Task<Product> CreateProductAsync(Product product);
        /// <summary>
        /// This method gets a product by its Id, 
        /// <see href="https://docs.codingtipi.com/docs/toolkit/recurrente/methods#get-product-async">See More</see>.
        /// </summary>
        /// <param name="productId">Id of the product you want to search</param>
        /// <returns>
        /// Object of type <c>Product</c>.
        /// </returns>
        Task<Product> GetProductAsync(string productId);
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
        Task<Subscription> CreateSubscriptionAsync(Subscription subscription);
        /// <summary>
        /// This method gets a Subscription by its Id, 
        /// <see href="https://docs.codingtipi.com/docs/toolkit/recurrente/methods#get-subscription-async">See More</see>.
        /// </summary>
        /// <param name="subscriptionId">Id of the subscription you want to search</param>
        /// <returns>
        /// Object of type <c>Subscription</c>.
        /// </returns>
        Task<Subscription> GetSubscriptionAsync(string subscriptionId);
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
        Task<bool> DeleteItemAsync(string id);
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
        Task<ActiveSubscription> GetActiveSubscriptionAsync(string activeId);
        /// <summary>
        /// Gets the information of a payment method used on a checkout, 
        /// <see href="https://docs.codingtipi.com/docs/toolkit/recurrente/methods#get-payment-method-async">See More</see>.
        /// </summary>
        /// <remarks>
        /// Gets the information of a payment method by it's checkout.
        /// </remarks>
        /// <param name="checkoutId">Id of the checkout you need to retrieve the payment method from</param>
        /// <returns>
        /// <c>PaymentMethod</c> object containing the information of your payment method
        /// </returns>
        Task<PaymentMethod> GetPaymentMethodAsync(string checkoutId);
    }
}
