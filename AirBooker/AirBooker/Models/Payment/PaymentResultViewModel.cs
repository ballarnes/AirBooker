using AirBooker.Domain.Models.Dtos;

namespace AirBooker.Web.Models.Payment
{
    public class PaymentResultViewModel
    {
        public Guid FlightBookingId { get; set; }
        public Guid? ReturnFlightBookingId { get; set; }
        public bool OperationSuccess { get; set; }
    }
}
