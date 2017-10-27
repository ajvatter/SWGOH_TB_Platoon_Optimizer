using AutoMapper;
using SWGOH.Entities;
using SWGOH.Web.ViewModels;

namespace SWGOH.Web.AutoMapperProfiles
{
    public class PhaseReportMappingProfile : Profile
    {
        public PhaseReportMappingProfile()
        {
            CreateMap<PhaseReport, PlatoonAssignmentsModel>()
                .ForMember(dest => dest.CharacterName, opt => opt.MapFrom(src => src.PlatoonCharacter.Character.DisplayName))
                .ForMember(dest => dest.Stars, opt => opt.MapFrom(src => src.MemberCharacter.Stars))
                .ForMember(dest => dest.Level, opt => opt.MapFrom(src => src.MemberCharacter.Level))
                .ForMember(dest => dest.Gear, opt => opt.MapFrom(src => src.MemberCharacter.Gear))
                .ForMember(dest => dest.Power, opt => opt.MapFrom(src => src.MemberCharacter.Power))
                .ForMember(dest => dest.AssignedMember, opt => opt.MapFrom(src => src.MemberCharacter.Member.DisplayName))
                .ForMember(dest => dest.AssignedPlatoon, opt => opt.MapFrom(src => src.PlatoonCharacter.TerritoryPlatoon.PhaseTerritory.PhaseLocation + " - " + src.PlatoonCharacter.TerritoryPlatoon.PlatoonNumber))
                .ForMember(dest => dest.ShipName, opt => opt.MapFrom(src => src.PlatoonShip.Ship.DisplayName))
                .ForMember(dest => dest.ShipStars, opt => opt.MapFrom(src => src.MemberShip.Stars))
                .ForMember(dest => dest.ShipLevel, opt => opt.MapFrom(src => src.MemberShip.Level))
                .ForMember(dest => dest.AssignedShipMember, opt => opt.MapFrom(src => src.MemberShip.Member.DisplayName))
                .ForMember(dest => dest.AssignedShipPlatoon, opt => opt.MapFrom(src => src.PlatoonShip.TerritoryPlatoon.PhaseTerritory.PhaseLocation + " - " + src.PlatoonShip.TerritoryPlatoon.PlatoonNumber));
        }
    }
}