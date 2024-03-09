namespace apps.Models.Request
{
    public class EditMasterRequest
    {
        public string? Code { get; set; }
        public string? NewCode { get; set; }
        public int Qty { get; set; } = 0;
        public decimal MyProperty { get; set; } = 0;
    }
}
