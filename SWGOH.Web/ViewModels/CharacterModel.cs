using SWGOH.Entities;
using System;

namespace SWGOH.Web.ViewModels
{
    public class CharacterModel
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string Alignment { get; set; }
    }
}