using Tipi.Tools.Payments.Enums;

namespace Tipi.Tools.Payments.Models
{
    /// <summary>
    /// Class representing a Single Price Object, <see href="https://docs.codingtipi.com/docs/toolkit/recurrente/classes#single-price">See More</see> 
    /// </summary>
    public class RecurringPrice
    {
        /// <summary>Price Id. </summary>
        public string Id { get; set; } = default!;
        /// <summary>Price Amount in Decimal factor. </summary>
        public float Amount { get; set; }
        /// <summary>Currency type.</summary>
        public Currency Currency { get; set; }
        /// <summary>Billing interval type.</summary>
        public BillingInterval Interval { get; set; }
        /// <summary>Number of interval to count before billing.</summary>
        public int IntervalCount { get; set; }
        /// <summary>Free tial Billing interval type.</summary>
        public BillingInterval FreeTrialInterval { get; private set; }
        /// <summary>Number of interval to count before finishing the free trial.</summary>
        public int FreeTrialIntervalCount { get; private set; }
        /// <summary>Automatic Cancellation periods.</summary>
        public int CancellationInterval { get; set; }
        /// <summary>
        /// Add a free trial period to the new <c>Subscription</c>, 
        /// <see href="https://docs.codingtipi.com/docs/toolkit/recurrente/classes#single-price">See More</see>.
        /// </summary>
        /// <remarks>
        /// This will add a free trial period to your product by the specified interval and count.
        /// </remarks>
        /// <param name="interval">Enum of type <c>BillingInterval</c> representing the interval that your free trial is going to count.</param>
        /// <param name="count">Number of intervals to count.</param>
        public void SetFreeTrial(BillingInterval interval, int count)
        {
            FreeTrialInterval = interval;
            FreeTrialIntervalCount = count;
        }

    }
}
