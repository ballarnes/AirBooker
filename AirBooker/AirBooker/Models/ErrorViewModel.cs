namespace AirBooker.Web.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public string? ErrorMessage { get; set; }
        public bool RequiresAdditionalInfo { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}