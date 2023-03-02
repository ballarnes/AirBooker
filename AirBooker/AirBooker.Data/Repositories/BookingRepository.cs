using AirBooker.Data.Contexts;
using AirBooker.Data.Entities;
using AirBooker.Data.Repositories.Contracts;
using AirBooker.Infrastructure.Contracts.Services;
using Microsoft.Extensions.Logging;

namespace AirBooker.Data.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<BookingRepository> _logger;

        public BookingRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BookingRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        public async Task<PaginatedData<Booking>> GetBookingsByPageAndFlightId(int pageIndex, int pageSize, Guid flightId)
        {
            IQueryable<Booking> query = _dbContext.Bookings;

            query = query.Where(x => x.FlightId == flightId);

            var totalItems = await query.CountAsync();

            var itemsOnPage = await query.OrderBy(x => x.BookingDateTime)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedData<Booking>()
            { 
                TotalCount = totalItems, 
                Data = itemsOnPage 
            };
        }

        public async Task<List<Booking>?> GetBookingById(Guid id)
        {
            IQueryable<Booking> query = _dbContext.Bookings;

            query = query.Where(x => x.Id == id);

            var booking = await query
                .Include(x => x.User)
                .Include(x => x.Flight)
                .ThenInclude(x => x.Airline)
                .Include(x => x.Flight)
                .ThenInclude(x => x.DepartureAirport)
                .Include(x => x.Flight)
                .ThenInclude(x => x.ArrivalAirport)
                .ToListAsync();

            return booking;
        }

        public async Task<Guid> CreateBooking(string userId, Guid flightId)
        {
            var booking = await _dbContext.Bookings.AddAsync(new Booking
            {
                UserId = userId,
                FlightId = flightId,
                BookingDateTime = DateTime.Now,
                isCanceled = false
            });

            var flight = await _dbContext.Flights.FirstOrDefaultAsync(x => x.Id == flightId);

            flight.FreeSeatsCount -= 1;

            await _dbContext.SaveChangesAsync();

            return booking.Entity.Id;
        }
    }
}
