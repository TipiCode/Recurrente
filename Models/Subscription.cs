using Tipi.Tools.Payments.Abstract;
using Tipi.Tools.Payments.Enums;

namespace Tipi.Tools.Payments.Models
{
    /// <summary>
    /// Class representing a Subscription object, 
    /// <see href="https://docs.codingtipi.com/docs/toolkit/recurrente/classes#subscription">See More</see>
    /// </summary>
    public class Subscription : BaseItem
    {
        /// <summary>Object containing the Price information</summary>
        public RecurringPrice Price { get; }
        /// <summary>
        /// Constructor to initialize the <c>Subscription</c> object.
        /// </summary>
        public Subscription(RecurringPrice price)
        {
            Price = price;
        }
    }
}
