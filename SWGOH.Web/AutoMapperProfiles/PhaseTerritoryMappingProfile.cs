using AutoMapper;
using SWGOH.Entities;
using SWGOH.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWGOH.Web.AutoMapperProfiles
{
    public class PhaseTerritoryMappingProfile : Profile
    {
        public PhaseTerritoryMappingProfile()
        {
            CreateMap<PhaseTerritory, PhaseTerritoryModel>()
                .ForMember(dest => dest.TerritoryPlatoon1, mo => mo.MapFrom(src => src.TerritoryPlatoons.Where(x => x.PlatoonNumber == 1).FirstOrDefault()))
                .ForMember(dest => dest.TerritoryPlatoon2, mo => mo.MapFrom(src => src.TerritoryPlatoons.Where(x => x.PlatoonNumber == 2).FirstOrDefault()))
                .ForMember(dest => dest.TerritoryPlatoon3, mo => mo.MapFrom(src => src.TerritoryPlatoons.Where(x => x.PlatoonNumber == 3).FirstOrDefault()))
                .ForMember(dest => dest.TerritoryPlatoon4, mo => mo.MapFrom(src => src.TerritoryPlatoons.Where(x => x.PlatoonNumber == 4).FirstOrDefault()))
                .ForMember(dest => dest.TerritoryPlatoon5, mo => mo.MapFrom(src => src.TerritoryPlatoons.Where(x => x.PlatoonNumber == 5).FirstOrDefault()))
                .ForMember(dest => dest.TerritoryPlatoon6, mo => mo.MapFrom(src => src.TerritoryPlatoons.Where(x => x.PlatoonNumber == 6).FirstOrDefault()));
        }

    }
}