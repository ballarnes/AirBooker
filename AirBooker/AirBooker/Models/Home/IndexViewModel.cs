using AirBooker.Data.Entities;
using AirBooker.Domain.Models.Dtos;
using AirBooker.Web.Models.Pagination;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AirBooker.Web.Models.Home
{
    public class IndexViewModel
    {
        public string? SelectedAirportId { get; set; }
        public IEnumerable<SelectListItem>? DepartureAirports { get; set; }
        public IEnumerable<FlightDto>? Flights { get; set; }
        public PaginationInfo? PaginationInfo { get; set; }
    }
}
