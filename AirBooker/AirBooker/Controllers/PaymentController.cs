using AirBooker.Domain.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System.Security.Claims;

namespace AirBooker.Web.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly IFlightService _flightService;
        private readonly IBookingService _bookingService;
        private readonly IDocumentService _documentService;
        private readonly IStringLocalizer<PaymentController> _localizer;

        public PaymentController(
            ILogger<PaymentController> logger,
            IFlightService flightService,
            IBookingService bookingService,
            IDocumentService documentService,
            IStringLocalizer<PaymentController> localizer)
            : base(logger)
        {
            _flightService = flightService;
            _bookingService = bookingService;
            _documentService = documentService;
            _localizer = localizer;
        }

        public async Task<IActionResult> Index(Guid flightId, Guid? returnFlightId)
        {
            var flightResponse = await _flightService.GetFlightById(flightId);

            var viewModel = new Models.Payment.PaymentViewModel()
            {
                Flight = flightResponse.Data.FirstOrDefault(),
                FlightId = flightId
            };

            if (returnFlightId != null)
            {
                var returnFlightResponse = await _flightService.GetFlightById(returnFlightId.Value);
                viewModel.ReturnFlight = returnFlightResponse.Data.FirstOrDefault();
                viewModel.ReturnFlightId = returnFlightId;
            }

            return View(viewModel);
        }

        public async Task<IActionResult> CreateBooking(Guid flightId, Guid? returnFlightId)
        {
            var response = await _bookingService.CreateBooking(User.FindFirstValue(ClaimTypes.NameIdentifier), flightId, returnFlightId);

            var viewModel = new Models.Payment.PaymentResultViewModel();

            if (response != null)
            {
                viewModel.FlightBookingId = response.FlightBookingId;
                viewModel.ReturnFlightBookingId = response.ReturnFlightBookindId;
                viewModel.OperationSuccess = true;
            }
            else
            {
                viewModel.OperationSuccess = false;
            }

            return View("PaymentResult", viewModel);
        }

        public async Task<IActionResult> GetReceipt(Guid flightBookingId, Guid? returnFlightBookingId)
        {
            var flightBookingResponse = await _bookingService.GetBookingById(flightBookingId);
            var flightBooking = flightBookingResponse.Data.FirstOrDefault();

            var flightBookingHTML = string.Format(_localizer["ReceiptMarkup"],
                flightBooking.User.UserName,
                flightBookingId,
                flightBooking.BookingDateTime,
                flightBooking.User.Email,
                flightBooking.Flight.Airline.Name,
                flightBooking.Flight.FlightNumber,
                flightBooking.Flight.DepartureDateTime,
                flightBooking.Flight.ArrivalDateTime,
                flightBooking.Flight.DepartureAirport.Name,
                flightBooking.Flight.ArrivalAirport.Name);

            if (returnFlightBookingId != null)
            {
                var returnFlightBookingResponse = await _bookingService.GetBookingById(returnFlightBookingId.Value);
                var returnFlightBooking = returnFlightBookingResponse.Data.FirstOrDefault();

                var returnFlightBookingHTML = string.Format(_localizer["ReceiptMarkup"],
                    returnFlightBooking.User.UserName,
                    returnFlightBookingId,
                    returnFlightBooking.BookingDateTime,
                    returnFlightBooking.User.Email,
                    returnFlightBooking.Flight.Airline.Name,
                    returnFlightBooking.Flight.FlightNumber,
                    returnFlightBooking.Flight.DepartureDateTime,
                    returnFlightBooking.Flight.ArrivalDateTime,
                    returnFlightBooking.Flight.DepartureAirport.Name,
                    returnFlightBooking.Flight.ArrivalAirport.Name);

                var document = _documentService.CreateCombinedPDFDocument(new[] { flightBookingHTML, returnFlightBookingHTML } , $"Booking - {flightBooking.BookingDateTime}");
                return File(document, "application/pdf");
            }
            else
            {
                var document = _documentService.CreatePDFDocument(flightBookingHTML, $"Booking - {flightBooking.BookingDateTime}");
                return File(document, "application/pdf");
            }
        }
    }
}
