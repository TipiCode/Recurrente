namespace Tipi.Tools.Payments.Dto
{
    internal class RecurrenteCard
    {
        public string last4 { get; set; } = default!;
        public string expiration_month { get; set; } = default!;
        public int expiration_year { get; set; } = default!;
        public string network { get; set; } = default!;
    }
}
