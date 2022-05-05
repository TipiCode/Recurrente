namespace Tipi.Tools.Payments.Interfaces
{
    public interface IRecurrente
    {
        Task<string> CreateCustomerAsync(string name, string email);
        Task<string> CreateCheckoutAsync(string client_id, string price_id);
    }
}
