using AutoMapper;
using Domain.DTOs;
using Domain.Models;


namespace Application.Mappings
{
    public class RestaurantProfile : Profile
    {
        public RestaurantProfile()
        {
            CreateMap<Restaurant, RestaurantDto>().ReverseMap();
            CreateMap<Restaurant, RestaurantCreateDto>().ReverseMap();
            CreateMap<Restaurant, RestaurantUpdateDto>().ReverseMap();
        }
    }
}
