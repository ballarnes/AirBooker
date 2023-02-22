using AirBooker.Data.Entities;

namespace AirBooker.Domain.Models.Dtos
{
    public class BookingDto
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
        public Guid FlightId { get; set; }
        public FlightDto Flight { get; set; } = null!;
        public DateTime BookingDateTime { get; set; }
        public bool isCanceled { get; set; }
    }
}
