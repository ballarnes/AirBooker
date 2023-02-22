using AirBooker.Data.Contexts;
using AirBooker.Data.Entities;
using AirBooker.Data.Repositories.Contracts;
using AirBooker.Infrastructure.Contracts.Services;
using Microsoft.Extensions.Logging;
using System;

namespace AirBooker.Data.Repositories
{
    public class FlightRepository : IFlightRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<FlightRepository> _logger;

        public FlightRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<FlightRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        public async Task<PaginatedData<Flight>> GetFlightsByPageAndAirportId(int pageIndex, int pageSize, Guid airportId)
        {
            IQueryable<Flight> query = _dbContext.Flights;

            query = query.Where(x => x.DepartureAirportId == airportId);

            var totalItems = await query.CountAsync();

            var itemsOnPage = await query.OrderBy(x => x.DepartureDateTime)
                .Include(x => x.Airline)
                .Include(x => x.DepartureAirport)
                .Include(x => x.ArrivalAirport)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedData<Flight>()
            {
                TotalCount = totalItems,
                Data = itemsOnPage
            };
        }

        public async Task<PaginatedData<Flight>> GetFlightsByPageAndAirportId(int pageIndex, int pageSize, Guid airportId, DateTime date)
        {
            IQueryable<Flight> query = _dbContext.Flights;

            query = query.Where(x => x.DepartureAirportId == airportId && 
                                     x.DepartureDateTime >= date && 
                                     x.DepartureDateTime.Date == date.Date);

            var totalItems = await query.CountAsync();

            var itemsOnPage = await query.OrderBy(x => x.DepartureDateTime)
                .Include(x => x.Airline)
                .Include(x => x.DepartureAirport)
                .Include(x => x.ArrivalAirport)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedData<Flight>()
            {
                TotalCount = totalItems,
                Data = itemsOnPage
            };
        }

        public async Task<PaginatedData<Flight>> GetFlightsByPageAndDate(int pageIndex, int pageSize, DateTime date)
        {
            IQueryable<Flight> query = _dbContext.Flights;

            query = query.Where(x => x.DepartureDateTime >= date && 
                                     x.DepartureDateTime.Date == date.Date);

            var totalItems = await query.CountAsync();

            var itemsOnPage = await query.OrderBy(x => x.DepartureDateTime)
                .Include(x => x.Airline)
                .Include(x => x.DepartureAirport)
                .Include(x => x.ArrivalAirport)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedData<Flight>()
            {
                TotalCount = totalItems,
                Data = itemsOnPage
            };
        }

        public async Task<List<Flight>?> GetFlightById(Guid id)
        {
            IQueryable<Flight> query = _dbContext.Flights;

            query = query.Where(x => x.Id == id);

            var flight = await query
                .Include(x => x.Airline)
                .Include(x => x.DepartureAirport)
                .Include(x => x.ArrivalAirport)
                .ToListAsync();

            return flight;
        }

        public async Task<List<Flight>?> GetFlightByNumberAndDate(string flightNumber, DateTime date)
        {
            IQueryable<Flight> query = _dbContext.Flights;

            query = query.Where(x => x.FlightNumber == flightNumber &&
                                     x.DepartureDateTime.Date == date.Date);

            var flight = await query
                .Include(x => x.Airline)
                .Include(x => x.DepartureAirport)
                .Include(x => x.ArrivalAirport)
                .ToListAsync();

            return flight;
        }

        public async Task<List<Flight>?> GetNearestReturnFlight(Guid departureAirportId, Guid arrivalAirportId, DateTime date)
        {
            IQueryable<Flight> query = _dbContext.Flights;

            query = query.Where(x => x.DepartureAirportId == departureAirportId &&
                                     x.ArrivalAirportId == arrivalAirportId &&
                                     x.DepartureDateTime >= date);

            var flight = await query
                .Include(x => x.Airline)
                .Include(x => x.DepartureAirport)
                .Include(x => x.ArrivalAirport)
                .FirstOrDefaultAsync();

            return new List<Flight> { flight };
        }

        public async Task<List<DateTime>?> GetAvailableFlightDates(string flightNumber)
        {
            IQueryable<Flight> query = _dbContext.Flights;

            query = query.Where(x => x.FlightNumber == flightNumber &&
                                     x.DepartureDateTime >= DateTime.Now);

            var dates = await query.Select(x => x.DepartureDateTime).ToListAsync();

            return dates;
        }
    }
}
