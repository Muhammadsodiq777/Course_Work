using AutoMapper;
using HotelListing.Data;
using HotelListing.Models;
using Microsoft.AspNetCore.Identity;

namespace HotelListing.Configurations
{
    public class MapperInitilizer: Profile
    {
        public MapperInitilizer()
        {
            CreateMap<Collection, CollectionDTO>().ReverseMap();
            CreateMap<Collection, CreateCollectionDTO>().ReverseMap();
            CreateMap<ApiUser,UserDTO>().ReverseMap();

        }
    }
}
