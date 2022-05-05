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
    }
}
