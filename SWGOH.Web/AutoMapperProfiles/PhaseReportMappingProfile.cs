using AutoMapper;
using SWGOH.Entities;
using SWGOH.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
                .ForMember(dest => dest.AssignedMember, opt => opt.MapFrom(src => src.MemberCharacter.Member.DisplayName))
                .ForMember(dest => dest.AssignedPlatoon, opt => opt.MapFrom(src => src.PlatoonCharacter.TerritoryPlatoon.PhaseTerritory.PhaseLocation + " - " + src.PlatoonCharacter.TerritoryPlatoon.PlatoonNumber));
        }
    }
}