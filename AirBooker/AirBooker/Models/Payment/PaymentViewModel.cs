using AirBooker.Domain.Models.Dtos;

namespace AirBooker.Web.Models.Payment
{
    public class PaymentViewModel
    {
        public FlightDto Flight { get; set; } = null!;
        public FlightDto? ReturnFlight { get; set; }
        public Guid FlightId { get; set; }
        public Guid? ReturnFlightId { get; set; }
        public string? CardholderName { get; set; }
        public string? CardNumber { get; set; }
        public string? ExpirationDate { get; set; }
        public int SecurityCode { get; set; }
    }
}
