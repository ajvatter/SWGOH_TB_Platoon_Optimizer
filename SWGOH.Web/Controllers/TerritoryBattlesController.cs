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

namespace SWGOH.Web.Controllers
{
    public class TerritoryBattlesController : Controller
    {
        private SwgohDb db = new SwgohDb();
        ApplicationDbContext userDb = new ApplicationDbContext();

        // GET: TerritoryBattles
        public ActionResult Index(Guid id)
        {
            var territoryBattles = db.TerritoryBattles.Where(x => x.Guild_Id == id).Include(t => t.Guild).Include(t => t.Phase1).Include(t => t.Phase2).Include(t => t.Phase3).Include(t => t.Phase4).Include(t => t.Phase5).Include(t => t.Phase6);
            return View(territoryBattles.ToList());
        }

        // GET: TerritoryBattles/Details/5
        public ActionResult Details(Guid? id)
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

            territoryBattle.Phase1 = new TerritoryBattlePhase()
            {
                Id = Guid.NewGuid(),
                RequiredStars = 2,
                HasSecondTerritory = false,
                Territory1 = new PhaseTerritory()
                {
                    Id = Guid.NewGuid(),
                    TerritoryPlatoon1 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon2 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon3 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon4 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon5 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon6 = new TerritoryPlatoon() { Id = Guid.NewGuid() }
                },
            };

            territoryBattle.Phase2 = new TerritoryBattlePhase()
            {
                Id = Guid.NewGuid(),
                RequiredStars = 3,
                HasSecondTerritory = true,
                Territory1 = new PhaseTerritory()
                {
                    Id = Guid.NewGuid(),
                    TerritoryPlatoon1 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon2 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon3 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon4 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon5 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon6 = new TerritoryPlatoon() { Id = Guid.NewGuid() }
                },
                Territory2 = new PhaseTerritory()
                {
                    Id = Guid.NewGuid(),
                    TerritoryPlatoon1 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon2 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon3 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon4 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon5 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon6 = new TerritoryPlatoon() { Id = Guid.NewGuid() }
                },
            };

            territoryBattle.Phase3 = new TerritoryBattlePhase()
            {
                Id = Guid.NewGuid(),
                RequiredStars = 4,
                HasSecondTerritory = true,
                Territory1 = new PhaseTerritory()
                {
                    Id = Guid.NewGuid(),
                    TerritoryPlatoon1 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon2 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon3 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon4 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon5 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon6 = new TerritoryPlatoon() { Id = Guid.NewGuid() }
                },
                Territory2 = new PhaseTerritory()
                {
                    Id = Guid.NewGuid(),
                    TerritoryPlatoon1 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon2 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon3 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon4 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon5 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon6 = new TerritoryPlatoon() { Id = Guid.NewGuid() }
                },
            };

            territoryBattle.Phase4 = new TerritoryBattlePhase()
            {
                Id = Guid.NewGuid(),
                RequiredStars = 5,
                HasSecondTerritory = true,
                Territory1 = new PhaseTerritory()
                {
                    Id = Guid.NewGuid(),
                    TerritoryPlatoon1 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon2 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon3 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon4 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon5 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon6 = new TerritoryPlatoon() { Id = Guid.NewGuid() }
                },
                Territory2 = new PhaseTerritory()
                {
                    Id = Guid.NewGuid(),
                    TerritoryPlatoon1 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon2 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon3 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon4 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon5 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon6 = new TerritoryPlatoon() { Id = Guid.NewGuid() }
                },
            };

            territoryBattle.Phase5 = new TerritoryBattlePhase()
            {
                Id = Guid.NewGuid(),
                RequiredStars = 6,
                HasSecondTerritory = true,
                Territory1 = new PhaseTerritory()
                {
                    Id = Guid.NewGuid(),
                    TerritoryPlatoon1 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon2 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon3 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon4 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon5 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon6 = new TerritoryPlatoon() { Id = Guid.NewGuid() }
                },
                Territory2 = new PhaseTerritory()
                {
                    Id = Guid.NewGuid(),
                    TerritoryPlatoon1 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon2 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon3 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon4 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon5 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon6 = new TerritoryPlatoon() { Id = Guid.NewGuid() }
                },
            };

            territoryBattle.Phase6 = new TerritoryBattlePhase()
            {
                Id = Guid.NewGuid(),
                RequiredStars = 7,
                HasSecondTerritory = true,
                Territory1 = new PhaseTerritory()
                {
                    Id = Guid.NewGuid(),
                    TerritoryPlatoon1 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon2 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon3 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon4 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon5 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon6 = new TerritoryPlatoon() { Id = Guid.NewGuid() }
                },
                Territory2 = new PhaseTerritory()
                {
                    Id = Guid.NewGuid(),
                    TerritoryPlatoon1 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon2 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon3 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon4 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon5 = new TerritoryPlatoon() { Id = Guid.NewGuid() },
                    TerritoryPlatoon6 = new TerritoryPlatoon() { Id = Guid.NewGuid() }
                },
            };

            if (ModelState.IsValid)
            {
                territoryBattle.Id = Guid.NewGuid();
                db.TerritoryBattles.Add(territoryBattle);
                db.SaveChanges();
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
            ViewBag.Phase1_Id = new SelectList(db.TerritoryBattlePhases, "Id", "Id", territoryBattle.Phase1_Id);
            ViewBag.Phase2_Id = new SelectList(db.TerritoryBattlePhases, "Id", "Id", territoryBattle.Phase2_Id);
            ViewBag.Phase3_Id = new SelectList(db.TerritoryBattlePhases, "Id", "Id", territoryBattle.Phase3_Id);
            ViewBag.Phase4_Id = new SelectList(db.TerritoryBattlePhases, "Id", "Id", territoryBattle.Phase4_Id);
            ViewBag.Phase5_Id = new SelectList(db.TerritoryBattlePhases, "Id", "Id", territoryBattle.Phase5_Id);
            ViewBag.Phase6_Id = new SelectList(db.TerritoryBattlePhases, "Id", "Id", territoryBattle.Phase6_Id);
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
            ViewBag.Phase1_Id = new SelectList(db.TerritoryBattlePhases, "Id", "Id", territoryBattle.Phase1_Id);
            ViewBag.Phase2_Id = new SelectList(db.TerritoryBattlePhases, "Id", "Id", territoryBattle.Phase2_Id);
            ViewBag.Phase3_Id = new SelectList(db.TerritoryBattlePhases, "Id", "Id", territoryBattle.Phase3_Id);
            ViewBag.Phase4_Id = new SelectList(db.TerritoryBattlePhases, "Id", "Id", territoryBattle.Phase4_Id);
            ViewBag.Phase5_Id = new SelectList(db.TerritoryBattlePhases, "Id", "Id", territoryBattle.Phase5_Id);
            ViewBag.Phase6_Id = new SelectList(db.TerritoryBattlePhases, "Id", "Id", territoryBattle.Phase6_Id);
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
