namespace Tipi.Tools.Payments.Config
{
    /// <summary>
    /// Class used to configure the <c>Recurrente</c> class, 
    /// <see href="https://docs.codingtipi.com/docs/toolkit/recurrente/classes#recurrente-options">See More</see>
    /// </summary>
    public class RecurrenteOptions
    {
        /// <summary>
        /// Your Secret Key provided by Recurrente.
        /// </summary>
        public string SecretKey { get; }
        /// <summary>
        /// Your Secret Key provided by Recurrente.
        /// </summary>
        public string PublicKey { get; }
        /// <summary>
        /// Constructor to initialize the <c>RecurrenteOptions</c> object.
        /// </summary>
        /// <remarks>
        /// Takes as parameters, the Secret Key and the Public Key provided by Recurrente.
        /// </remarks>
        /// <param name="pKey">Your Public Key provided by Recurrente.</param>
        /// <param name="sKey">Your Secret Key provided by Recurrente.</param>
        public RecurrenteOptions(string pKey, string sKey)
        {
            SecretKey = sKey;
            PublicKey = pKey;
        }
    }
}
