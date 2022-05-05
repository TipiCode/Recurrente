namespace Tipi.Tools.Payments.Config
{
    public class RecurrenteOptions
    {
        public string SecretKey { get; }
        public string PublicKey { get; }
        public RecurrenteOptions(string pKey, string sKey)
        {
            SecretKey = sKey;
            PublicKey = pKey;
        }
    }
}
