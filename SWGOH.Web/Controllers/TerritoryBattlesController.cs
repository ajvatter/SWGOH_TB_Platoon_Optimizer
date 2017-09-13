using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SWGOH.Entities;
using SWGOH.Web.DataContexts;
using SWGOH.Web.Models;
using SWGOH.Web.ViewModels;
using AutoMapper;

namespace SWGOH.Web.Controllers
{
    public class TerritoryBattlesController : Controller
    {
        private SwgohDb db = new SwgohDb();
        ApplicationDbContext userDb = new ApplicationDbContext();

        // GET: TerritoryBattles
        public ActionResult Index(Guid id)
        {
            List<TerritoryBattle> territoryBattles = db.TerritoryBattles.Where(x => x.Guild_Id == id).ToList();
            List<TerritoryBattleModel> model = Mapper.Map<List<TerritoryBattle>, List<TerritoryBattleModel>>(territoryBattles);
            return View(model);
        }

        // GET: TerritoryBattles/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TerritoryBattle territoryBattle = db.TerritoryBattles.Include(x => x.TerritoryBattlePhases).SingleOrDefault(y => y.Id == id);
            if (territoryBattle == null)
            {
                return HttpNotFound();
            }

            TerritoryBattleModel model = Mapper.Map<TerritoryBattle, TerritoryBattleModel>(territoryBattle);
            return View(model);
        }

        // GET: TerritoryBattles/Create
        [Authorize(Roles = "Officers")]
        public ActionResult Create()
        {
            TerritoryBattle territoryBattle = new TerritoryBattle();

            territoryBattle.Id = Guid.NewGuid();
            territoryBattle.Guild_Id = userDb.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault().Guild_Id;
            territoryBattle.StartDate = DateTime.Now;
            territoryBattle.IsActive = true;

            return View(territoryBattle);
        }

        // POST: TerritoryBattles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Officers")]
        public ActionResult Create(TerritoryBattle territoryBattle)
        {
            territoryBattle.Id = Guid.NewGuid();

            List<TerritoryBattlePhase> territoryBattlePhases = new List<TerritoryBattlePhase>()
            {
                new TerritoryBattlePhase()
                {
                    Id = Guid.NewGuid(),
                    HasSecondTerritory = false,
                    RequiredStars = 2,
                    Phase = 1,
                    TerritoryBattle_Id = territoryBattle.Id,
                },
                new TerritoryBattlePhase()
                {
                    Id = Guid.NewGuid(),
                    HasSecondTerritory = true,
                    RequiredStars = 3,
                    Phase = 2,
                    TerritoryBattle_Id = territoryBattle.Id,
                },
                new TerritoryBattlePhase()
                {
                    Id = Guid.NewGuid(),
                    HasSecondTerritory = true,
                    RequiredStars = 3,
                    Phase = 3,
                    TerritoryBattle_Id = territoryBattle.Id,
                },
                new TerritoryBattlePhase()
                {
                    Id = Guid.NewGuid(),
                    HasSecondTerritory = true,
                    RequiredStars = 5,
                    Phase = 4,
                    TerritoryBattle_Id = territoryBattle.Id,
                },
                new TerritoryBattlePhase()
                {
                    Id = Guid.NewGuid(),
                    HasSecondTerritory = true,
                    RequiredStars = 6,
                    Phase = 5,
                    TerritoryBattle_Id = territoryBattle.Id,
                },
                new TerritoryBattlePhase()
                {
                    Id = Guid.NewGuid(),
                    HasSecondTerritory = true,
                    RequiredStars = 7,
                    Phase = 6,
                    TerritoryBattle_Id = territoryBattle.Id,
                }
            };
            List<PhaseTerritory> phaseTerritories = new List<PhaseTerritory>()
            {
                new PhaseTerritory()
                {
                    Id = Guid.NewGuid(),
                    PhaseLocation = "Middle",
                    TerritoryBattlePhase_Id = territoryBattlePhases.Where(x => x.Phase == 1).FirstOrDefault().Id,
                    TerritoryBattlePhase = territoryBattlePhases.Where(x => x.Phase == 1).FirstOrDefault()
                },
                new PhaseTerritory()
                {
                    Id = Guid.NewGuid(),
                    PhaseLocation = "Middle",
                    TerritoryBattlePhase_Id = territoryBattlePhases.Where(x => x.Phase == 2).FirstOrDefault().Id,
                    TerritoryBattlePhase = territoryBattlePhases.Where(x => x.Phase == 2).FirstOrDefault()
                },
                new PhaseTerritory()
                {
                    Id = Guid.NewGuid(),
                    PhaseLocation = "Bottom",
                    TerritoryBattlePhase_Id = territoryBattlePhases.Where(x => x.Phase == 2).FirstOrDefault().Id,
                    TerritoryBattlePhase = territoryBattlePhases.Where(x => x.Phase == 2).FirstOrDefault()
                },
                new PhaseTerritory()
                {
                    Id = Guid.NewGuid(),
                    PhaseLocation = "Middle",
                    TerritoryBattlePhase_Id = territoryBattlePhases.Where(x => x.Phase == 3).FirstOrDefault().Id,
                    TerritoryBattlePhase = territoryBattlePhases.Where(x => x.Phase == 3).FirstOrDefault()
                },
                new PhaseTerritory()
                {
                    Id = Guid.NewGuid(),
                    PhaseLocation = "Bottom",
                    TerritoryBattlePhase_Id = territoryBattlePhases.Where(x => x.Phase == 3).FirstOrDefault().Id,
                    TerritoryBattlePhase = territoryBattlePhases.Where(x => x.Phase == 3).FirstOrDefault()
                },
                new PhaseTerritory()
                {
                    Id = Guid.NewGuid(),
                    PhaseLocation = "Middle",
                    TerritoryBattlePhase_Id = territoryBattlePhases.Where(x => x.Phase == 4).FirstOrDefault().Id,
                    TerritoryBattlePhase = territoryBattlePhases.Where(x => x.Phase == 4).FirstOrDefault()
                },
                new PhaseTerritory()
                {
                    Id = Guid.NewGuid(),
                    PhaseLocation = "Bottom",
                    TerritoryBattlePhase_Id = territoryBattlePhases.Where(x => x.Phase == 4).FirstOrDefault().Id,
                    TerritoryBattlePhase = territoryBattlePhases.Where(x => x.Phase == 4).FirstOrDefault()
                },
                new PhaseTerritory()
                {
                    Id = Guid.NewGuid(),
                    PhaseLocation = "Middle",
                    TerritoryBattlePhase_Id = territoryBattlePhases.Where(x => x.Phase == 5).FirstOrDefault().Id,
                    TerritoryBattlePhase = territoryBattlePhases.Where(x => x.Phase == 5).FirstOrDefault()
                },
                new PhaseTerritory()
                {
                    Id = Guid.NewGuid(),
                    PhaseLocation = "Bottom",
                    TerritoryBattlePhase_Id = territoryBattlePhases.Where(x => x.Phase == 5).FirstOrDefault().Id,
                    TerritoryBattlePhase = territoryBattlePhases.Where(x => x.Phase == 5).FirstOrDefault()
                },
                new PhaseTerritory()
                {
                    Id = Guid.NewGuid(),
                    PhaseLocation = "Middle",
                    TerritoryBattlePhase_Id = territoryBattlePhases.Where(x => x.Phase == 6).FirstOrDefault().Id,
                    TerritoryBattlePhase = territoryBattlePhases.Where(x => x.Phase == 6).FirstOrDefault()
                },
                new PhaseTerritory()
                {
                    Id = Guid.NewGuid(),
                    PhaseLocation = "Bottom",
                    TerritoryBattlePhase_Id = territoryBattlePhases.Where(x => x.Phase == 6).FirstOrDefault().Id,
                    TerritoryBattlePhase = territoryBattlePhases.Where(x => x.Phase == 6).FirstOrDefault()
                },
            };
            List<TerritoryPlatoon> territoryPlatoons = new List<TerritoryPlatoon>()
            {
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 1,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 1).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 2,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 1).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 3,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 1).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 4,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 1).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 5,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 1).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 6,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 1).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 1,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 2).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 2,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 2).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 3,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 2).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 4,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 2).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 5,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 2).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 6,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 2).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 1,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 3).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 2,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 3).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 3,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 3).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 4,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 3).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 5,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 3).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 6,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 3).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 1,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 3).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 2,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 3).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 3,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 3).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 4,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 3).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 5,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 3).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 6,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 3).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 1,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 4).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 2,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 4).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 3,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 4).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 4,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 4).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 5,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 4).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 6,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 4).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 1,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 4).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 2,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 4).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 3,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 4).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 4,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 4).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 5,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 4).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 6,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 4).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 1,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 4).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 2,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 4).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 3,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 4).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 4,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 4).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 5,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 4).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 6,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 4).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 1,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 5).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 2,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 5).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 3,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 5).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 4,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 5).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 5,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 5).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 6,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 5).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 1,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 5).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 2,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 5).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 3,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 5).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 4,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 5).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 5,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 5).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 6,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 5).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 1,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 6).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 2,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 6).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 3,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 6).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 4,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 6).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 5,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 6).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 6,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == 6).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 1,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 6).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 2,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 6).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 3,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 6).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 4,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 6).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 5,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 6).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 6,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == 6).FirstOrDefault()
                },
            };
            List<PlatoonCharacter> platoonCharacters = new List<PlatoonCharacter>();

            foreach (var platoon in territoryPlatoons)
            {
                List<PlatoonCharacter> platoonCharactersAdd = new List<PlatoonCharacter>()
                {
                    new PlatoonCharacter(){ Id = Guid.NewGuid(), PlatoonPosition = 1, TerritoryPlatoon_Id = platoon.Id, TerritoryPlatoon = platoon },
                    new PlatoonCharacter(){ Id = Guid.NewGuid(), PlatoonPosition = 2, TerritoryPlatoon_Id = platoon.Id, TerritoryPlatoon = platoon },
                    new PlatoonCharacter(){ Id = Guid.NewGuid(), PlatoonPosition = 3, TerritoryPlatoon_Id = platoon.Id, TerritoryPlatoon = platoon },
                    new PlatoonCharacter(){ Id = Guid.NewGuid(), PlatoonPosition = 4, TerritoryPlatoon_Id = platoon.Id, TerritoryPlatoon = platoon },
                    new PlatoonCharacter(){ Id = Guid.NewGuid(), PlatoonPosition = 5, TerritoryPlatoon_Id = platoon.Id, TerritoryPlatoon = platoon },
                    new PlatoonCharacter(){ Id = Guid.NewGuid(), PlatoonPosition = 6, TerritoryPlatoon_Id = platoon.Id, TerritoryPlatoon = platoon },
                    new PlatoonCharacter(){ Id = Guid.NewGuid(), PlatoonPosition = 7, TerritoryPlatoon_Id = platoon.Id, TerritoryPlatoon = platoon },
                    new PlatoonCharacter(){ Id = Guid.NewGuid(), PlatoonPosition = 8, TerritoryPlatoon_Id = platoon.Id, TerritoryPlatoon = platoon },
                    new PlatoonCharacter(){ Id = Guid.NewGuid(), PlatoonPosition = 9, TerritoryPlatoon_Id = platoon.Id, TerritoryPlatoon = platoon },
                    new PlatoonCharacter(){ Id = Guid.NewGuid(), PlatoonPosition = 10, TerritoryPlatoon_Id = platoon.Id, TerritoryPlatoon = platoon },
                    new PlatoonCharacter(){ Id = Guid.NewGuid(), PlatoonPosition = 11, TerritoryPlatoon_Id = platoon.Id, TerritoryPlatoon = platoon },
                    new PlatoonCharacter(){ Id = Guid.NewGuid(), PlatoonPosition = 12, TerritoryPlatoon_Id = platoon.Id, TerritoryPlatoon = platoon },
                    new PlatoonCharacter(){ Id = Guid.NewGuid(), PlatoonPosition = 13, TerritoryPlatoon_Id = platoon.Id, TerritoryPlatoon = platoon },
                    new PlatoonCharacter(){ Id = Guid.NewGuid(), PlatoonPosition = 14, TerritoryPlatoon_Id = platoon.Id, TerritoryPlatoon = platoon },
                    new PlatoonCharacter(){ Id = Guid.NewGuid(), PlatoonPosition = 15, TerritoryPlatoon_Id = platoon.Id, TerritoryPlatoon = platoon },
                };
                platoonCharacters.AddRange(platoonCharactersAdd);
            }
            //List<PlatoonCharacter> platoonCharacters = new List<PlatoonCharacter>()
            //{
            //    new PlatoonCharacter()
            //    {
            //        Id = Guid.NewGuid(),
            //        PlatoonPosition = 1,
            //        TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //    },
            //    new PlatoonCharacter()
            //    {
            //        Id = Guid.NewGuid(),
            //        PlatoonPosition = 2,
            //        TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //    },
            //    new PlatoonCharacter()
            //    {
            //        Id = Guid.NewGuid(),
            //        PlatoonPosition = 3,
            //        TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //    },
            //    new PlatoonCharacter()
            //    {
            //        Id = Guid.NewGuid(),
            //        PlatoonPosition = 4,
            //        TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //    },
            //    new PlatoonCharacter()
            //    {
            //        Id = Guid.NewGuid(),
            //        PlatoonPosition = 5,
            //        TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //    },
            //    new PlatoonCharacter()
            //    {
            //        Id = Guid.NewGuid(),
            //        PlatoonPosition = 6,
            //        TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //    },
            //    new PlatoonCharacter()
            //    {
            //        Id = Guid.NewGuid(),
            //        PlatoonPosition = 7,
            //        TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //    },
            //    new PlatoonCharacter()
            //    {
            //        Id = Guid.NewGuid(),
            //        PlatoonPosition = 8,
            //        TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //    },
            //    new PlatoonCharacter()
            //    {
            //        Id = Guid.NewGuid(),
            //        PlatoonPosition = 9,
            //        TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //    },
            //    new PlatoonCharacter()
            //    {
            //        Id = Guid.NewGuid(),
            //        PlatoonPosition = 10,
            //        TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //    },
            //    new PlatoonCharacter()
            //    {
            //        Id = Guid.NewGuid(),
            //        PlatoonPosition = 11,
            //        TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //    },
            //    new PlatoonCharacter()
            //    {
            //        Id = Guid.NewGuid(),
            //        PlatoonPosition = 12,
            //        TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //    },
            //    new PlatoonCharacter()
            //    {
            //        Id = Guid.NewGuid(),
            //        PlatoonPosition = 13,
            //        TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //    },
            //    new PlatoonCharacter()
            //    {
            //        Id = Guid.NewGuid(),
            //        PlatoonPosition = 14,
            //        TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //    },
            //    new PlatoonCharacter()
            //    {
            //        Id = Guid.NewGuid(),
            //        PlatoonPosition = 15,
            //        TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //    },
            //                 new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                 new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                 new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                 new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                 new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 1).FirstOrDefault().Id
            //                },

            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 2).FirstOrDefault().Id
            //                },
            //};
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 3).FirstOrDefault().Id
            //                },
            //                #endregion
            //                #region Phase 4 Platoons
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 4).FirstOrDefault().Id
            //                },
            //                #endregion
            //                #region Phase 5 Platoons
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 5).FirstOrDefault().Id
            //                },
            //                #endregion
            //                #region Phase 6 Platoons
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 1 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 2 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 3 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 4 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 5 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Middle" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 1,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 2,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 3,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 4,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 5,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 6,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 7,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 8,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 9,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 10,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 11,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 12,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 13,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 14,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //                new PlatoonCharacter()
            //                {
            //                    Id = Guid.NewGuid(),
            //                    PlatoonPosition = 15,
            //                    TerritoryPlatoon_Id = territoryPlatoons.Where(x => x.PlatoonNumber == 6 && x.PhaseTerritory.PhaseLocation == "Bottom" && x.PhaseTerritory.TerritoryBattlePhase.Phase == 6).FirstOrDefault().Id
            //                },
            //#endregion
            //            };

            if (ModelState.IsValid)
            {
                //territoryBattle.Id = Guid.NewGuid();
                db.TerritoryBattles.Add(territoryBattle);
                db.SaveChanges();
                db.BulkInsert(territoryBattlePhases);
                db.BulkInsert(phaseTerritories);
                db.BulkInsert(territoryPlatoons);
                db.BulkInsert(platoonCharacters);
                //db.TerritoryPlatoons.Add(territoryPlatoons.FirstOrDefault());
                //db.SaveChanges();
                return RedirectToAction("Details", new { id = territoryBattle.Id });
            }

            return View(territoryBattle);
        }

        // GET: TerritoryBattles/Edit/5
        [Authorize(Roles = "Officers")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TerritoryBattle territoryBattle = db.TerritoryBattles.Find(id);
            if (territoryBattle == null)
            {
                return HttpNotFound();
            }
            ViewBag.Guild_Id = new SelectList(db.Guilds, "Id", "Name", territoryBattle.Guild_Id);
            return View(territoryBattle);
        }

        // POST: TerritoryBattles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Officers")]
        public ActionResult Edit(TerritoryBattle territoryBattle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(territoryBattle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Guild_Id = new SelectList(db.Guilds, "Id", "Name", territoryBattle.Guild_Id);

            return View(territoryBattle);
        }

        // GET: TerritoryBattles/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TerritoryBattle territoryBattle = db.TerritoryBattles.Find(id);
            if (territoryBattle == null)
            {
                return HttpNotFound();
            }
            return View(territoryBattle);
        }

        // POST: TerritoryBattles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            TerritoryBattle territoryBattle = db.TerritoryBattles.Find(id);
            db.TerritoryBattles.Remove(territoryBattle);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
