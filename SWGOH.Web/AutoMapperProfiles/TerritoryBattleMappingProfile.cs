using AutoMapper;
using SWGOH.Entities;
using SWGOH.Web.ViewModels;
using System.Linq;

namespace SWGOH.Web.AutoMapperProfiles
{
    public class TerritoryBattleMappingProfile : Profile
    {
        public TerritoryBattleMappingProfile()
        {
            CreateMap<TerritoryBattle, TerritoryBattleModel>()
                .ForMember(dest => dest.Phase1, mo => mo.MapFrom(src => src.TerritoryBattlePhases.Where(x => x.Phase == 1).FirstOrDefault()))
                .ForMember(dest => dest.Phase2, mo => mo.MapFrom(src => src.TerritoryBattlePhases.Where(x => x.Phase == 2).FirstOrDefault()))
                .ForMember(dest => dest.Phase3, mo => mo.MapFrom(src => src.TerritoryBattlePhases.Where(x => x.Phase == 3).FirstOrDefault()))
                .ForMember(dest => dest.Phase4, mo => mo.MapFrom(src => src.TerritoryBattlePhases.Where(x => x.Phase == 4).FirstOrDefault()))
                .ForMember(dest => dest.Phase5, mo => mo.MapFrom(src => src.TerritoryBattlePhases.Where(x => x.Phase == 5).FirstOrDefault()))
                .ForMember(dest => dest.Phase6, mo => mo.MapFrom(src => src.TerritoryBattlePhases.Where(x => x.Phase == 6).FirstOrDefault()));

            CreateMap<TerritoryBattle, TerritoryBattleIndexModel>()
                .ForMember(dest => dest.Phase1_Id, mo => mo.MapFrom(src => src.TerritoryBattlePhases.Where(x => x.Phase == 1).FirstOrDefault().Id))
                .ForMember(dest => dest.Phase2_Id, mo => mo.MapFrom(src => src.TerritoryBattlePhases.Where(x => x.Phase == 2).FirstOrDefault().Id))
                .ForMember(dest => dest.Phase3_Id, mo => mo.MapFrom(src => src.TerritoryBattlePhases.Where(x => x.Phase == 3).FirstOrDefault().Id))
                .ForMember(dest => dest.Phase4_Id, mo => mo.MapFrom(src => src.TerritoryBattlePhases.Where(x => x.Phase == 4).FirstOrDefault().Id))
                .ForMember(dest => dest.Phase5_Id, mo => mo.MapFrom(src => src.TerritoryBattlePhases.Where(x => x.Phase == 5).FirstOrDefault().Id))
                .ForMember(dest => dest.Phase6_Id, mo => mo.MapFrom(src => src.TerritoryBattlePhases.Where(x => x.Phase == 6).FirstOrDefault().Id));

            CreateMap<TerritoryBattle, TerritoryBattleResultsModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.StartDate))
                .ForMember(dest => dest.Phase1Points, opt => opt.MapFrom(src => src.TerritoryBattlePhases.FirstOrDefault(x => x.Phase == 1).PhaseTerritories.FirstOrDefault().TotalPointsEarned))
                .ForMember(dest => dest.Phase2TopCharPoints, opt => opt.MapFrom(src => src.TerritoryBattlePhases.FirstOrDefault(x => x.Phase == 2).PhaseTerritories.FirstOrDefault(x => x.PhaseLocation == "Middle").TotalPointsEarned))
                .ForMember(dest => dest.Phase2BottomCharPoints, opt => opt.MapFrom(src => src.TerritoryBattlePhases.FirstOrDefault(x => x.Phase == 2).PhaseTerritories.FirstOrDefault(x => x.PhaseLocation == "Bottom").TotalPointsEarned))
                .ForMember(dest => dest.Phase3FleetPoints, opt => opt.MapFrom(src => src.TerritoryBattlePhases.FirstOrDefault(x => x.Phase == 3).PhaseTerritories.FirstOrDefault(x => x.PhaseLocation == "Top").TotalPointsEarned))
                .ForMember(dest => dest.Phase3TopCharPoints, opt => opt.MapFrom(src => src.TerritoryBattlePhases.FirstOrDefault(x => x.Phase == 3).PhaseTerritories.FirstOrDefault(x => x.PhaseLocation == "Middle").TotalPointsEarned))
                .ForMember(dest => dest.Phase3BottomCharPoints, opt => opt.MapFrom(src => src.TerritoryBattlePhases.FirstOrDefault(x => x.Phase == 3).PhaseTerritories.FirstOrDefault(x => x.PhaseLocation == "Bottom").TotalPointsEarned))
                .ForMember(dest => dest.Phase4FleetPoints, opt => opt.MapFrom(src => src.TerritoryBattlePhases.FirstOrDefault(x => x.Phase == 4).PhaseTerritories.FirstOrDefault(x => x.PhaseLocation == "Top").TotalPointsEarned))
                .ForMember(dest => dest.Phase4TopCharPoints, opt => opt.MapFrom(src => src.TerritoryBattlePhases.FirstOrDefault(x => x.Phase == 4).PhaseTerritories.FirstOrDefault(x => x.PhaseLocation == "Middle").TotalPointsEarned))
                .ForMember(dest => dest.Phase4BottomCharPoints, opt => opt.MapFrom(src => src.TerritoryBattlePhases.FirstOrDefault(x => x.Phase == 4).PhaseTerritories.FirstOrDefault(x => x.PhaseLocation == "Bottom").TotalPointsEarned))
                .ForMember(dest => dest.Phase5FleetPoints, opt => opt.MapFrom(src => src.TerritoryBattlePhases.FirstOrDefault(x => x.Phase == 5).PhaseTerritories.FirstOrDefault(x => x.PhaseLocation == "Top").TotalPointsEarned))
                .ForMember(dest => dest.Phase5TopCharPoints, opt => opt.MapFrom(src => src.TerritoryBattlePhases.FirstOrDefault(x => x.Phase == 5).PhaseTerritories.FirstOrDefault(x => x.PhaseLocation == "Middle").TotalPointsEarned))
                .ForMember(dest => dest.Phase5BottomCharPoints, opt => opt.MapFrom(src => src.TerritoryBattlePhases.FirstOrDefault(x => x.Phase == 5).PhaseTerritories.FirstOrDefault(x => x.PhaseLocation == "Bottom").TotalPointsEarned))
                .ForMember(dest => dest.Phase6FleetPoints, opt => opt.MapFrom(src => src.TerritoryBattlePhases.FirstOrDefault(x => x.Phase == 6).PhaseTerritories.FirstOrDefault(x => x.PhaseLocation == "Top").TotalPointsEarned))
                .ForMember(dest => dest.Phase6TopCharPoints, opt => opt.MapFrom(src => src.TerritoryBattlePhases.FirstOrDefault(x => x.Phase == 6).PhaseTerritories.FirstOrDefault(x => x.PhaseLocation == "Middle").TotalPointsEarned))
                .ForMember(dest => dest.Phase6BottomCharPoints, opt => opt.MapFrom(src => src.TerritoryBattlePhases.FirstOrDefault(x => x.Phase == 6).PhaseTerritories.FirstOrDefault(x => x.PhaseLocation == "Bottom").TotalPointsEarned));
        }
    }
}