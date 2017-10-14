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
        }
    }
}