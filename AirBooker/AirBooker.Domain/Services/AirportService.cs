using AirBooker.Data.Contexts;
using AirBooker.Data.Repositories;
using AirBooker.Data.Repositories.Contracts;
using AirBooker.Domain.Models.Dtos;
using AirBooker.Domain.Models.Responses;
using AirBooker.Domain.Services.Contracts;
using AirBooker.Infrastructure.Contracts.Services;
using AirBooker.Infrastructure.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace AirBooker.Domain.Services
{
    public class AirportService : BaseDataService<ApplicationDbContext>, IAirportService
    {
        private readonly IAirportRepository _airportRepository;
        private readonly IMapper _mapper;

        public AirportService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            IAirportRepository airportRepository,
            IMapper mapper)
            : base(dbContextWrapper, logger)
        {
            _airportRepository = airportRepository;
            _mapper = mapper;
        }

        public async Task<DataResponse<AirportDto>?> GetAirports()
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _airportRepository.GetAirports();

                if (result == null)
                {
                    return null;
                }

                return new DataResponse<AirportDto>()
                {
                    Data = result.Select(x => _mapper.Map<AirportDto>(x)).ToList(),
                };
            });
        }

        public async Task<IEnumerable<SelectListItem>> GetSelectListAirports()
        {
            return await ExecuteSafeAsync(async () =>
            {
                var airports = await _airportRepository.GetAirports();

                var selectListItems = airports.ConvertAll(x =>
                {
                    return new SelectListItem()
                    {
                        Text = x.Name,
                        Value = x.Id.ToString(),
                        Selected = false
                    };
                });

                return selectListItems;
            });
        }

        public async Task<DataResponse<AirportDto>?> GetAirportById(Guid id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _airportRepository.GetAirportById(id);

                if (result == null)
                {
                    return null;
                }

                return new DataResponse<AirportDto>()
                {
                    Data = result.Select(x => _mapper.Map<AirportDto>(x)).ToList(),
                };
            });
        }
    }
}
