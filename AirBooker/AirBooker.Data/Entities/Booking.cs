namespace AirBooker.Data.Entities
{
    public class Booking
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = null!;
        public ApplicationUser User { get; set; } = null!;
        public Guid FlightId { get; set; }
        public Flight Flight { get; set; } = null!;
        public DateTime BookingDateTime { get; set; }
        public bool isCanceled { get; set; }
    }
}
