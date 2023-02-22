using AirBooker.Data.Entities;

namespace AirBooker.Data.Repositories.Contracts
{
    public interface IAirportRepository
    {
        Task<List<Airport>> GetAirports();
        Task<List<Airport>?> GetAirportById(Guid id);
    }
}
