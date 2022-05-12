namespace Tipi.Tools.Payments.Dto
{
    internal class RecurrentePaymentMethod
    {
        public string id { get; set; } = default!;
        public string type { get; set; } = default!;
        public RecurrenteCard card { get; set; } = default!;
    }
}
