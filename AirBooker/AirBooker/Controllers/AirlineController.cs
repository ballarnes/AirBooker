using AirBooker.Domain.Services.Contracts;
using AirBooker.Web.Controllers;
using AirBooker.Web.Models;
using AirBooker.Web.Models.Airline;
using Microsoft.AspNetCore.Mvc;

namespace AirBooker.Controllers
{
    public class AirlineController : BaseController
    {
        private readonly IAirlineService _airlineService;

        public AirlineController(
            ILogger<AirlineController> logger,
            IAirlineService airlineService)
            : base(logger)
        {
            _airlineService = airlineService;
        }

        public async Task<IActionResult> Index(string? id)
        {
            Guid guidId;

            if (!Guid.TryParse(id, out guidId))
            {
                return RedirectToAction("Error", "Home");
            }

            var airlines = await _airlineService.GetAirlineById(guidId);

            if (airlines.Data.Count() == 0)
            {
                return RedirectToAction("Error", "Home");
            }

            var airline = airlines.Data.FirstOrDefault()!;

            var viewModel = new IndexViewModel()
            {
                Airline = airline,
            };

            return View(viewModel);
        }
    }
}