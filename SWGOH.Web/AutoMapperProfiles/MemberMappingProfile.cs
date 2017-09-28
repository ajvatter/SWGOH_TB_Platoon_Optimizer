using AutoMapper;
using SWGOH.Entities;
using SWGOH.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWGOH.Web.AutoMapperProfiles
{
    public class MemberMappingProfile : Profile
    {
        public MemberMappingProfile()
        {
            CreateMap<Member, MemberModel>()                
                .ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.DisplayName))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ShipPower, opt => opt.MapFrom(src => src.ShipPower))
                .ForMember(dest => dest.CharacterPower, opt => opt.MapFrom(src => src.CharacterPower))
                .ForMember(dest => dest.UrlExt, opt => opt.MapFrom(src => src.UrlExt));
        }
    }
}