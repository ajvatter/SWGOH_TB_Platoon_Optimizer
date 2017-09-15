using SWGOH.Entities;
using SWGOH.Web.DataContexts;
using SWGOH.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SWGOH.Web.Controllers
{
    public class ReportsController : Controller
    {
        private SwgohDb db = new SwgohDb();
        // GET: Reports
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult PlatoonAssignmentsByCharacter(Guid id)
        {
            TerritoryBattlePhase phase = db.TerritoryBattlePhases.Find(id);

            DataSet ds = new DataSet("Assignments");
            List<PlatoonAssignmentsByCharacter> model = new List<ViewModels.PlatoonAssignmentsByCharacter>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SwgohDb"].ConnectionString))
            {
                SqlCommand sqlComm = new SqlCommand("PlattonAssignmentsByCharacter", conn);
                //sqlComm.Parameters.AddWithValue("@guildGuid", phase.TerritoryBattle.Guild_Id);
                sqlComm.Parameters.AddWithValue("@phaseGuid", phase.Id);

                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);

                model = ds.Tables[0].AsEnumerable().Select(
                    dataRow => new PlatoonAssignmentsByCharacter
                    {
                        CharacterName = dataRow.Field<string>("DisplayName"),
                        NeededCount = dataRow.Field<int>("NeedCount"),
                        HaveCount = dataRow.Field<int>("HaveCount"),
                        AssignedMembers = dataRow.Field<string>("AssignedMembers").Replace(",", "<br/>")
                    }).ToList();
            }

            return View(model);
        }
    }
}