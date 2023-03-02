using AirBooker.Domain.Models.Dtos;
using AirBooker.Domain.Models.Responses;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AirBooker.Domain.Services.Contracts
{
    public interface IAirportService
    {
        Task<DataResponse<AirportDto>?> GetAirports();
        Task<DataResponse<AirportDto>?> GetAirportById(Guid id);
        Task<IEnumerable<SelectListItem>> GetSelectListAirports();
    }
}
