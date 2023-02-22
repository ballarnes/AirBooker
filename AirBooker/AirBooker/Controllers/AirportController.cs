using AirBooker.Domain.Services.Contracts;
using AirBooker.Web.Models.Airline;
using AirBooker.Web.Models.Airport;
using Microsoft.AspNetCore.Mvc;

namespace AirBooker.Web.Controllers
{
    public class AirportController : BaseController
    {
        private readonly IAirportService _airportService;

        public AirportController(
            ILogger<AirportController> logger,
            IAirportService airportService)
            : base(logger)
        {
            _airportService = airportService;
        }

        public async Task<IActionResult> Index(string? id)
        {
            Guid guidId;

            if (!Guid.TryParse(id, out guidId))
            {
                return RedirectToAction("Error", "Home");
            }

            var airport = await _airportService.GetAirportById(guidId);

            if (airport.Data.Count() == 0)
            {
                return RedirectToAction("Error", "Home");
            }

            var viewModel = new Models.Airport.IndexViewModel()
            {
                Airport = airport.Data.FirstOrDefault()!
            };

            return View(viewModel);
        }
    }
}
