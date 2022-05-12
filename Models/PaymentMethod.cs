namespace Tipi.Tools.Payments.Models
{
    /// <summary>
    /// Class representing the Checkout object, 
    /// <see href="https://docs.codingtipi.com/docs/toolkit/recurrente/classes#payment-method">See More</see>
    /// </summary>
    public class PaymentMethod
    {
        /// <summary>
        /// Your Payment Method Id.
        /// </summary>
        public string Id { get; } = default!;
        /// <summary>
        /// Your Payment Method Type.
        /// </summary>
        public string Type { get; } = default!;
        /// <summary>
        /// The card your user used to pay.
        /// </summary>
        public Card Card { get; } = default!;
        /// <summary>
        /// Constructor to initialize the <c>PaymentMethod</c> object whith a type card.
        /// </summary>
        /// <param name="id">Your Payment Method Id..</param>
        /// <param name="last4">The 4 last digits of your card.</param>
        /// <param name="expMonth">The expiration month of your card.</param>
        /// <param name="expYear">The expiration year of your card.</param>
        /// <param name="network">Your card Provider.</param>
        public PaymentMethod(string id, string last4, string expMonth, int expYear, string network)
        {
            Id = id;
            Type = "card";
            Card = new Card(last4, expMonth, expYear, network);
        }
    }
}
