using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SWGOH.Web.ViewModels
{
    public class TerritoryPhaseReportModel
    {
        public Guid Id { get; set; }
        public Guid? MemberId { get; set; }
        public SelectList Members { get; set; }
    }
}