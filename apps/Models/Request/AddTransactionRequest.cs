namespace apps.Models.Request
{
    public class AddTransactionRequest
    {
        public string? Code { get; set; }
        public int Qty { get; set; }
        public decimal Balance { get; set; }
    }
}
