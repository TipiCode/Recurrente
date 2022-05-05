namespace Tipi.Tools.Payments.Dto
{
    internal class RecurrenteSubscriptionPrice
    {
        public string amount_as_decimal { get; set; } = default!;
        public string currency { get; set; } = default!;
        public string charge_type { get; set; } = default!;
        public string billing_interval_count { get; set; } = default!;
        public string billing_interval { get; set; } = default!;
        public string free_trial_interval_count { get; set; } = default!;
        public string free_trial_interval { get; set; } = default!;
        public string periods_before_automatic_cancellation { get; set; } = default!;
    }
}
