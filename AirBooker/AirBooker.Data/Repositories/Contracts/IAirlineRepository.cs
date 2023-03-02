using AirBooker.Data.Entities;

namespace AirBooker.Data.Repositories.Contracts
{
    public interface IAirlineRepository
    {
        Task<List<Airline>?> GetAirlineById(Guid id);
    }
}
