namespace Tipi.Tools.Payments.Models
{
    /// <summary>
    /// Class representing the ActiveSubscription object, 
    /// <see href="https://docs.codingtipi.com/docs/toolkit/recurrente/classes#active-subscription">See More</see>
    /// </summary>
    public class ActiveSubscription
    {
        /// <summary>Subscription Id. </summary>
        public string Id { get; set; } = default!;
        /// <summary>Subscription status. </summary>
        public string Status { get; set; } = default!;
        /// <summary>Date when the subscription started. </summary>
        public DateTime Started { get; set; } = default!;
        /// <summary>Date when the subscription will be charged again. </summary>
        public DateTime NextBilling { get; set; } = default!;
        /// <summary>Subscriber Id. </summary>
        public string UserId { get; set; } = default!;
        /// <summary>Subscriber Email. </summary>
        public string Email { get; set; } = default!;
        /// <summary>Product Id. </summary>
        public string ProductId { get; set; } = default!;
    }
}
