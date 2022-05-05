namespace Tipi.Tools.Payments.Dto
{
    internal class RecurrenteSubscription
    {
        public string name { get; set; } = default!;
        public string description { get; set; } = default!;
        public string phone_requirement { get; set; } = default!;
        public string address_requirement { get; set; } = default!;
        public string billing_info_requirement { get; set; } = default!;
        public string image_url { get; set; } = default!;
        public string cancel_url { get; set; } = default!;
        public string success_url { get; set; } = default!;
    }
}
