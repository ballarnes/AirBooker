using AirBooker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AirBooker.Web.Controllers
{
    public class BaseController : Controller
    {
        private readonly ILogger<BaseController> _logger;

        public BaseController(
            ILogger<BaseController> logger)
        {
            _logger = logger;
        }

        public IActionResult RedirectToError(string? message = null, bool requiresAdditionalInfo = false)
        {
            return RedirectToAction("Error", new { message = message, requiresAdditionalInfo = requiresAdditionalInfo });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(string? message = null, bool requiresAdditionalInfo = false)
        {
            var viewModel = new ErrorViewModel()
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                ErrorMessage = message,
                RequiresAdditionalInfo = requiresAdditionalInfo,
            };

            return View(viewModel);
        }
    }
}
