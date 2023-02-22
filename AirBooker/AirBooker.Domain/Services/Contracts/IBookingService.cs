using AirBooker.Domain.Models.Dtos;
using AirBooker.Domain.Models.Responses;

namespace AirBooker.Domain.Services.Contracts
{
    public interface IBookingService
    {
        Task<PaginatedDataResponse<BookingDto>?> GetBookingsByPageAndFlightId(int pageIndex, int pageSize, Guid flightId);
        Task<DataResponse<BookingDto>?> GetBookingById(Guid id);
        Task<BookingResponse> CreateBooking(string userId, Guid flightId, Guid? returnFlightId);
    }
}
