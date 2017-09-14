﻿using AutoMapper;
using SWGOH.Entities;
using SWGOH.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWGOH.Web.AutoMapperProfiles
{
    public class TerritoryPlatoonMappingProfile : Profile
    {
        public TerritoryPlatoonMappingProfile()
        {
            CreateMap<TerritoryPlatoon, TerritoryPlatoonModel>()
                .ForMember(dest => dest.Character1, mo => mo.MapFrom(src => src.PlatoonCharacters.Where(x => x.PlatoonPosition == 1).FirstOrDefault()))
                .ForMember(dest => dest.Character2, mo => mo.MapFrom(src => src.PlatoonCharacters.Where(x => x.PlatoonPosition == 2).FirstOrDefault()))
                .ForMember(dest => dest.Character3, mo => mo.MapFrom(src => src.PlatoonCharacters.Where(x => x.PlatoonPosition == 3).FirstOrDefault()))
                .ForMember(dest => dest.Character4, mo => mo.MapFrom(src => src.PlatoonCharacters.Where(x => x.PlatoonPosition == 4).FirstOrDefault()))
                .ForMember(dest => dest.Character5, mo => mo.MapFrom(src => src.PlatoonCharacters.Where(x => x.PlatoonPosition == 5).FirstOrDefault()))
                .ForMember(dest => dest.Character6, mo => mo.MapFrom(src => src.PlatoonCharacters.Where(x => x.PlatoonPosition == 6).FirstOrDefault()))
                .ForMember(dest => dest.Character7, mo => mo.MapFrom(src => src.PlatoonCharacters.Where(x => x.PlatoonPosition == 7).FirstOrDefault()))
                .ForMember(dest => dest.Character8, mo => mo.MapFrom(src => src.PlatoonCharacters.Where(x => x.PlatoonPosition == 8).FirstOrDefault()))
                .ForMember(dest => dest.Character9, mo => mo.MapFrom(src => src.PlatoonCharacters.Where(x => x.PlatoonPosition == 9).FirstOrDefault()))
                .ForMember(dest => dest.Character10, mo => mo.MapFrom(src => src.PlatoonCharacters.Where(x => x.PlatoonPosition == 10).FirstOrDefault()))
                .ForMember(dest => dest.Character11, mo => mo.MapFrom(src => src.PlatoonCharacters.Where(x => x.PlatoonPosition == 11).FirstOrDefault()))
                .ForMember(dest => dest.Character12, mo => mo.MapFrom(src => src.PlatoonCharacters.Where(x => x.PlatoonPosition == 12).FirstOrDefault()))
                .ForMember(dest => dest.Character13, mo => mo.MapFrom(src => src.PlatoonCharacters.Where(x => x.PlatoonPosition == 13).FirstOrDefault()))
                .ForMember(dest => dest.Character14, mo => mo.MapFrom(src => src.PlatoonCharacters.Where(x => x.PlatoonPosition == 14).FirstOrDefault()))
                .ForMember(dest => dest.Character15, mo => mo.MapFrom(src => src.PlatoonCharacters.Where(x => x.PlatoonPosition == 15).FirstOrDefault()));
        }

    }
}