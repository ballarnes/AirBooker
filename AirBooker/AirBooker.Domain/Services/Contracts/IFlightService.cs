using AirBooker.Domain.Models.Dtos;
using AirBooker.Domain.Models.Responses;

namespace AirBooker.Domain.Services.Contracts
{
    public interface IFlightService
    {
        Task<PaginatedDataResponse<FlightDto>?> GetFlightsByPageAndAirportId(int pageIndex, int pageSize, Guid airportId);
        Task<PaginatedDataResponse<FlightDto>?> GetFlightsByPageAndAirportId(int pageIndex, int pageSize, Guid airportId, DateTime date);
        Task<PaginatedDataResponse<FlightDto>?> GetFlightsByPageAndDate(int pageIndex, int pageSize, DateTime date);
        Task<DataResponse<FlightDto>?> GetFlightById(Guid id);
        Task<DataResponse<FlightDto>?> GetFlightByNumberAndDate(string flightNumber, DateTime date);
        Task<DataResponse<FlightDto>?> GetNearestReturnFlight(Guid departureAirportId, Guid arrivalAirportId, DateTime date);
        Task<DataResponse<DateTime>?> GetAvailableFlightDates(string flightNumber);
    }
}
