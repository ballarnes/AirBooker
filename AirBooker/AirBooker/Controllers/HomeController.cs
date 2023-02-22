using AirBooker.Domain.Services.Contracts;
using AirBooker.Web.Controllers;
using AirBooker.Web.Models.Home;
using AirBooker.Web.Models.Pagination;
using Microsoft.AspNetCore.Mvc;

namespace AirBooker.Controllers
{
	public class HomeController : BaseController
    {
        private readonly IFlightService _flightService;
        private readonly IAirportService _airportService;

        public HomeController(
			ILogger<HomeController> logger,
            IFlightService flightService,
			IAirportService airportService)
            : base(logger)
        {
            _flightService = flightService;
			_airportService = airportService;
        }

        public async Task<IActionResult> Index(int? pageNumber, string? selectedAirportId)
		{
			pageNumber ??= 0;
			var itemsOnPage = 3;

			var airports = await _airportService.GetSelectListAirports();
			var flights = await _flightService.GetFlightsByPageAndDate(pageNumber.Value, 3, DateTime.Now);

            if (selectedAirportId != null)
			{
				flights = await _flightService.GetFlightsByPageAndAirportId(pageNumber.Value, 3, new Guid(selectedAirportId), DateTime.Now);
			}

            var paginationInfo = new PaginationInfo()
            {
                ActualPage = pageNumber.Value,
                ItemsPerPage = itemsOnPage,
                TotalItems = flights.Count,
                TotalPages = (int)Math.Ceiling((decimal)flights.Count / itemsOnPage),
                SelectedAirportId = selectedAirportId
            };

			var viewModel = new IndexViewModel()
			{
				Flights = flights.Data,
				DepartureAirports = airports,
				PaginationInfo = paginationInfo,
			};

			return View("Index", viewModel);
		}

        public IActionResult Privacy()
        {
            return View("Privacy");
        }
	}
}