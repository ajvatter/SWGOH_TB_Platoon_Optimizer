using SWGOH.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWGOH.Web.ViewModels
{
    public class CharCount
    {
        public Guid Id { get; set; }
        public string Alignment { get; set; }
        public string Name { get; set; }
        public int OneStarCount { get; set; }
        public int TwoStarCount { get; set; }
        public int ThreeStarCount { get; set; }
        public int FourStarCount { get; set; }
        public int FiveStarCount { get; set; }
        public int SixStarCount { get; set; }
        public int SevenStarCount { get; set; }
    }
}