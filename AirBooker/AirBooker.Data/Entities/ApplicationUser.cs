using Microsoft.AspNetCore.Identity;

namespace AirBooker.Data.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? City { get; set; }
        public ICollection<Booking>? Bookings { get; set; } = new List<Booking>();
    }
}
