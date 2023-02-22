using AirBooker.Data.Contexts;
using AirBooker.Data.Entities;
using AirBooker.Data.Repositories.Contracts;
using AirBooker.Infrastructure.Contracts.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirBooker.Data.Repositories
{
    public class AirlineRepository : IAirlineRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<AirlineRepository> _logger;

        public AirlineRepository(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<AirlineRepository> logger)
        {
            _dbContext = dbContextWrapper.DbContext;
            _logger = logger;
        }

        public async Task<List<Airline>?> GetAirlineById(Guid id)
        {
            IQueryable<Airline> query = _dbContext.Airlines;

            query = query.Where(x => x.Id == id);

            var airline = await query
                .Include(x => x.AirlinesAirports)
                .ThenInclude(x => x.Airport).ToListAsync();

            return airline;
        }
    }
}
