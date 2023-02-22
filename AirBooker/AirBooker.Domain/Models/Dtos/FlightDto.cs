using AirBooker.Data.Entities;

namespace AirBooker.Domain.Models.Dtos
{
    public class FlightDto
    {
        public Guid Id { get; set; }
        public string FlightNumber { get; set; } = null!;
        public Guid AirlineId { get; set; }
        public AirlineDto Airline { get; set; } = null!;
        public int FreeSeatsCount { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public TimeSpan TravelTime { get; set; }
        public Guid DepartureAirportId { get; set; }
        public AirportDto DepartureAirport { get; set; } = null!;
        public Guid ArrivalAirportId { get; set; }
        public AirportDto ArrivalAirport { get; set; } = null!;
        public List<BookingDto>? Bookings { get; set; }
    }
}
