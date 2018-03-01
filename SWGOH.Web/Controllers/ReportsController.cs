using AutoMapper;
using SWGOH.Entities;
using SWGOH.Web.DataContexts;
using SWGOH.Web.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Caching;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using EntityFramework.BulkExtensions;

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

        public ActionResult NewPlatoonAssignments(Guid id, Guid? memberId, Guid? guildId = null)
        {
            TerritoryBattlePhase tbp = db.TerritoryBattlePhases.Find(id);
            ViewBag.PhaseNumber = tbp.Phase;

            var members = db.Members.Where(x => x.Guild_Id == tbp.TerritoryBattle.Guild_Id).Select(s => new
            {
                Text = s.DisplayName,
                Value = s.Id
            }).OrderBy(x => x.Text).AsEnumerable();

            SelectList selectList = new SelectList(members, "Value", "Text");

            TerritoryPhaseReportModel model = new TerritoryPhaseReportModel()
            {
                Members = selectList,
                MemberId = memberId,
                Id = id,
            };

            ViewBag.GuildId = guildId;

            if (tbp.RefreshReport)
            {
                foreach (DictionaryEntry key in HttpContext.Cache)
                {
                    HttpContext.Cache.Remove(key.Key.ToString());
                }

                db.BulkDelete(db.PhaseReports.Where(x => x.TerritoryBattlePhase_Id == id).ToList());

                db = new SwgohDb();
                var tbpUpdate = db.TerritoryBattlePhases.Find(id);
                tbpUpdate.RefreshReport = false;

                db.Entry(tbpUpdate).State = EntityState.Modified;
                db.SaveChanges();
                db = new SwgohDb();
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult NewPlatoonAssignmentsData(DataSourceRequest command, Guid id, Guid? memberId, Guid? guildId = null)
        {
            DataSourceResult gridModel;

            var tbp = db.TerritoryBattlePhases.Find(id);
            if (guildId == null)
            {
                guildId = tbp.TerritoryBattle.Guild_Id;
            }
            var ds = new DataTable();
            var newReport = (IEnumerable<PhaseReport>)HttpContext.Cache.Get("PlatoonAssignments" + id + guildId);

            if (newReport == null)
            {
                newReport = db.PhaseReports.Where(x => x.TerritoryBattlePhase_Id == id && x.GuildId == guildId && x.MemberShip_Id == null);
                if (newReport.Count() == 0)
                {
                    using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SwgohDb"].ConnectionString))
                    {
                        var sqlComm = new SqlCommand("sp_RequiredPlatoonCharacters", conn);
                        sqlComm.Parameters.AddWithValue("@phaseGuid", id);

                        sqlComm.CommandType = System.Data.CommandType.StoredProcedure;

                        var da = new SqlDataAdapter {SelectCommand = sqlComm};

                        da.Fill(ds);

                        ds.Columns.Add("Member_Id");
                        Guid? currentCharId = Guid.Empty;
                        var memberCharacters = new List<MemberCharacter>();
                        var assignedChars = 0;

                        foreach (DataRow row in ds.Rows)
                        {
                            if (currentCharId != row.Field<Guid>("Character_Id"))
                            {
                                currentCharId = row.Field<Guid>("Character_Id");
                                memberCharacters = db.MemberCharacters.Where(x => x.Character_Id == currentCharId
                                                                               && x.Member.Guild_Id == guildId
                                                                               && x.Stars >= tbp.RequiredStars)
                                                                               .OrderBy(x => x.Power)
                                                                               .ThenBy(x => x.Stars)
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
                                MemberCharacter = db.MemberCharacters.Find(dataRow.Field<Guid?>("Character_Id")),
                                GuildId = guildId
                            }).ToList();

                        db.BulkInsert(newReport);
                    }
                }
                HttpContext.Cache.Insert("PlatoonAssignments" + id + guildId, newReport, null, Cache.NoAbsoluteExpiration, new TimeSpan(24, 0, 0));
            }
            if (memberId.HasValue)
            {
                gridModel = newReport.Where(x => x.MemberCharacter.Member_Id == memberId).ToList().ToDataSourceResult(command, Mapper.Map<PhaseReport, PlatoonAssignmentsModel>);
            }
            else
            {
                gridModel = (DataSourceResult)HttpContext.Cache.Get("PlatoonAssignmentsGrid" + id + guildId);
                if (gridModel == null)
                {
                    gridModel = newReport.ToDataSourceResult(command, Mapper.Map<PhaseReport, PlatoonAssignmentsModel>);
                    HttpContext.Cache.Insert("PlatoonAssignmentsGrid" + id + guildId, gridModel, null, Cache.NoAbsoluteExpiration, new TimeSpan(24, 0, 0));
                }
            }
            return Json(gridModel);
        }

        [HttpPost]
        public ActionResult ShipPlatoonAssignmentsData(DataSourceRequest command, Guid id, Guid? memberId)
        {
            var gridModel = new DataSourceResult();

            TerritoryBattlePhase tbp = db.TerritoryBattlePhases.Find(id);
            DataTable ds = new DataTable();
            IEnumerable<PhaseReport> newReport = (IEnumerable<PhaseReport>)HttpContext.Cache.Get("ShipPlatoonAssignments" + id.ToString());

            if (newReport == null)
            {
                newReport = db.PhaseReports.Where(x => x.TerritoryBattlePhase_Id == id && x.MemberCharacter_Id == null);
                if (newReport.Count() == 0)
                {
                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SwgohDb"].ConnectionString))
                    {
                        SqlCommand sqlComm = new SqlCommand("sp_RequiredPlatoonShips", conn);
                        sqlComm.Parameters.AddWithValue("@phaseGuid", id);

                        sqlComm.CommandType = System.Data.CommandType.StoredProcedure;

                        SqlDataAdapter da = new SqlDataAdapter();
                        da.SelectCommand = sqlComm;

                        da.Fill(ds);

                        Guid? currentShipId = Guid.Empty;
                        List<MemberShip> memberShips = new List<MemberShip>();
                        int assignedShips = 0;

                        foreach (DataRow row in ds.Rows)
                        {
                            if (currentShipId != row.Field<Guid>("Ship_Id"))
                            {
                                currentShipId = row.Field<Guid>("Ship_Id");
                                memberShips = db.MemberShips.Where(x => x.Ship_Id == currentShipId
                                                                               && x.Member.Guild_Id == tbp.TerritoryBattle.Guild_Id
                                                                               && x.Stars >= tbp.RequiredStars)
                                                                               .OrderBy(x => x.Stars)
                                                                               .ThenBy(x => x.Level).ToList();
                                assignedShips = 0;
                            }

                            try
                            {
                                row[1] = memberShips[assignedShips].Id;
                            }
                            catch
                            {
                                row[1] = DBNull.Value;
                            }
                            assignedShips++;
                        }
                        newReport = ds.AsEnumerable().Select(
                            dataRow => new PhaseReport
                            {
                                Id = Guid.NewGuid(),
                                PlatoonShip_Id = dataRow.Field<Guid?>("PlatoonShip_Id"),
                                PlatoonShip = db.PlatoonShips.Find(dataRow.Field<Guid?>("PlatoonShip_Id")),
                                TerritoryBattlePhase_Id = dataRow.Field<Guid>("TerritoryBattlePhase_Id"),
                                TerritoryBattlePhase = db.TerritoryBattlePhases.Find(dataRow.Field<Guid>("TerritoryBattlePhase_Id")),
                                MemberShip_Id = dataRow.Field<Guid?>("Ship_Id"),
                                MemberShip = db.MemberShips.Find(dataRow.Field<Guid?>("Ship_Id"))
                            }).ToList();

                        db.BulkInsert(newReport);
                    }
                }
                HttpContext.Cache.Insert("ShipPlatoonAssignments" + id.ToString(), newReport, null, Cache.NoAbsoluteExpiration, new TimeSpan(24, 0, 0));
            }
            if (memberId.HasValue)
            {
                gridModel = newReport.Where(x => x.MemberShip.Member_Id == memberId).ToList().ToDataSourceResult(command, newGrid => Mapper.Map<PhaseReport, PlatoonAssignmentsModel>(newGrid));
            }
            else
            {
                gridModel = (DataSourceResult)HttpContext.Cache.Get("ShipPlatoonAssignmentsGrid" + id.ToString());
                if (gridModel == null)
                {
                    gridModel = newReport.ToDataSourceResult(command, newGrid => Mapper.Map<PhaseReport, PlatoonAssignmentsModel>(newGrid));
                    HttpContext.Cache.Insert("ShipPlatoonAssignmentsGrid" + id.ToString(), gridModel, null, Cache.NoAbsoluteExpiration, new TimeSpan(24, 0, 0));
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
            foreach (DictionaryEntry key in HttpContext.Cache)
            {
                HttpContext.Cache.Remove(key.Key.ToString());
            }

            return RedirectToAction("PlatoonAssignmentsByCharacter", "Reports", new { id = id });
        }
    }
}