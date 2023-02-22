using AirBooker.Domain.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace AirBooker.Web.Controllers
{
    public class FlightController : BaseController
    {
        private readonly IFlightService _flightService;

        public FlightController(
            ILogger<FlightController> logger,
            IFlightService flightService)
            : base(logger)
        {
            _flightService = flightService;
        }

        public async Task<IActionResult> Index(string? id, bool? withReturnFlight)
        {
            Guid guidId;
            withReturnFlight ??= false;

            if (!Guid.TryParse(id, out guidId))
            {
                return RedirectToError("Error while opening flight page.", true);
            }

            var flightResponse = await _flightService.GetFlightById(guidId);

            if (flightResponse.Data.Count() == 0)
            {
                return RedirectToError("Error while opening flight page.", true);
            }

            var flight = flightResponse.Data.FirstOrDefault();

            var availableFlightDates = await _flightService.GetAvailableFlightDates(flight.FlightNumber);

            var viewModel = new Models.Flight.IndexViewModel()
            {
                Flight = flight,
                FlightNumber = flight.FlightNumber,
                FlightId = flight.Id,
                WithReturnFlight = withReturnFlight.Value,
                AvailableFlightDates = availableFlightDates.Data.ToList(),
            };

            if (withReturnFlight.Value)
            {
                var returnFlightResponse = await _flightService.GetNearestReturnFlight(flight.ArrivalAirportId, flight.DepartureAirportId, flight.ArrivalDateTime);
                var returnFlight = returnFlightResponse.Data.FirstOrDefault();
                viewModel.ReturnFlight = returnFlight;
                viewModel.ReturnFlightNumber = returnFlight.FlightNumber;
                var availableReturnFlightDates = await _flightService.GetAvailableFlightDates(returnFlight.FlightNumber);
                viewModel.AvailableReturnFlightDates = availableReturnFlightDates.Data.ToList();
            }

            return View("Index", viewModel);
        }

        public async Task<IActionResult> Overview(string flightNumber, DateTime selectedFlightDate, string? returnFlightNumber, DateTime? selectedReturnFlightDate)
        {
            var flightResponse = await _flightService.GetFlightByNumberAndDate(flightNumber, selectedFlightDate);

            if (flightResponse.Data.Count() == 0)
            {
                return RedirectToError("There are no available flights for this destination on selected dates.");
            }

            var flight = flightResponse.Data.FirstOrDefault();

            var viewModel = new Models.Flight.OverviewViewModel()
            {
                Flight = flightResponse.Data.FirstOrDefault()
            };

            if (selectedReturnFlightDate != null)
            {
                var returnFlightResponse = await _flightService.GetFlightByNumberAndDate(returnFlightNumber, selectedReturnFlightDate.Value);
                viewModel.WithReturnFlight = true;
                viewModel.ReturnFlight = returnFlightResponse.Data.FirstOrDefault();
            }

            return View("Overview", viewModel);
        }
    }
}
