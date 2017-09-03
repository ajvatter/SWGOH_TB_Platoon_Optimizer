using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class CharacterModel : BaseModel
    {
        public string Name { get; set; }
        public string UrlExt { get; set; }
    }
}