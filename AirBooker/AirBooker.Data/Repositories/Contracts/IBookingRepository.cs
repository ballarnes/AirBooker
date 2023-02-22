using AirBooker.Data.Entities;

namespace AirBooker.Data.Repositories.Contracts
{
    public interface IBookingRepository
    {
        Task<PaginatedData<Booking>> GetBookingsByPageAndFlightId(int pageIndex, int pageSize, Guid flightId);
        Task<List<Booking>?> GetBookingById(Guid id);
        Task<Guid> CreateBooking(string userId, Guid flightId);
    }
}
