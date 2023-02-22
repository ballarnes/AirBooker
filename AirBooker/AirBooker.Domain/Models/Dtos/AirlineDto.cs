namespace AirBooker.Domain.Models.Dtos
{
    public class AirlineDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Country { get; set; } = null!;
        public byte? Rating { get; set; }
        public DateTime? Founded { get; set; }
        public string? LogoUrl { get; set; }
        public ICollection<AirlineAirportDto> AirlinesAirports { get; set; } = null!;
    }
}
