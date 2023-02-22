using AirBooker.Domain.Models.Dtos;

namespace AirBooker.Web.Models.Payment
{
    public class PaymentViewModel
    {
        public Guid FlightId { get; set; }
        public Guid? ReturnFlightId { get; set; }
        public string? CardholderName { get; set; }
        public string? CardNumber { get; set; }
        public string? ExpirationDate { get; set; }
        public int SecurityCode { get; set; }
    }
}
