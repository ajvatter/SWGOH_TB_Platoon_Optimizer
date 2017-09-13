using AutoMapper;
using SWGOH.Entities;
using SWGOH.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWGOH.Web.AutoMapperProfiles
{
    public class TerritoryBattlePhaseMappingProfile : Profile
    {
        public TerritoryBattlePhaseMappingProfile()
        {
            CreateMap<TerritoryBattlePhase, TerritoryBattlePhaseModel>()
                .ForMember(dest => dest.Territory1, mo => mo.MapFrom(src => src.PhaseTerritories.Where(x => x.PhaseLocation == "Middle").FirstOrDefault()))
                .ForMember(dest => dest.Territory2, mo => mo.MapFrom(src => src.PhaseTerritories.Where(x => x.PhaseLocation == "Middle").FirstOrDefault()));               
        }
    
    }
}