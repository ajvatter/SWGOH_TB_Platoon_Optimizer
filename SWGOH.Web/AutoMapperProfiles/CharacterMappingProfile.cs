using AutoMapper;
using SWGOH.Entities;
using SWGOH.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWGOH.Web.AutoMapperProfiles
{
    public class CharacterMappingProfile : Profile
    {
        public CharacterMappingProfile()
        {
            CreateMap<Character, CharacterModel>()                
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Alignment, opt => opt.MapFrom(src => Enum.GetName(typeof(Alignment), src.Alignment)));                
        }

    }
}