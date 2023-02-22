using AirBooker.Domain.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AirBooker.Web.Controllers
{
    public class PaymentController : BaseController
    {
        private readonly IBookingService _bookingService;
        private readonly IDocumentService _documentService;

        public const string ReceiptMarkup = "<h1 style=\"text-align:center\">Receipt for <strong>{0}</strong></h1>\r\n\r\n<hr />\r\n<table class=\"table\" style=\"width:1200.6px\">\r\n\t<thead>\r\n\t\t<tr>\r\n\t\t\t<th scope=\"col\" style=\"text-align:left; width:50%\">Booking №: {1}</th>\r\n\t\t\t<th scope=\"col\" style=\"text-align:right; width:50%\">Date: {2}</th>\r\n\t\t</tr>\r\n\t</thead>\r\n</table>\r\n\r\n<table border=\"1\" cellpadding=\"1\" cellspacing=\"1\" style=\"width:500px\">\r\n\t<caption>\r\n\t<p>&nbsp;</p>\r\n\r\n\t<p><strong>Your data:</strong></p>\r\n\t</caption>\r\n\t<thead>\r\n\t\t<tr>\r\n\t\t\t<th scope=\"col\">Username: {0}</th>\r\n\t\t</tr>\r\n\t</thead>\r\n\t<tbody>\r\n\t\t<tr>\r\n\t\t\t<td style=\"text-align:center\">Email: {0}</td>\r\n\t\t</tr>\r\n\t</tbody>\r\n</table>\r\n\r\n<table border=\"1\" cellpadding=\"1\" cellspacing=\"1\" style=\"width:500px\">\r\n\t<caption>\r\n\t<p>&nbsp;</p>\r\n\r\n\t<p><strong>Ticket Info:</strong></p>\r\n\t</caption>\r\n\t<thead>\r\n\t\t<tr>\r\n\t\t\t<th scope=\"col\">Airline: {4}</th>\r\n\t\t</tr>\r\n\t</thead>\r\n\t<tbody>\r\n\t\t<tr>\r\n\t\t\t<td style=\"text-align:center\">Flight №: {5}</td>\r\n\t\t</tr>\r\n\t\t<tr>\r\n\t\t\t<td style=\"text-align:center\">Departure: {6}</td>\r\n\t\t</tr>\r\n\t\t<tr>\r\n\t\t\t<td style=\"text-align:center\">Arrival: {7}</td>\r\n\t\t</tr>\r\n\t</tbody>\r\n</table>\r\n\r\n<p>&nbsp;</p>\r\n\r\n<hr />\r\n<p style=\"text-align:center\">AirBooker - 2023</p>\r\n";

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

            var pageHTML = string.Format(ReceiptMarkup, 
                flightBooking.User.UserName, 
                flightBookingId, 
                flightBooking.BookingDateTime, 
                flightBooking.User.Email, 
                flightBooking.Flight.Airline.Name,
                flightBooking.Flight.FlightNumber,
                flightBooking.Flight.DepartureDateTime,
                flightBooking.Flight.ArrivalDateTime);

            using (MemoryStream stream = new MemoryStream())
            {
                var document = _documentService.CreatePDFDocument(pageHTML);
                document.Save(stream, false);
                return File(stream.ToArray(), "application/pdf");
            }
        }
    }
}
