using AirBooker.Data.Contexts;
using AirBooker.Data.Repositories;
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
    public class FlightService : BaseDataService<ApplicationDbContext>, IFlightService
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IMapper _mapper;

        public FlightService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            IFlightRepository flightRepository,
            IMapper mapper)
            : base(dbContextWrapper, logger)
        {
            _flightRepository = flightRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedDataResponse<FlightDto>?> GetFlightsByPageAndAirportId(int pageIndex, int pageSize, Guid airportId)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _flightRepository.GetFlightsByPageAndAirportId(pageIndex, pageSize, airportId);

                if (result == null)
                {
                    return null;
                }

                return new PaginatedDataResponse<FlightDto>()
                {
                    Count = result.TotalCount,
                    Data = result.Data.Select(x => _mapper.Map<FlightDto>(x)).ToList(),
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };
            });
        }

        public async Task<PaginatedDataResponse<FlightDto>?> GetFlightsByPageAndAirportId(int pageIndex, int pageSize, Guid airportId, DateTime date)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _flightRepository.GetFlightsByPageAndAirportId(pageIndex, pageSize, airportId, date);

                if (result == null)
                {
                    return null;
                }

                return new PaginatedDataResponse<FlightDto>()
                {
                    Count = result.TotalCount,
                    Data = result.Data.Select(x => _mapper.Map<FlightDto>(x)).ToList(),
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };
            });
        }

        public async Task<PaginatedDataResponse<FlightDto>?> GetFlightsByPageAndDate(int pageIndex, int pageSize, DateTime date)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _flightRepository.GetFlightsByPageAndDate(pageIndex, pageSize, date);

                if (result == null)
                {
                    return null;
                }

                return new PaginatedDataResponse<FlightDto>()
                {
                    Count = result.TotalCount,
                    Data = result.Data.Select(x => _mapper.Map<FlightDto>(x)).ToList(),
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };
            });
        }

        public async Task<DataResponse<FlightDto>?> GetFlightById(Guid id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _flightRepository.GetFlightById(id);

                if (result == null)
                {
                    return null;
                }

                return new DataResponse<FlightDto>()
                {
                    Data = result.Select(x => _mapper.Map<FlightDto>(x)).ToList(),
                };
            });
        }

        public async Task<DataResponse<FlightDto>?> GetFlightByNumberAndDate(string flightNumber, DateTime date)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _flightRepository.GetFlightByNumberAndDate(flightNumber, date);

                if (result == null)
                {
                    return null;
                }

                return new DataResponse<FlightDto>()
                {
                    Data = result.Select(x => _mapper.Map<FlightDto>(x)).ToList(),
                };
            });
        }

        public async Task<DataResponse<FlightDto>?> GetNearestReturnFlight(Guid departureAirportId, Guid arrivalAirportId, DateTime date)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _flightRepository.GetNearestReturnFlight(departureAirportId, arrivalAirportId, date);

                if (result == null)
                {
                    return null;
                }

                return new DataResponse<FlightDto>()
                {
                    Data = result.Select(x => _mapper.Map<FlightDto>(x)).ToList(),
                };
            });
        }

        public async Task<DataResponse<DateTime>?> GetAvailableFlightDates(string flightNumber)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _flightRepository.GetAvailableFlightDates(flightNumber);

                if (result == null)
                {
                    return null;
                }

                return new DataResponse<DateTime>()
                {
                    Data = result,
                };
            });
        }
    }
}
