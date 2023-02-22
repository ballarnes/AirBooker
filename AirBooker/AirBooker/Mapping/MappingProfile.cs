using AirBooker.Data.Entities;
using AirBooker.Domain.Models.Dtos;
using AutoMapper;

namespace AirBooker.Web.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<AirlineAirport, AirlineAirportDto>();
            CreateMap<Airport, AirportDto>()
                .ForMember("CityPhotoUrl", opt
                    => opt.MapFrom<PictureFileNameResolver, string>(x => x.CityPhotoFileName!))
                .ForMember("PhotoUrl", opt
                    => opt.MapFrom<PictureFileNameResolver, string>(x => x.AirportPhotoFileName!));
            CreateMap<Booking, BookingDto>();
            CreateMap<Flight, FlightDto>();
            CreateMap<Airline, AirlineDto>()
                .ForMember("LogoUrl", opt
                    => opt.MapFrom<PictureFileNameResolver, string>(x => x.LogoFileName!));
        }
    }
}
