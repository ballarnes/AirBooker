using AirBooker.Data.Contexts;
using AirBooker.Data.Repositories;
using AirBooker.Data.Repositories.Contracts;
using AirBooker.Domain.Models.Dtos;
using AirBooker.Domain.Models.Responses;
using AirBooker.Domain.Services.Contracts;
using AirBooker.Infrastructure.Contracts.Services;
using AirBooker.Infrastructure.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AirBooker.Domain.Services
{
    public class BookingService : BaseDataService<ApplicationDbContext>, IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public BookingService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            IBookingRepository bookingRepository,
            IMapper mapper)
            : base(dbContextWrapper, logger)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedDataResponse<BookingDto>?> GetBookingsByPageAndFlightId(int pageIndex, int pageSize, Guid flightId)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _bookingRepository.GetBookingsByPageAndFlightId(pageIndex, pageSize, flightId);

                if (result == null)
                {
                    return null;
                }

                return new PaginatedDataResponse<BookingDto>()
                {
                    Count = result.TotalCount,
                    Data = result.Data.Select(x => _mapper.Map<BookingDto>(x)).ToList(),
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };
            });
        }

        public async Task<DataResponse<BookingDto>?> GetBookingById(Guid id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _bookingRepository.GetBookingById(id);

                if (result == null)
                {
                    return null;
                }

                return new DataResponse<BookingDto>()
                {
                    Data = result.Select(x => _mapper.Map<BookingDto>(x)).ToList(),
                };
            });
        }

        public async Task<BookingResponse> CreateBooking(string userId, Guid flightId, Guid? returnFlightId)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var flightBookingId = await _bookingRepository.CreateBooking(userId, flightId);

                var result = new BookingResponse()
                {
                    FlightBookingId = flightBookingId
                };

                if (returnFlightId != null)
                {
                    var returnFlightBookingId = await _bookingRepository.CreateBooking(userId, returnFlightId.Value);
                    result.ReturnFlightBookindId = returnFlightBookingId;
                }

                return result;
            });
        }
    }
}
