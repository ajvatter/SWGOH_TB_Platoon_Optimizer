using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SWGOH.Entities;
using SWGOH.Web.DataContexts;
using SWGOH.Web.Models;
using SWGOH.Web.ViewModels;
using AutoMapper;
using EntityFramework.BulkExtensions;

namespace SWGOH.Web.Controllers
{
    public class TerritoryBattlesController : Controller
    {
        private SwgohDb db = new SwgohDb();
        ApplicationDbContext userDb = new ApplicationDbContext();

        // GET: TerritoryBattles
        [Authorize]
        public ActionResult Index(Guid? id)
        {
            if (id == null)
            {
                if (!string.IsNullOrEmpty(User.Identity.Name))
                {
                    id = userDb.Users.FirstOrDefault(x => x.UserName == User.Identity.Name)?.Guild_Id;
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }


            var territoryBattles = db.TerritoryBattles.Where(x => x.Guild_Id == id).OrderByDescending(x => x.StartDate).ToList();
            var model = Mapper.Map<List<TerritoryBattle>, List<TerritoryBattleIndexModel>>(territoryBattles);
            return View(model);
        }

        public ActionResult ManageResults(Guid id)
        {
            var tb = db.TerritoryBattles.Find(id);

            var model = Mapper.Map<TerritoryBattle, TerritoryBattleResultsModel>(tb);
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult ManageResults(TerritoryBattleResultsModel territoryBattleResultsModel)
        {
            if (ModelState.IsValid)
            {
                List<PhaseTerritory> phaseTerritories = db.PhaseTerritories
                    .Where(x => x.TerritoryBattlePhase.TerritoryBattle_Id == territoryBattleResultsModel.Id).ToList();

                phaseTerritories.FirstOrDefault(x => x.TerritoryBattlePhase.Phase == 1).TotalPointsEarned =
                    territoryBattleResultsModel.Phase1Points;

                phaseTerritories.FirstOrDefault(x => x.TerritoryBattlePhase.Phase == 2 && x.PhaseLocation == "Middle").TotalPointsEarned =
                    territoryBattleResultsModel.Phase2TopCharPoints;
                phaseTerritories.FirstOrDefault(x => x.TerritoryBattlePhase.Phase == 2 && x.PhaseLocation == "Bottom").TotalPointsEarned =
                    territoryBattleResultsModel.Phase2BottomCharPoints;

                phaseTerritories.FirstOrDefault(x => x.TerritoryBattlePhase.Phase == 3 && x.PhaseLocation == "Top").TotalPointsEarned =
                    territoryBattleResultsModel.Phase3FleetPoints;
                phaseTerritories.FirstOrDefault(x => x.TerritoryBattlePhase.Phase == 3 && x.PhaseLocation == "Middle").TotalPointsEarned =
                    territoryBattleResultsModel.Phase3TopCharPoints;
                phaseTerritories.FirstOrDefault(x => x.TerritoryBattlePhase.Phase == 3 && x.PhaseLocation == "Bottom").TotalPointsEarned =
                    territoryBattleResultsModel.Phase3BottomCharPoints;

                phaseTerritories.FirstOrDefault(x => x.TerritoryBattlePhase.Phase == 4 && x.PhaseLocation == "Top").TotalPointsEarned =
                    territoryBattleResultsModel.Phase4FleetPoints;
                phaseTerritories.FirstOrDefault(x => x.TerritoryBattlePhase.Phase == 4 && x.PhaseLocation == "Middle").TotalPointsEarned =
                    territoryBattleResultsModel.Phase4TopCharPoints;
                phaseTerritories.FirstOrDefault(x => x.TerritoryBattlePhase.Phase == 4 && x.PhaseLocation == "Bottom").TotalPointsEarned =
                    territoryBattleResultsModel.Phase4BottomCharPoints;

                phaseTerritories.FirstOrDefault(x => x.TerritoryBattlePhase.Phase == 5 && x.PhaseLocation == "Top").TotalPointsEarned =
                    territoryBattleResultsModel.Phase5FleetPoints;
                phaseTerritories.FirstOrDefault(x => x.TerritoryBattlePhase.Phase == 5 && x.PhaseLocation == "Middle").TotalPointsEarned =
                    territoryBattleResultsModel.Phase5TopCharPoints;
                phaseTerritories.FirstOrDefault(x => x.TerritoryBattlePhase.Phase == 5 && x.PhaseLocation == "Bottom").TotalPointsEarned =
                    territoryBattleResultsModel.Phase5BottomCharPoints;

                phaseTerritories.FirstOrDefault(x => x.TerritoryBattlePhase.Phase == 6 && x.PhaseLocation == "Top").TotalPointsEarned =
                    territoryBattleResultsModel.Phase6FleetPoints;
                phaseTerritories.FirstOrDefault(x => x.TerritoryBattlePhase.Phase == 6 && x.PhaseLocation == "Middle").TotalPointsEarned =
                    territoryBattleResultsModel.Phase6TopCharPoints;
                phaseTerritories.FirstOrDefault(x => x.TerritoryBattlePhase.Phase == 6 && x.PhaseLocation == "Bottom").TotalPointsEarned =
                    territoryBattleResultsModel.Phase6BottomCharPoints;

                db = new SwgohDb();
                db.BulkUpdate(phaseTerritories);
                //db.Entry(territoryBattle).State = EntityState.Modified;
                //db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(territoryBattleResultsModel);
        }

        // GET: TerritoryBattles/Details/5
        [Authorize]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var territoryBattle = db.TerritoryBattles.Include(x => x.TerritoryBattlePhases).SingleOrDefault(y => y.Id == id);
            if (territoryBattle == null)
            {
                return HttpNotFound();
            }

            TerritoryBattleModel model = Mapper.Map<TerritoryBattle, TerritoryBattleModel>(territoryBattle);
            return View(model);
        }
       
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create()
        {
            var territoryBattle = new TerritoryBattle
            {
                Id = Guid.NewGuid(),
                Guild_Id = userDb.Users.FirstOrDefault(x => x.UserName == User.Identity.Name).Guild_Id,
                StartDate = DateTime.Now,
                IsActive = true
            };

            List<TerritoryBattlePhase> territoryBattlePhases = new List<TerritoryBattlePhase>()
            {
                new TerritoryBattlePhase()
                {
                    Id = Guid.NewGuid(),
                    HasSecondTerritory = true,
                    HasThirdTerritory = true,
                    RequiredStars = 5,
                    Phase = 4,
                    TerritoryBattle_Id = territoryBattle.Id,
                },
                new TerritoryBattlePhase()
                {
                    Id = Guid.NewGuid(),
                    HasSecondTerritory = true,
                    HasThirdTerritory = true,
                    RequiredStars = 6,
                    Phase = 5,
                    TerritoryBattle_Id = territoryBattle.Id,
                },
                new TerritoryBattlePhase()
                {
                    Id = Guid.NewGuid(),
                    HasSecondTerritory = true,
                    HasThirdTerritory = true,
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
                    TerritoryBattlePhase_Id = territoryBattlePhases.FirstOrDefault(x => x.Phase == 4).Id,
                    TerritoryBattlePhase = territoryBattlePhases.FirstOrDefault(x => x.Phase == 4)
                },
                new PhaseTerritory()
                {
                    Id = Guid.NewGuid(),
                    PhaseLocation = "Bottom",
                    TerritoryBattlePhase_Id = territoryBattlePhases.FirstOrDefault(x => x.Phase == 4).Id,
                    TerritoryBattlePhase = territoryBattlePhases.FirstOrDefault(x => x.Phase == 4)
                },
                new PhaseTerritory()
                {
                    Id = Guid.NewGuid(),
                    PhaseLocation = "Top",
                    TerritoryBattlePhase_Id = territoryBattlePhases.FirstOrDefault(x => x.Phase == 4).Id,
                    TerritoryBattlePhase = territoryBattlePhases.FirstOrDefault(x => x.Phase == 4)
                },
                new PhaseTerritory()
                {
                    Id = Guid.NewGuid(),
                    PhaseLocation = "Middle",
                    TerritoryBattlePhase_Id = territoryBattlePhases.FirstOrDefault(x => x.Phase == 5).Id,
                    TerritoryBattlePhase = territoryBattlePhases.FirstOrDefault(x => x.Phase == 5)
                },
                new PhaseTerritory()
                {
                    Id = Guid.NewGuid(),
                    PhaseLocation = "Bottom",
                    TerritoryBattlePhase_Id = territoryBattlePhases.FirstOrDefault(x => x.Phase == 5).Id,
                    TerritoryBattlePhase = territoryBattlePhases.FirstOrDefault(x => x.Phase == 5)
                },
                new PhaseTerritory()
                {
                    Id = Guid.NewGuid(),
                    PhaseLocation = "Top",
                    TerritoryBattlePhase_Id = territoryBattlePhases.FirstOrDefault(x => x.Phase == 5).Id,
                    TerritoryBattlePhase = territoryBattlePhases.FirstOrDefault(x => x.Phase == 5)
                },
                new PhaseTerritory()
                {
                    Id = Guid.NewGuid(),
                    PhaseLocation = "Middle",
                    TerritoryBattlePhase_Id = territoryBattlePhases.FirstOrDefault(x => x.Phase == 6).Id,
                    TerritoryBattlePhase = territoryBattlePhases.FirstOrDefault(x => x.Phase == 6)
                },
                new PhaseTerritory()
                {
                    Id = Guid.NewGuid(),
                    PhaseLocation = "Bottom",
                    TerritoryBattlePhase_Id = territoryBattlePhases.FirstOrDefault(x => x.Phase == 6).Id,
                    TerritoryBattlePhase = territoryBattlePhases.FirstOrDefault(x => x.Phase == 6)
                },
                new PhaseTerritory()
                {
                    Id = Guid.NewGuid(),
                    PhaseLocation = "Top",
                    TerritoryBattlePhase_Id = territoryBattlePhases.FirstOrDefault(x => x.Phase == 6).Id,
                    TerritoryBattlePhase = territoryBattlePhases.FirstOrDefault(x => x.Phase == 6)
                },
            };

            List<TerritoryPlatoon> territoryPlatoons = new List<TerritoryPlatoon>();

            foreach (var phaseTerritory in territoryBattlePhases)
            {
                List<TerritoryPlatoon> territoryPlatoonsAdd = new List<TerritoryPlatoon>()
                {
                    new TerritoryPlatoon()
                    {
                        Id = Guid.NewGuid(),
                        PlatoonNumber = 1,
                        PhaseTerritory_Id = phaseTerritories.FirstOrDefault(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == phaseTerritory.Phase).Id,
                        PhaseTerritory = phaseTerritories.FirstOrDefault(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == phaseTerritory.Phase)
                    },
                    new TerritoryPlatoon()
                    {
                        Id = Guid.NewGuid(),
                        PlatoonNumber = 2,
                        PhaseTerritory_Id = phaseTerritories.FirstOrDefault(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == phaseTerritory.Phase).Id,
                        PhaseTerritory = phaseTerritories.FirstOrDefault(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == phaseTerritory.Phase)
                    },
                    new TerritoryPlatoon()
                    {
                        Id = Guid.NewGuid(),
                        PlatoonNumber = 3,
                        PhaseTerritory_Id = phaseTerritories.FirstOrDefault(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == phaseTerritory.Phase).Id,
                        PhaseTerritory = phaseTerritories.FirstOrDefault(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == phaseTerritory.Phase)
                    },
                    new TerritoryPlatoon()
                    {
                        Id = Guid.NewGuid(),
                        PlatoonNumber = 4,
                        PhaseTerritory_Id = phaseTerritories.FirstOrDefault(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == phaseTerritory.Phase).Id,
                        PhaseTerritory = phaseTerritories.FirstOrDefault(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == phaseTerritory.Phase)
                    },
                    new TerritoryPlatoon()
                    {
                        Id = Guid.NewGuid(),
                        PlatoonNumber = 5,
                        PhaseTerritory_Id = phaseTerritories.FirstOrDefault(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == phaseTerritory.Phase).Id,
                        PhaseTerritory = phaseTerritories.FirstOrDefault(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == phaseTerritory.Phase)
                    },
                    new TerritoryPlatoon()
                    {
                        Id = Guid.NewGuid(),
                        PlatoonNumber = 6,
                        PhaseTerritory_Id = phaseTerritories.FirstOrDefault(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == phaseTerritory.Phase).Id,
                        PhaseTerritory = phaseTerritories.FirstOrDefault(x => x.PhaseLocation == "Middle" && x.TerritoryBattlePhase.Phase == phaseTerritory.Phase)
                    }
                };

                territoryPlatoons.AddRange(territoryPlatoonsAdd);

                if(phaseTerritory.HasSecondTerritory)
                {
                    territoryPlatoonsAdd = new List<TerritoryPlatoon>()
                    {
                        new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 1,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == phaseTerritory.Phase).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == phaseTerritory.Phase).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 2,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == phaseTerritory.Phase).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == phaseTerritory.Phase).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 3,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == phaseTerritory.Phase).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == phaseTerritory.Phase).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 4,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == phaseTerritory.Phase).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == phaseTerritory.Phase).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 5,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == phaseTerritory.Phase).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == phaseTerritory.Phase).FirstOrDefault()
                },
                new TerritoryPlatoon()
                {
                    Id = Guid.NewGuid(),
                    PlatoonNumber = 6,
                    PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == phaseTerritory.Phase).FirstOrDefault().Id,
                    PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Bottom" && x.TerritoryBattlePhase.Phase == phaseTerritory.Phase).FirstOrDefault()
                },
                    };

                    territoryPlatoons.AddRange(territoryPlatoonsAdd);
                }


                if (phaseTerritory.HasThirdTerritory)
                {
                    territoryPlatoonsAdd = new List<TerritoryPlatoon>()
                    {
                        new TerritoryPlatoon()
                        {
                            Id = Guid.NewGuid(),
                            PlatoonNumber = 1,
                            PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Top" && x.TerritoryBattlePhase.Phase == phaseTerritory.Phase).FirstOrDefault().Id,
                            PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Top" && x.TerritoryBattlePhase.Phase == phaseTerritory.Phase).FirstOrDefault()
                        },
                        new TerritoryPlatoon()
                        {
                            Id = Guid.NewGuid(),
                            PlatoonNumber = 2,
                            PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Top" && x.TerritoryBattlePhase.Phase == phaseTerritory.Phase).FirstOrDefault().Id,
                            PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Top" && x.TerritoryBattlePhase.Phase == phaseTerritory.Phase).FirstOrDefault()
                        },
                        new TerritoryPlatoon()
                        {
                            Id = Guid.NewGuid(),
                            PlatoonNumber = 3,
                            PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Top" && x.TerritoryBattlePhase.Phase == phaseTerritory.Phase).FirstOrDefault().Id,
                            PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Top" && x.TerritoryBattlePhase.Phase == phaseTerritory.Phase).FirstOrDefault()
                        },
                        new TerritoryPlatoon()
                        {
                            Id = Guid.NewGuid(),
                            PlatoonNumber = 4,
                            PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Top" && x.TerritoryBattlePhase.Phase == phaseTerritory.Phase).FirstOrDefault().Id,
                            PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Top" && x.TerritoryBattlePhase.Phase == phaseTerritory.Phase).FirstOrDefault()
                        },
                        new TerritoryPlatoon()
                        {
                            Id = Guid.NewGuid(),
                            PlatoonNumber = 5,
                            PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Top" && x.TerritoryBattlePhase.Phase == phaseTerritory.Phase).FirstOrDefault().Id,
                            PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Top" && x.TerritoryBattlePhase.Phase == phaseTerritory.Phase).FirstOrDefault()
                        },
                        new TerritoryPlatoon()
                        {
                            Id = Guid.NewGuid(),
                            PlatoonNumber = 6,                    
                            PhaseTerritory_Id = phaseTerritories.Where(x => x.PhaseLocation == "Top" && x.TerritoryBattlePhase.Phase == phaseTerritory.Phase).FirstOrDefault().Id,
                            PhaseTerritory = phaseTerritories.Where(x => x.PhaseLocation == "Top" && x.TerritoryBattlePhase.Phase == phaseTerritory.Phase).FirstOrDefault()
                        },
                    };

                    territoryPlatoons.AddRange(territoryPlatoonsAdd);
                }
            }

            List<PlatoonCharacter> platoonCharacters = new List<PlatoonCharacter>();
            List<PlatoonShip> platoonShips = new List<PlatoonShip>();

            foreach (var platoon in territoryPlatoons)
            {
                if (platoon.PhaseTerritory.PhaseLocation != "Top")
                {
                    var platoonCharactersAdd = new List<PlatoonCharacter>()
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
                else
                {
                    List<PlatoonShip> platoonShipsAdd = new List<PlatoonShip>()
                    {
                        new PlatoonShip(){ Id = Guid.NewGuid(), PlatoonPosition = 1, TerritoryPlatoon_Id = platoon.Id, TerritoryPlatoon = platoon },
                        new PlatoonShip(){ Id = Guid.NewGuid(), PlatoonPosition = 2, TerritoryPlatoon_Id = platoon.Id, TerritoryPlatoon = platoon },
                        new PlatoonShip(){ Id = Guid.NewGuid(), PlatoonPosition = 3, TerritoryPlatoon_Id = platoon.Id, TerritoryPlatoon = platoon },
                        new PlatoonShip(){ Id = Guid.NewGuid(), PlatoonPosition = 4, TerritoryPlatoon_Id = platoon.Id, TerritoryPlatoon = platoon },
                        new PlatoonShip(){ Id = Guid.NewGuid(), PlatoonPosition = 5, TerritoryPlatoon_Id = platoon.Id, TerritoryPlatoon = platoon },
                        new PlatoonShip(){ Id = Guid.NewGuid(), PlatoonPosition = 6, TerritoryPlatoon_Id = platoon.Id, TerritoryPlatoon = platoon },
                        new PlatoonShip(){ Id = Guid.NewGuid(), PlatoonPosition = 7, TerritoryPlatoon_Id = platoon.Id, TerritoryPlatoon = platoon },
                        new PlatoonShip(){ Id = Guid.NewGuid(), PlatoonPosition = 8, TerritoryPlatoon_Id = platoon.Id, TerritoryPlatoon = platoon },
                        new PlatoonShip(){ Id = Guid.NewGuid(), PlatoonPosition = 9, TerritoryPlatoon_Id = platoon.Id, TerritoryPlatoon = platoon },
                        new PlatoonShip(){ Id = Guid.NewGuid(), PlatoonPosition = 10, TerritoryPlatoon_Id = platoon.Id, TerritoryPlatoon = platoon },
                        new PlatoonShip(){ Id = Guid.NewGuid(), PlatoonPosition = 11, TerritoryPlatoon_Id = platoon.Id, TerritoryPlatoon = platoon },
                        new PlatoonShip(){ Id = Guid.NewGuid(), PlatoonPosition = 12, TerritoryPlatoon_Id = platoon.Id, TerritoryPlatoon = platoon },
                        new PlatoonShip(){ Id = Guid.NewGuid(), PlatoonPosition = 13, TerritoryPlatoon_Id = platoon.Id, TerritoryPlatoon = platoon },
                        new PlatoonShip(){ Id = Guid.NewGuid(), PlatoonPosition = 14, TerritoryPlatoon_Id = platoon.Id, TerritoryPlatoon = platoon },
                        new PlatoonShip(){ Id = Guid.NewGuid(), PlatoonPosition = 15, TerritoryPlatoon_Id = platoon.Id, TerritoryPlatoon = platoon },
                    };
                    platoonShips.AddRange(platoonShipsAdd);
                }
            }

            if (ModelState.IsValid)
            {
                db.TerritoryBattles.Add(territoryBattle);
                db.SaveChanges();
                db.BulkInsert(territoryBattlePhases);
                db.BulkInsert(phaseTerritories);
                db.BulkInsert(territoryPlatoons);
                db.BulkInsert(platoonCharacters);
                db.BulkInsert(platoonShips);
                return RedirectToAction("Index", "TerritoryBattles", new { });
            }

            return View(territoryBattle);
        }

        // GET: TerritoryBattles/Edit/5
        [Authorize]
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
        [Authorize]
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
        [Authorize(Roles = "Administrators")]
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
        [Authorize(Roles = "Administrators")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            TerritoryBattle territoryBattle = db.TerritoryBattles.Find(id);

            db.PlatoonShips.RemoveRange(db.PlatoonShips.Where(x => x.TerritoryPlatoon.PhaseTerritory.TerritoryBattlePhase.TerritoryBattle_Id == id).ToList());
            db.SaveChanges();

            db.PlatoonCharacters.RemoveRange(db.PlatoonCharacters.Where(x => x.TerritoryPlatoon.PhaseTerritory.TerritoryBattlePhase.TerritoryBattle_Id == id).ToList());
            db.SaveChanges();

            db.TerritoryPlatoons.RemoveRange(db.TerritoryPlatoons.Where(x => x.PhaseTerritory.TerritoryBattlePhase.TerritoryBattle_Id == id).ToList());
            db.SaveChanges();

            db.PhaseTerritories.RemoveRange(db.PhaseTerritories.Where(x => x.TerritoryBattlePhase.TerritoryBattle_Id == id).ToList());
            db.SaveChanges();

            db.TerritoryBattlePhases.RemoveRange(db.TerritoryBattlePhases.Where(x => x.TerritoryBattle_Id == id).ToList());
            db.SaveChanges();

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
