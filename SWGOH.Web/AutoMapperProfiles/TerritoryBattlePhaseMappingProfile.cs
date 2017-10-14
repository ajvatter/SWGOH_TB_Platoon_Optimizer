using AutoMapper;
using SWGOH.Entities;
using SWGOH.Web.ViewModels;
using System.Linq;

namespace SWGOH.Web.AutoMapperProfiles
{
    public class TerritoryBattlePhaseMappingProfile : Profile
    {
        public TerritoryBattlePhaseMappingProfile()
        {
            CreateMap<TerritoryBattlePhase, TerritoryBattlePhaseModel>()
                .ForMember(dest => dest.Territory1, mo => mo.MapFrom(src => src.PhaseTerritories.Where(x => x.PhaseLocation == "Middle").FirstOrDefault()))
                .ForMember(dest => dest.Territory2, mo => mo.MapFrom(src => src.PhaseTerritories.Where(x => x.PhaseLocation == "Bottom").FirstOrDefault()))
                .ForMember(dest => dest.Territory3, mo => mo.MapFrom(src => src.PhaseTerritories.Where(x => x.PhaseLocation == "Top" ).FirstOrDefault()));

            CreateMap<TerritoryBattlePhase, PlatoonClosureModel>()
                .ForMember(dest => dest.Top1, opt => opt.MapFrom(src => src.PhaseTerritories.Where(x => x.PhaseLocation == "Top").FirstOrDefault().TerritoryPlatoons.Where(x => x.PlatoonNumber == 1).FirstOrDefault().IsClosed))
                .ForMember(dest => dest.Top2, opt => opt.MapFrom(src => src.PhaseTerritories.Where(x => x.PhaseLocation == "Top").FirstOrDefault().TerritoryPlatoons.Where(x => x.PlatoonNumber == 2).FirstOrDefault().IsClosed))
                .ForMember(dest => dest.Top3, opt => opt.MapFrom(src => src.PhaseTerritories.Where(x => x.PhaseLocation == "Top").FirstOrDefault().TerritoryPlatoons.Where(x => x.PlatoonNumber == 3).FirstOrDefault().IsClosed))
                .ForMember(dest => dest.Top4, opt => opt.MapFrom(src => src.PhaseTerritories.Where(x => x.PhaseLocation == "Top").FirstOrDefault().TerritoryPlatoons.Where(x => x.PlatoonNumber == 4).FirstOrDefault().IsClosed))
                .ForMember(dest => dest.Top5, opt => opt.MapFrom(src => src.PhaseTerritories.Where(x => x.PhaseLocation == "Top").FirstOrDefault().TerritoryPlatoons.Where(x => x.PlatoonNumber == 5).FirstOrDefault().IsClosed))
                .ForMember(dest => dest.Top6, opt => opt.MapFrom(src => src.PhaseTerritories.Where(x => x.PhaseLocation == "Top").FirstOrDefault().TerritoryPlatoons.Where(x => x.PlatoonNumber == 6).FirstOrDefault().IsClosed))
                .ForMember(dest => dest.Middle1, opt => opt.MapFrom(src => src.PhaseTerritories.Where(x => x.PhaseLocation == "Middle").FirstOrDefault().TerritoryPlatoons.Where(x => x.PlatoonNumber == 1).FirstOrDefault().IsClosed))
                .ForMember(dest => dest.Middle2, opt => opt.MapFrom(src => src.PhaseTerritories.Where(x => x.PhaseLocation == "Middle").FirstOrDefault().TerritoryPlatoons.Where(x => x.PlatoonNumber == 2).FirstOrDefault().IsClosed))
                .ForMember(dest => dest.Middle3, opt => opt.MapFrom(src => src.PhaseTerritories.Where(x => x.PhaseLocation == "Middle").FirstOrDefault().TerritoryPlatoons.Where(x => x.PlatoonNumber == 3).FirstOrDefault().IsClosed))
                .ForMember(dest => dest.Middle4, opt => opt.MapFrom(src => src.PhaseTerritories.Where(x => x.PhaseLocation == "Middle").FirstOrDefault().TerritoryPlatoons.Where(x => x.PlatoonNumber == 4).FirstOrDefault().IsClosed))
                .ForMember(dest => dest.Middle5, opt => opt.MapFrom(src => src.PhaseTerritories.Where(x => x.PhaseLocation == "Middle").FirstOrDefault().TerritoryPlatoons.Where(x => x.PlatoonNumber == 5).FirstOrDefault().IsClosed))
                .ForMember(dest => dest.Middle6, opt => opt.MapFrom(src => src.PhaseTerritories.Where(x => x.PhaseLocation == "Middle").FirstOrDefault().TerritoryPlatoons.Where(x => x.PlatoonNumber == 6).FirstOrDefault().IsClosed))
                .ForMember(dest => dest.Bottom1, opt => opt.MapFrom(src => src.PhaseTerritories.Where(x => x.PhaseLocation == "Bottom").FirstOrDefault().TerritoryPlatoons.Where(x => x.PlatoonNumber == 1).FirstOrDefault().IsClosed))
                .ForMember(dest => dest.Bottom2, opt => opt.MapFrom(src => src.PhaseTerritories.Where(x => x.PhaseLocation == "Bottom").FirstOrDefault().TerritoryPlatoons.Where(x => x.PlatoonNumber == 2).FirstOrDefault().IsClosed))
                .ForMember(dest => dest.Bottom3, opt => opt.MapFrom(src => src.PhaseTerritories.Where(x => x.PhaseLocation == "Bottom").FirstOrDefault().TerritoryPlatoons.Where(x => x.PlatoonNumber == 3).FirstOrDefault().IsClosed))
                .ForMember(dest => dest.Bottom4, opt => opt.MapFrom(src => src.PhaseTerritories.Where(x => x.PhaseLocation == "Bottom").FirstOrDefault().TerritoryPlatoons.Where(x => x.PlatoonNumber == 4).FirstOrDefault().IsClosed))
                .ForMember(dest => dest.Bottom5, opt => opt.MapFrom(src => src.PhaseTerritories.Where(x => x.PhaseLocation == "Bottom").FirstOrDefault().TerritoryPlatoons.Where(x => x.PlatoonNumber == 5).FirstOrDefault().IsClosed))
                .ForMember(dest => dest.Bottom6, opt => opt.MapFrom(src => src.PhaseTerritories.Where(x => x.PhaseLocation == "Bottom").FirstOrDefault().TerritoryPlatoons.Where(x => x.PlatoonNumber == 6).FirstOrDefault().IsClosed));
        }
    }
}
