namespace AirBooker.Data.Entities
{
    public class Airport
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Place { get; set; } = null!;
        public string? CityPhotoFileName { get; set; }
        public string? AirportPhotoFileName { get; set; }
        public ICollection<Flight> DepartureFlights { get; set; } = new List<Flight>();
        public ICollection<Flight> ArrivalFlights { get; set; } = new List<Flight>();
        public ICollection<AirlineAirport> AirlinesAirports { get; set; } = new List<AirlineAirport>();
    }
}
