namespace AirBooker.Domain.Models.Responses
{
    public class BookingResponse
    {
        public Guid FlightBookingId { get; set; }
        public Guid? ReturnFlightBookindId { get; set; }
    }
}
