using AirBooker.Domain.Models.Dtos;
using AirBooker.Domain.Models.Responses;

namespace AirBooker.Domain.Services.Contracts
{
    public interface IAirlineService
    {
        Task<DataResponse<AirlineDto>?> GetAirlineById(Guid id);
    }
}
