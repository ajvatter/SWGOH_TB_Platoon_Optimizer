using AutoMapper;
using SWGOH.Entities;
using SWGOH.Web.DataContexts;
using SWGOH.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Caching;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using EntityFramework.BulkExtensions;
using Kendo.Mvc;
using System.Linq.Expressions;

namespace SWGOH.Web.Controllers
{
    public class DashboardController : Controller
    {
        private SwgohDb db = new SwgohDb();
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        
    }
}