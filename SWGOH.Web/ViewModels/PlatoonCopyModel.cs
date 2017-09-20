using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SWGOH.Web.ViewModels
{
    public class PlatoonCopyModel
    {
        public Guid CopyToPlatoonId { get; set; }
        public Guid SelectedPlatoonId { get; set; }
        public SelectList PlatoonOptions { get; set; }
    }
}