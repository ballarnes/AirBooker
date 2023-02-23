using AirBooker.Domain.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AirBooker.Web.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly IBookingService _bookingService;
        private readonly IDocumentService _documentService;

        public const string ReceiptMarkup = "<h1 style=\"text-align:center\">Receipt for <strong>{0}</strong></h1>\r\n\r\n<hr />\r\n<table border=\"1\" cellpadding=\"1\" cellspacing=\"1\" style=\"margin-top:30px; width:100%\">\r\n\t<tbody>\r\n\t\t<tr>\r\n\t\t\t<td style=\"width:50%\">Booking №: <strong>{1}</strong></td>\r\n\t\t\t<td style=\"text-align:right; width:50%\">Date: <strong>{2}</strong></td>\r\n\t\t</tr>\r\n\t</tbody>\r\n</table>\r\n\r\n<h3 style=\"text-align:center\">Your info:</h3>\r\n\r\n<table border=\"1\" cellpadding=\"1\" cellspacing=\"1\" style=\"margin-top:30px; width:100%\">\r\n\t<tbody>\r\n\t\t<tr>\r\n\t\t\t<td style=\"width:50%\">Username: <strong>{0}</strong></td>\r\n\t\t\t<td style=\"text-align:right; width:50%\">Email: <strong>{3}</strong></td>\r\n\t\t</tr>\r\n\t</tbody>\r\n</table>\r\n\r\n<p>&nbsp;</p>\r\n\r\n<h3 style=\"text-align:center\">Ticket info:</h3>\r\n\r\n<table border=\"1\" cellpadding=\"1\" cellspacing=\"1\" style=\"margin-bottom:30px; width:100%\">\r\n\t<tbody>\r\n\t\t<tr>\r\n\t\t\t<td style=\"width:50%\">Airline: <strong>{4}</strong></td>\r\n\t\t\t<td style=\"text-align:right; width:50%\">Flight №: <strong>{5}</strong></td>\r\n\t\t</tr>\r\n\t\t<tr>\r\n\t\t\t<td style=\"width:50%\">Departure: <strong>{6}</strong></td>\r\n\t\t\t<td style=\"text-align:right; width:50%\">Arrival: <strong>{7}</strong></td>\r\n\t\t</tr>\r\n\t\t<tr>\r\n\t\t\t<td style=\"width:50%\">Departure airport: <strong>{8}</strong></td>\r\n\t\t\t<td style=\"text-align:right; width:50%\">Arrival airport: <strong>{9}</strong></td>\r\n\t\t</tr>\r\n\t</tbody>\r\n</table>\r\n\r\n<hr />\r\n<p>&nbsp;</p>\r\n\r\n<h3 style=\"text-align:center\">AirBooker - 2023</h3>\r\n";

        public PaymentController(
            ILogger<PaymentController> logger,
            IBookingService bookingService,
            IDocumentService documentService)
            : base(logger)
        {
            _bookingService = bookingService;
            _documentService = documentService;
        }

        public IActionResult Index(Guid flightId, Guid? returnFlightId)
        {
            var viewModel = new Models.Payment.PaymentViewModel()
            {
                FlightId = flightId
            };

            if (returnFlightId != null)
            {
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

            var flightBookingHTML = string.Format(ReceiptMarkup,
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

                var returnFlightBookingHTML = string.Format(ReceiptMarkup,
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
