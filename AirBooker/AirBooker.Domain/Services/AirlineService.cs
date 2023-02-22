using AirBooker.Data.Contexts;
using AirBooker.Data.Repositories.Contracts;
using AirBooker.Domain.Models.Dtos;
using AirBooker.Domain.Models.Responses;
using AirBooker.Domain.Services.Contracts;
using AirBooker.Infrastructure.Contracts.Services;
using AirBooker.Infrastructure.Services;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AirBooker.Domain.Services
{
    public class AirlineService : BaseDataService<ApplicationDbContext>, IAirlineService
    {
        private readonly IAirlineRepository _airlineRepository;
        private readonly IMapper _mapper;

        public AirlineService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            IAirlineRepository airlineRepository,
            IMapper mapper)
            : base(dbContextWrapper, logger)
        {
            _airlineRepository = airlineRepository;
            _mapper = mapper;
        }

        public async Task<DataResponse<AirlineDto>?> GetAirlineById(Guid id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _airlineRepository.GetAirlineById(id);

                if (result == null)
                {
                    return null;
                }

                return new DataResponse<AirlineDto>()
                {
                    Data = result.Select(x => _mapper.Map<AirlineDto>(x)).ToList(),
                };
            });
        }
    }
}
