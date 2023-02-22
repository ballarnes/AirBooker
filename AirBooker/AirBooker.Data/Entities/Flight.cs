namespace AirBooker.Data.Entities
{
    public class Flight
    {
        public Guid Id { get; set; }
        public string FlightNumber { get; set; } = null!;
        public Guid AirlineId { get; set; }
        public Airline Airline { get; set; } = null!;
        public int FreeSeatsCount { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public TimeSpan TravelTime { get; set; }
        public Guid DepartureAirportId { get; set; }
        public Airport DepartureAirport { get; set; } = null!;
        public Guid ArrivalAirportId { get; set; }
        public Airport ArrivalAirport { get; set; } = null!;
        public ICollection<Booking>? Bookings { get; set; } = new List<Booking>();
    }
}
