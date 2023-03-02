using AirBooker.Data.Entities;
using AirBooker.Domain.Models.Dtos;
using AutoMapper;
using Microsoft.Extensions.Options;

namespace AirBooker.Web.Mapping
{
    public class PictureFileNameResolver : IMemberValueResolver<Airline, AirlineDto, string, object>, IMemberValueResolver<Airport, AirportDto, string, object>
    {
        private readonly AirBookerConfig _config;

        public PictureFileNameResolver(IOptionsSnapshot<AirBookerConfig> config)
        {
            _config = config.Value;
        }

        public object Resolve(Airline source, AirlineDto destination, string sourceMember, object destMember, ResolutionContext context)
        {
            return $"{_config.CdnHost}/{_config.ImgUrl}/{sourceMember}";
        }

        public object Resolve(Airport source, AirportDto destination, string sourceMember, object destMember, ResolutionContext context)
        {
            return $"{_config.CdnHost}/{_config.ImgUrl}/{sourceMember}";
        }
    }
}
