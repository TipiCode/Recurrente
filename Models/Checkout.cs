namespace Tipi.Tools.Payments.Models
{
    /// <summary>
    /// Class representing the Checkout object, 
    /// <see href="https://docs.codingtipi.com/docs/toolkit/recurrente/classes#checkout">See More</see>
    /// </summary>
    public class Checkout
    {
        /// <summary>
        /// Your Checkout Id.
        /// </summary>
        public string Id { get; } = default!;
        /// <summary>
        /// Your Checkout Url.
        /// </summary>
        public string Url { get; } = default!;
        /// <summary>
        /// Constructor to initialize the <c>Checkout</c> object.
        /// </summary>
        /// <remarks>
        /// Takes as parameters, the checkout Id and the checkout Url.
        /// </remarks>
        /// <param name="id">Your Checkout Id.</param>
        /// <param name="url">Your Checkout Url.</param>
        public Checkout(string id, string url)
        {
            Id = id;
            Url = url;
        }
    }
}
