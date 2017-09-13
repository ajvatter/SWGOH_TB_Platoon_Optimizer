using AutoMapper;
using SWGOH.Entities;
using SWGOH.Web.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SWGOH.Web.App_Start
{
    public static class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfiles(typeof(AutoMapperConfig));
            });
        }

    }
}