using SWGOH.Web.DataContexts;
using System.Web.Mvc;

namespace SWGOH.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            var dbConn = new SwgohDb();
            ViewBag.Connection = dbConn.Database.Connection.ConnectionString;
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult PrivacyPolicy()
        {
            return View();
        }
    }
}