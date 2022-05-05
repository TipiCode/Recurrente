namespace Tipi.Tools.Payments.Dto
{
    internal class RecurrenteProductPrice
    {
        public string amount_as_decimal { get; set; } = default!;
        public string currency { get; set; } = default!;
        public string charge_type { get; set; } = default!;
    }
}
