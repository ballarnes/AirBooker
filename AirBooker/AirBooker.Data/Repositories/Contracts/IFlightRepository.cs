using AirBooker.Data.Entities;

namespace AirBooker.Data.Repositories.Contracts
{
    public interface IFlightRepository
    {
        Task<PaginatedData<Flight>> GetFlightsByPageAndAirportId(int pageIndex, int pageSize, Guid airportId);
        Task<PaginatedData<Flight>> GetFlightsByPageAndAirportId(int pageIndex, int pageSize, Guid airportId, DateTime date);
        Task<PaginatedData<Flight>> GetFlightsByPageAndDate(int pageIndex, int pageSize, DateTime date);
        Task<List<Flight>?> GetFlightById(Guid id);
        Task<List<Flight>?> GetFlightByNumberAndDate(string flightNumber, DateTime date);
        Task<List<Flight>?> GetNearestReturnFlight(Guid departureAirportId, Guid arrivalAirportId, DateTime date);
        Task<List<DateTime>?> GetAvailableFlightDates(string flightNumber);
    }
}
