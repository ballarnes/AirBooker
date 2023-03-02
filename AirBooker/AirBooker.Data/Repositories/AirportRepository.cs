using AirBooker.Data.Contexts;
using AirBooker.Data.Entities;
using AirBooker.Data.Repositories.Contracts;
using AirBooker.Infrastructure.Contracts.Services;
using Microsoft.Extensions.Logging;

namespace AirBooker.Data.Repositories
{
    public class AirportRepository : IAirportRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<AirportRepository> _logger;

        public AirportRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<AirportRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        public async Task<List<Airport>> GetAirports()
        {
            IQueryable<Airport> query = _dbContext.Airports;

            var airports = await query
                .Include(x => x.AirlinesAirports)
                .ThenInclude(x => x.Airline).ToListAsync();

            return airports;
        }

        public async Task<List<Airport>?> GetAirportById(Guid id)
        {
            IQueryable<Airport> query = _dbContext.Airports;

            query = query.Where(x => x.Id == id);

            var airport = await query
                .Include(x => x.AirlinesAirports)
                .ThenInclude(x => x.Airline).ToListAsync();

            return airport;
        }
    }
}
