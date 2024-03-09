namespace apps.Models.Request
{
    public class AddMasterRequest
    {
        public string? Code { get; set; }
        public int? Qty { get; set; } = 0;
        public decimal Balance { get; set; } = 0;
    }
}
