namespace AirBooker.Web.Models
{
    public class PhotoCardViewModel
    {
        public string PhotoUrl { get; set; } = null!;
        public string OverlayText { get; set; } = null!;
        public bool IsLink { get; set; }
        public string? Controller { get; set; }
        public string? Action { get; set; }
        public string? Id { get; set; }
    }
}
