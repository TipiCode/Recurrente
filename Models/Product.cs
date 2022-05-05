using Tipi.Tools.Payments.Abstract;
using Tipi.Tools.Payments.Enums;

namespace Tipi.Tools.Payments.Models
{
    /// <summary>
    /// Class representing a Product object, 
    /// <see href="https://docs.codingtipi.com/docs/toolkit/recurrente/classes#product">See More</see>
    /// </summary>
    public class Product : BaseItem
    {
        /// <summary>Object containing the Price information</summary>
        public SinglePrice Price { get; }
        /// <summary>
        /// Constructor to initialize the <c>Product</c> object.
        /// </summary>
        public Product(SinglePrice price)
        {
            Price = price;
        }
    }
}
