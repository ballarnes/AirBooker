using AirBooker.Domain.Models.Dtos;

namespace AirBooker.Web.Models.Flight
{
    public class OverviewViewModel
    {
        public FlightDto Flight { get; set; } = null!;
        public FlightDto? ReturnFlight { get; set; }
        public bool WithReturnFlight { get; set; }
    }
}
