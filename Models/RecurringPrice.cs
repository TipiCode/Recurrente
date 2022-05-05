using Tipi.Tools.Payments.Enums;

namespace Tipi.Tools.Payments.Models
{
    /// <summary>
    /// Class representing a Single Price Object, <see href="https://docs.codingtipi.com/docs/toolkit/recurrente/classes#single-price">See More</see> 
    /// </summary>
    public class RecurringPrice
    {
        /// <summary>Price Amount in Decimal factor. </summary>
        public float Amount { get; set; }
        /// <summary>Currency type.</summary>
        public Currency Currency { get; set; }
        /// <summary>Billing interval type.</summary>
        public BillingInterval Interval { get; set; }
        /// <summary>Number of interval to count before billing.</summary>
        public int IntervalCount { get; set; }
        /// <summary>Enables a Free Trial.</summary>
        public bool FreeTrial { get; }
        /// <summary>Free tial Billing interval type.</summary>
        public BillingInterval? FreeTrialInterval { get; }
        /// <summary>Number of interval to count before finishing the free trial.</summary>
        public int? FreeTrialIntervalCount { get; }
        /// <summary>Automatic Cancellation periods.</summary>
        public int? CancellationInterval { get; set; }

    }
}
