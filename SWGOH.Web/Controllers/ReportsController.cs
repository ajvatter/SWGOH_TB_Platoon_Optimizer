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
using System.Web.Caching;
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
            List<PlatoonAssignmentsByCharacter> model = (List<PlatoonAssignmentsByCharacter>)HttpContext.Cache.Get("PlatoonAssignmentsByCharacter" + id.ToString());
            if (model == null)
            {
                using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SwgohDb"].ConnectionString))
                {
                    SqlCommand sqlComm = new SqlCommand("PlattonAssignmentsByCharacter", conn);
                    sqlComm.CommandTimeout = 60;
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
                            AssignedMembers = dataRow.Field<string>("AssignedMembers"),
                            AssignedPlatoons = dataRow.Field<string>("Platoons")
                        }).ToList();
                    foreach (var assign in model)
                    {
                        if (assign.AssignedMembers != null && assign.AssignedMembers != "")
                        {
                            assign.AssignedMembers = assign.AssignedMembers.Replace(",", "<br/>");
                        }
                        if (assign.AssignedPlatoons != null && assign.AssignedPlatoons != "")
                        {
                            assign.AssignedPlatoons = assign.AssignedPlatoons.Replace(",", "<br/>");
                        }
                    }
                }
                HttpContext.Cache.Insert("PlatoonAssignmentsByCharacter" + id.ToString(), model, null, Cache.NoAbsoluteExpiration, new TimeSpan(24, 0, 0));
            }

            ViewBag.PhaseNumber = phase.Phase;
            ViewBag.Id = phase.Id;

            return View(model);
        }

        public ActionResult PlatoonAssignmentsByMember(Guid id)
        {
            TerritoryBattlePhase phase = db.TerritoryBattlePhases.Find(id);

            DataSet ds = new DataSet("MemberAssignments");
            List<PlatoonAssignmentsByMember> model = (List<PlatoonAssignmentsByMember>)HttpContext.Cache.Get("PlatoonAssignmentsByMember" + id.ToString());
            if (model == null)
            {
                List<PlatoonAssignmentsByCharacter> paByChar = (List<PlatoonAssignmentsByCharacter>)HttpContext.Cache.Get("PlatoonAssignmentsByCharacter" + id.ToString());
                model = new List<ViewModels.PlatoonAssignmentsByMember>();

                foreach (var charAssignment in paByChar)
                {
                    if (charAssignment.HaveCount != 0)
                    {
                        string[] members = charAssignment.AssignedMembers.Split(new string[] { "<br/>" }, StringSplitOptions.None);
                        string[] platoons = charAssignment.AssignedPlatoons.Split(new string[] { "<br/>" }, StringSplitOptions.None);

                        for (int i = 0; i < members.Count(); i++)
                        {
                            if (model.Where(x => x.MemberName.Contains(members[i])).Count() == 0 && members[i] != "")
                            {
                                PlatoonAssignmentsByMember memAssignment = new ViewModels.PlatoonAssignmentsByMember()
                                {
                                    MemberName = members[i],
                                    AssignedCharacters = charAssignment.CharacterName + " - " + platoons[i]
                                };
                                model.Add(memAssignment);
                            }
                            else if (members[i] != "")
                            {
                                var obj = model.FirstOrDefault(x => x.MemberName == members[i]);
                                if (obj != null) obj.AssignedCharacters = obj.AssignedCharacters + "<br/>" + charAssignment.CharacterName + " - " + platoons[i];
                            }
                        }
                    }
                }

                HttpContext.Cache.Insert("PlatoonAssignmentsByMember" + id.ToString(), model, null, Cache.NoAbsoluteExpiration, new TimeSpan(24, 0, 0));
            }

            return PartialView(model);
        }

        public ActionResult ClearReports(Guid id)
        {
            HttpContext.Cache.Remove("PlatoonAssignmentsByCharacter" + id.ToString());
            HttpContext.Cache.Remove("PlatoonAssignmentsByMember" + id.ToString());

            return RedirectToAction("PlatoonAssignmentsByCharacter", "Reports", new { id = id });
        }
    }
}