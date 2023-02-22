namespace AirBooker.Domain.Models.Dtos
{
    public class AirportDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Place { get; set; } = null!;
        public string? CityPhotoUrl { get; set; } = null!;
        public string? PhotoUrl { get; set; } = null!;
        public ICollection<AirlineAirportDto> AirlinesAirports { get; set; } = null!;
    }
}
