using Tipi.Tools.Payments.Enums;

namespace Tipi.Tools.Payments.Abstract
{
    /// <summary>
    /// Class representing a base Item type, 
    /// </summary>
    public abstract class BaseItem
    {
        /// <summary> Item Id. </summary>
        public string Id { get; set; } = default!;
        /// <summary> Item name. </summary>
        public string Name { get; set; } = default!;
        /// <summary> Item description (Optional). </summary>
        public string Description { get; set; } = default!;
        /// <summary> Item Phone requirement at checkout. </summary>
        public Requirements PhoneRequirement { get; set; }
        /// <summary> Item Address requirement at checkout. </summary>
        public Requirements AddressRequirement { get; set; }
        /// <summary> Item Billing Info requirement at checkout. </summary>
        public Requirements BillingInfoRequirement { get; set; }
        /// <summary> Item Image. </summary>
        public string Image { get; set; } = default!;
        /// <summary> Cancel Redirection URL. </summary>
        public string CancelUrl { get; set; } = default!;
        /// <summary> Success Redirection URL. </summary>
        public string SuccessUrl { get; set; } = default!;
        /// <summary> StoreFront URL. </summary>
        public string StoreUrl { get; set; } = default!;
        /// <summary> Item status. </summary>
        public string Status { get; set; } = default!;
    }
}
