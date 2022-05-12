using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tipi.Tools.Payments.Models
{
    /// <summary>
    /// Class representing the Card object, 
    /// <see href="https://docs.codingtipi.com/docs/toolkit/recurrente/classes#card">See More</see>
    /// </summary>
    public class Card
    {
        /// <summary>
        /// The 4 last digits of your card.
        /// </summary>
        public string Last4Digits { get; } = default!;
        /// <summary>
        /// The expiration month of your card
        /// </summary>
        public string ExpirationMonth { get; } = default!;
        /// <summary>
        /// The expiration year of your card
        /// </summary>
        public string ExpiratioYear { get; } = default!;
        /// <summary>
        /// Your card Provider
        /// </summary>
        public string Provider { get; } = default!;
        /// <summary>
        /// Constructor to initialize the <c>Card</c> object.
        /// </summary>
        /// <param name="last4">The 4 last digits of your card.</param>
        /// <param name="expritationMonth">The expiration month of your card.</param>
        /// <param name="expirationYear">The expiration year of your card.</param>
        /// <param name="network">Your card Provider.</param>
        public Card(string last4, string expritationMonth, int expirationYear, string network)
        {
            Last4Digits = last4;
            ExpirationMonth = expritationMonth;
            ExpiratioYear = expirationYear.ToString();
            Provider = network;
        }
    }
}
