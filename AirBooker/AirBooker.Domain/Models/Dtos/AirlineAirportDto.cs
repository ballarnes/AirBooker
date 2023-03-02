namespace AirBooker.Domain.Models.Dtos
{
    public class AirlineAirportDto
    {
        public Guid AirlineId { get; set; }
        public AirlineDto Airline { get; set; } = null!;
        public Guid AirportId { get; set; }
        public AirportDto Airport { get; set; } = null!;
    }
}
