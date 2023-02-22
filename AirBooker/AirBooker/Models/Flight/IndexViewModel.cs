using AirBooker.Domain.Models.Dtos;

namespace AirBooker.Web.Models.Flight
{
    public class IndexViewModel
    {
        public FlightDto Flight { get; set; } = null!;
        public FlightDto? ReturnFlight { get; set; }
        public List<DateTime> AvailableFlightDates { get; set; } = null!;
        public List<DateTime>? AvailableReturnFlightDates { get; set; }
        public bool WithReturnFlight { get; set; }
        public Guid FlightId { get; set; }
        public string FlightNumber { get; set; } = null!;
        public string? ReturnFlightNumber { get; set; }
        public DateTime SelectedFlightDate { get; set; }
        public DateTime SelectedReturnFlightDate { get; set; }
    }
}
