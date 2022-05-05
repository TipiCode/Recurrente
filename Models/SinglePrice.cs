using Tipi.Tools.Payments.Enums;

namespace Tipi.Tools.Payments.Models
{
    /// <summary>
    /// Class representing a Single Price Object, <see href="https://docs.codingtipi.com/docs/toolkit/recurrente/classes#single-price">See More</see> 
    /// </summary>
    public class SinglePrice
    {
        /// <summary>Price Id. </summary>
        public string Id { get; set; } = default!;
        /// <summary>Price Amount in Decimal factor. </summary>
        public double Amount { get; set; }
        /// <summary>Currency type </summary>
        public Currency Currency { get; set; }
    }
}
