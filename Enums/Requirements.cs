namespace Tipi.Tools.Payments.Enums
{
    /// <summary>
    /// Enum used to define the required options for products, 
    /// <see href="https://docs.codingtipi.com/docs/toolkit/recurrente/enums#requirements">See More</see>
    /// </summary>
    public enum Requirements
    {
        /// <summary>Used if the field displayed and is required.</summary>
        Required,
        /// <summary>Used if the field is displayed and is optional.</summary>
        Optional,
        /// <summary>Used if the field is not needed.</summary>
        None
    }
}
