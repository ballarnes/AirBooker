namespace AirBooker.Data.Entities
{
    public class Airline
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Country { get; set; } = null!;
        public byte? Rating { get; set; }
        public DateTime? Founded { get; set; }
        public string? LogoFileName { get; set; }
        public ICollection<Flight> Flights { get; set; } = new List<Flight>();
        public ICollection<AirlineAirport> AirlinesAirports { get; set; } = new List<AirlineAirport>();
    }
}
