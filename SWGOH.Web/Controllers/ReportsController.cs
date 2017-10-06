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

                    sqlComm.CommandType = System.Data.CommandType.StoredProcedure;

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


        public ActionResult NewPlatoonAssignments(Guid id, Guid? memberId)
        {
            //TerritoryBattlePhase tbp = db.TerritoryBattlePhases.Find(id);
            //var model = Mapper.Map<TerritoryBattlePhase, TerritoryBattlePhaseModel>(tbp);

            var members = db.Members.Select(s => new
            {
                Text = s.DisplayName,
                Value = s.Id
            }).OrderBy(x => x.Text).AsEnumerable();

            SelectList selectList = new SelectList(members, "Value", "Text");

            TerritoryPhaseReportModel model = new TerritoryPhaseReportModel()
            {
                Members = selectList,
                MemberId = memberId,
                Id = id
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult NewPlatoonAssignmentsData(DataSourceRequest command, Guid id, Guid? memberId)
        {
            var gridModel = new DataSourceResult();

            TerritoryBattlePhase tbp = db.TerritoryBattlePhases.Find(id);
            DataTable ds = new DataTable();
            IEnumerable<PhaseReport> newReport = (IEnumerable<PhaseReport>)HttpContext.Cache.Get("PlatoonAssignments" + id.ToString());
            // db.PhaseReports.Where(x => x.TerritoryBattlePhase_Id == id);
            //gridModel = (DataSourceResult)HttpContext.Cache.Get("PlatoonAssignments" + id.ToString());
            if (newReport == null)
            {
                newReport = db.PhaseReports.Where(x => x.TerritoryBattlePhase_Id == id);
                if (newReport.Count() == 0)
                {
                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SwgohDb"].ConnectionString))
                    {
                        SqlCommand sqlComm = new SqlCommand("sp_RequiredPlatoonCharacters", conn);
                        sqlComm.Parameters.AddWithValue("@phaseGuid", id);

                        sqlComm.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = sqlComm;

                        da.Fill(ds);

                        Guid? currentCharId = Guid.Empty;
                        List<MemberCharacter> memberCharacters = new List<MemberCharacter>();
                        int assignedChars = 0;

                        foreach (DataRow row in ds.Rows)
                        {
                            if (currentCharId != row.Field<Guid>("Character_Id"))
                            {
                                currentCharId = row.Field<Guid>("Character_Id");
                                memberCharacters = db.MemberCharacters.Where(x => x.Character_Id == currentCharId
                                                                               && x.Member.Guild_Id == tbp.TerritoryBattle.Guild_Id
                                                                               && x.Stars >= tbp.RequiredStars)
                                                                               .OrderBy(x => x.Stars)
                                                                               .ThenBy(x => x.Level)
                                                                               .ThenBy(x => x.Gear).ToList();
                                assignedChars = 0;
                            }

                            try
                            {
                                row[1] = memberCharacters[assignedChars].Id;
                            }
                            catch
                            {
                                row[1] = DBNull.Value;
                            }
                            assignedChars++;
                        }
                        newReport = ds.AsEnumerable().Select(
                            dataRow => new PhaseReport
                            {
                                Id = Guid.NewGuid(),
                                PlatoonCharacter_Id = dataRow.Field<Guid?>("PlatoonCharacter_Id"),
                                PlatoonCharacter = db.PlatoonCharacters.Find(dataRow.Field<Guid?>("PlatoonCharacter_Id")),
                                TerritoryBattlePhase_Id = dataRow.Field<Guid>("TerritoryBattlePhase_Id"),
                                TerritoryBattlePhase = db.TerritoryBattlePhases.Find(dataRow.Field<Guid>("TerritoryBattlePhase_Id")),
                                MemberCharacter_Id = dataRow.Field<Guid?>("Character_Id"),
                                MemberCharacter = db.MemberCharacters.Find(dataRow.Field<Guid?>("Character_Id"))
                            }).ToList();

                        db.BulkInsert(newReport);
                    }
                }            
                    HttpContext.Cache.Insert("PlatoonAssignments" + id.ToString(), newReport, null, Cache.NoAbsoluteExpiration, new TimeSpan(24, 0, 0));
            }
            if (memberId.HasValue)
            {
                gridModel = newReport.Where(x => x.MemberCharacter.Member_Id == memberId).ToList().ToDataSourceResult(command, newGrid => Mapper.Map<PhaseReport, PlatoonAssignmentsModel>(newGrid));
            }
            else
            {
                gridModel = (DataSourceResult)HttpContext.Cache.Get("PlatoonAssignmentsGrid" + id.ToString());
                if (gridModel == null)
                {
                    gridModel = newReport.ToDataSourceResult(command, newGrid => Mapper.Map<PhaseReport, PlatoonAssignmentsModel>(newGrid));
                    HttpContext.Cache.Insert("PlatoonAssignmentsGrid" + id.ToString(), gridModel, null, Cache.NoAbsoluteExpiration, new TimeSpan(24, 0, 0));
                }
            }
            return Json(gridModel);
        }

        [HttpPost]
        public virtual ActionResult PlatoonCharacters(DataSourceRequest command, TerritoryBattlePhaseModel model)
        {
            var gridModel = new DataSourceResult();

            DataSet ds = new DataSet("Counts");

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SwgohDb"].ConnectionString))
            {
                SqlCommand sqlComm = new SqlCommand("sp_RequiredPlatoonCharacters", conn);
                sqlComm.Parameters.AddWithValue("@phaseGuid", model.Id);

                sqlComm.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);

                gridModel.Data = ds.Tables[0].AsEnumerable().Select(
                    dataRow => new CharCount
                    {
                        Id = dataRow.Field<Guid>("Character_Id"),
                        Name = dataRow.Field<string>("Name"),
                        Alignment = dataRow.Field<string>("Alignment"),
                        OneStarCount = dataRow.Field<int>("OneStarCount"),
                        TwoStarCount = dataRow.Field<int>("TwoStarCount"),
                        ThreeStarCount = dataRow.Field<int>("ThreeStarCount"),
                        FourStarCount = dataRow.Field<int>("FourStarCount"),
                        FiveStarCount = dataRow.Field<int>("FiveStarCount"),
                        SixStarCount = dataRow.Field<int>("SixStarCount"),
                        SevenStarCount = dataRow.Field<int>("SevenStarCount"),
                    }).AsEnumerable().OrderBy(x => x.Name);
                gridModel.Total = ds.Tables[0].AsEnumerable().Count();
            }
            return Json(gridModel);
        }

        public ActionResult ClearReports(Guid id)
        {
            HttpContext.Cache.Remove("PlatoonAssignmentsByCharacter" + id.ToString());
            HttpContext.Cache.Remove("PlatoonAssignmentsByMember" + id.ToString());

            return RedirectToAction("PlatoonAssignmentsByCharacter", "Reports", new { id = id });
        }
    }
}