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
using SWGOH.Web.ViewModels;
using AutoMapper;
using EntityFramework.BulkExtensions;

namespace SWGOH.Web.Controllers
{
    public class TerritoryPlatoonsController : Controller
    {
        private SwgohDb db = new SwgohDb();

        // GET: TerritoryPlatoons
        public ActionResult Index()
        {
            var territoryPlatoons = db.TerritoryPlatoons.ToList();
            return View(territoryPlatoons);
        }

        // GET: TerritoryPlatoons/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TerritoryPlatoon territoryPlatoon = db.TerritoryPlatoons.Include(x => x.PlatoonCharacters).Include(x => x.PlatoonShips).SingleOrDefault(x => x.Id == id);
            if (territoryPlatoon == null)
            {
                return HttpNotFound();
            }
            TerritoryPlatoonModel model = Mapper.Map<TerritoryPlatoon, TerritoryPlatoonModel>(territoryPlatoon);
            return View(model);
        }

        // GET: TerritoryPlatoons/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TerritoryPlatoons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Character1_Id,Character2_Id,Character3_Id,Character4_Id,Character5_Id,Character6_Id,Character7_Id,Character8_Id,Character9_Id,Character10_Id,Character11_Id,Character12_Id,Character13_Id,Character14_Id,Character15_Id,Character1Member_Id,Character2Member_Id,Character3Member_Id,Character4Member_Id,Character5Member_Id,Character6Member_Id,Character7Member_Id,Character8Member_Id,Character9Member_Id,Character10Member_Id,Character11Member_Id,Character12Member_Id,Character13Member_Id,Character14Member_Id,Character15Member_Id")] TerritoryPlatoon territoryPlatoon)
        {
            if (ModelState.IsValid)
            {
                territoryPlatoon.Id = Guid.NewGuid();
                db.TerritoryPlatoons.Add(territoryPlatoon);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(territoryPlatoon);
        }

        // GET: TerritoryPlatoons/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TerritoryPlatoon territoryPlatoon = db.TerritoryPlatoons.Include(x => x.PlatoonCharacters).Include(x => x.PlatoonShips).SingleOrDefault(x => x.Id == id);
            if (territoryPlatoon == null)
            {
                return HttpNotFound();
            }
            TerritoryPlatoonModel model = Mapper.Map<TerritoryPlatoon, TerritoryPlatoonModel>(territoryPlatoon);

            model.Items = db.Characters.Where(x => x.Alignment == Alignment.LightSide).OrderBy(x => x.DisplayName);           
            model.Ships = db.Ships.OrderBy(x => x.DisplayName);
            return View(model);
        }

        // POST: TerritoryPlatoons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TerritoryPlatoonModel territoryPlatoonModel)
        {
            if (ModelState.IsValid)
            {
                TerritoryPlatoon territoryPlatoon = db.TerritoryPlatoons.Include(x => x.PlatoonCharacters).Include(x => x.PlatoonShips).SingleOrDefault(x => x.Id == territoryPlatoonModel.Id);

                if (territoryPlatoon.PlatoonCharacters.Count() != 0)
                {
                    List<PlatoonCharacter> pcUpdate = new List<PlatoonCharacter>();

                    var character1 = territoryPlatoon.PlatoonCharacters.Where(x => x.Id == territoryPlatoonModel.Character1.Id).FirstOrDefault();
                    var character2 = territoryPlatoon.PlatoonCharacters.Where(x => x.Id == territoryPlatoonModel.Character2.Id).FirstOrDefault();
                    var character3 = territoryPlatoon.PlatoonCharacters.Where(x => x.Id == territoryPlatoonModel.Character3.Id).FirstOrDefault();
                    var character4 = territoryPlatoon.PlatoonCharacters.Where(x => x.Id == territoryPlatoonModel.Character4.Id).FirstOrDefault();
                    var character5 = territoryPlatoon.PlatoonCharacters.Where(x => x.Id == territoryPlatoonModel.Character5.Id).FirstOrDefault();
                    var character6 = territoryPlatoon.PlatoonCharacters.Where(x => x.Id == territoryPlatoonModel.Character6.Id).FirstOrDefault();
                    var character7 = territoryPlatoon.PlatoonCharacters.Where(x => x.Id == territoryPlatoonModel.Character7.Id).FirstOrDefault();
                    var character8 = territoryPlatoon.PlatoonCharacters.Where(x => x.Id == territoryPlatoonModel.Character8.Id).FirstOrDefault();
                    var character9 = territoryPlatoon.PlatoonCharacters.Where(x => x.Id == territoryPlatoonModel.Character9.Id).FirstOrDefault();
                    var character10 = territoryPlatoon.PlatoonCharacters.Where(x => x.Id == territoryPlatoonModel.Character10.Id).FirstOrDefault();
                    var character11 = territoryPlatoon.PlatoonCharacters.Where(x => x.Id == territoryPlatoonModel.Character11.Id).FirstOrDefault();
                    var character12 = territoryPlatoon.PlatoonCharacters.Where(x => x.Id == territoryPlatoonModel.Character12.Id).FirstOrDefault();
                    var character13 = territoryPlatoon.PlatoonCharacters.Where(x => x.Id == territoryPlatoonModel.Character13.Id).FirstOrDefault();
                    var character14 = territoryPlatoon.PlatoonCharacters.Where(x => x.Id == territoryPlatoonModel.Character14.Id).FirstOrDefault();
                    var character15 = territoryPlatoon.PlatoonCharacters.Where(x => x.Id == territoryPlatoonModel.Character15.Id).FirstOrDefault();
                    character1 = territoryPlatoonModel.Character1;
                    character2 = territoryPlatoonModel.Character2;
                    character3 = territoryPlatoonModel.Character3;
                    character4 = territoryPlatoonModel.Character4;
                    character5 = territoryPlatoonModel.Character5;
                    character6 = territoryPlatoonModel.Character6;
                    character7 = territoryPlatoonModel.Character7;
                    character8 = territoryPlatoonModel.Character8;
                    character9 = territoryPlatoonModel.Character9;
                    character10 = territoryPlatoonModel.Character10;
                    character11 = territoryPlatoonModel.Character11;
                    character12 = territoryPlatoonModel.Character12;
                    character13 = territoryPlatoonModel.Character13;
                    character14 = territoryPlatoonModel.Character14;
                    character15 = territoryPlatoonModel.Character15;
                    pcUpdate.Add(character1);
                    pcUpdate.Add(character2);
                    pcUpdate.Add(character3);
                    pcUpdate.Add(character4);
                    pcUpdate.Add(character5);
                    pcUpdate.Add(character6);
                    pcUpdate.Add(character7);
                    pcUpdate.Add(character8);
                    pcUpdate.Add(character9);
                    pcUpdate.Add(character10);
                    pcUpdate.Add(character11);
                    pcUpdate.Add(character12);
                    pcUpdate.Add(character13);
                    pcUpdate.Add(character14);
                    pcUpdate.Add(character15);
                    db.BulkUpdate(pcUpdate);
                }
                else
                {
                    List<PlatoonShip> psUpdate = new List<PlatoonShip>();

                    var ship1 = territoryPlatoon.PlatoonShips.Where(x => x.Id == territoryPlatoonModel.Ship1.Id).FirstOrDefault();
                    var ship2 = territoryPlatoon.PlatoonShips.Where(x => x.Id == territoryPlatoonModel.Ship2.Id).FirstOrDefault();
                    var ship3 = territoryPlatoon.PlatoonShips.Where(x => x.Id == territoryPlatoonModel.Ship3.Id).FirstOrDefault();
                    var ship4 = territoryPlatoon.PlatoonShips.Where(x => x.Id == territoryPlatoonModel.Ship4.Id).FirstOrDefault();
                    var ship5 = territoryPlatoon.PlatoonShips.Where(x => x.Id == territoryPlatoonModel.Ship5.Id).FirstOrDefault();
                    var ship6 = territoryPlatoon.PlatoonShips.Where(x => x.Id == territoryPlatoonModel.Ship6.Id).FirstOrDefault();
                    var ship7 = territoryPlatoon.PlatoonShips.Where(x => x.Id == territoryPlatoonModel.Ship7.Id).FirstOrDefault();
                    var ship8 = territoryPlatoon.PlatoonShips.Where(x => x.Id == territoryPlatoonModel.Ship8.Id).FirstOrDefault();
                    var ship9 = territoryPlatoon.PlatoonShips.Where(x => x.Id == territoryPlatoonModel.Ship9.Id).FirstOrDefault();
                    var ship10 = territoryPlatoon.PlatoonShips.Where(x => x.Id == territoryPlatoonModel.Ship10.Id).FirstOrDefault();
                    var ship11 = territoryPlatoon.PlatoonShips.Where(x => x.Id == territoryPlatoonModel.Ship11.Id).FirstOrDefault();
                    var ship12 = territoryPlatoon.PlatoonShips.Where(x => x.Id == territoryPlatoonModel.Ship12.Id).FirstOrDefault();
                    var ship13 = territoryPlatoon.PlatoonShips.Where(x => x.Id == territoryPlatoonModel.Ship13.Id).FirstOrDefault();
                    var ship14 = territoryPlatoon.PlatoonShips.Where(x => x.Id == territoryPlatoonModel.Ship14.Id).FirstOrDefault();
                    var ship15 = territoryPlatoon.PlatoonShips.Where(x => x.Id == territoryPlatoonModel.Ship15.Id).FirstOrDefault();
                    ship1 = territoryPlatoonModel.Ship1;
                    ship2 = territoryPlatoonModel.Ship2;
                    ship3 = territoryPlatoonModel.Ship3;
                    ship4 = territoryPlatoonModel.Ship4;
                    ship5 = territoryPlatoonModel.Ship5;
                    ship6 = territoryPlatoonModel.Ship6;
                    ship7 = territoryPlatoonModel.Ship7;
                    ship8 = territoryPlatoonModel.Ship8;
                    ship9 = territoryPlatoonModel.Ship9;
                    ship10 = territoryPlatoonModel.Ship10;
                    ship11 = territoryPlatoonModel.Ship11;
                    ship12 = territoryPlatoonModel.Ship12;
                    ship13 = territoryPlatoonModel.Ship13;
                    ship14 = territoryPlatoonModel.Ship14;
                    ship15 = territoryPlatoonModel.Ship15;
                    psUpdate.Add(ship1);
                    psUpdate.Add(ship2);
                    psUpdate.Add(ship3);
                    psUpdate.Add(ship4);
                    psUpdate.Add(ship5);
                    psUpdate.Add(ship6);
                    psUpdate.Add(ship7);
                    psUpdate.Add(ship8);
                    psUpdate.Add(ship9);
                    psUpdate.Add(ship10);
                    psUpdate.Add(ship11);
                    psUpdate.Add(ship12);
                    psUpdate.Add(ship13);
                    psUpdate.Add(ship14);
                    psUpdate.Add(ship15);
                    db.BulkUpdate(psUpdate);
                }
                HttpContext.Cache.Remove("PlatoonAssignmentsByCharacter" + territoryPlatoon.PhaseTerritory.TerritoryBattlePhase_Id.ToString());
                HttpContext.Cache.Remove("PlatoonAssignments" + territoryPlatoon.PhaseTerritory.TerritoryBattlePhase_Id.ToString());
                HttpContext.Cache.Remove("PlatoonAssignmentsGrid" + territoryPlatoon.PhaseTerritory.TerritoryBattlePhase_Id.ToString());
                HttpContext.Cache.Remove("ShipPlatoonAssignments" + territoryPlatoon.PhaseTerritory.TerritoryBattlePhase_Id.ToString());
                HttpContext.Cache.Remove("ShipPlatoonAssignmentsGrid" + territoryPlatoon.PhaseTerritory.TerritoryBattlePhase_Id.ToString());
                db.BulkDelete(db.PhaseReports.Where(x => x.TerritoryBattlePhase_Id == territoryPlatoon.PhaseTerritory.TerritoryBattlePhase_Id).ToList());

                return RedirectToAction("Details", "TerritoryBattlePhases", new { id = territoryPlatoon.PhaseTerritory.TerritoryBattlePhase_Id });
            }

            return View(territoryPlatoonModel);
        }

        // GET: TerritoryPlatoons/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TerritoryPlatoon territoryPlatoon = db.TerritoryPlatoons.Find(id);
            if (territoryPlatoon == null)
            {
                return HttpNotFound();
            }
            return View(territoryPlatoon);
        }

        // POST: TerritoryPlatoons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            TerritoryPlatoon territoryPlatoon = db.TerritoryPlatoons.Find(id);
            db.TerritoryPlatoons.Remove(territoryPlatoon);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult ClosePlatoon(Guid id)
        {
            TerritoryPlatoon platoon = db.TerritoryPlatoons.Find(id);
            Guid tbpId = platoon.PhaseTerritory.TerritoryBattlePhase_Id;
            if (platoon.IsClosed == false) platoon.IsClosed = true;
            else platoon.IsClosed = false;

            db.Entry(platoon).State = EntityState.Modified;
            db.SaveChanges();

            HttpContext.Cache.Remove("PlatoonAssignmentsByCharacter" + platoon.PhaseTerritory.TerritoryBattlePhase_Id.ToString());
            HttpContext.Cache.Remove("PlatoonAssignments" + platoon.PhaseTerritory.TerritoryBattlePhase_Id.ToString());
            HttpContext.Cache.Remove("PlatoonAssignmentsGrid" + platoon.PhaseTerritory.TerritoryBattlePhase_Id.ToString());
            HttpContext.Cache.Remove("ShipPlatoonAssignments" + platoon.PhaseTerritory.TerritoryBattlePhase_Id.ToString());
            HttpContext.Cache.Remove("ShipPlatoonAssignmentsGrid" + platoon.PhaseTerritory.TerritoryBattlePhase_Id.ToString());

            db.BulkDelete(db.PhaseReports.Where(x => x.TerritoryBattlePhase_Id == platoon.PhaseTerritory.TerritoryBattlePhase_Id).ToList());
            return RedirectToAction("Details", "TerritoryBattlePhases", new { id = tbpId });
        }

        public ActionResult CopyPlatoon(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TerritoryPlatoon territoryPlatoon = db.TerritoryPlatoons.Include(x => x.PlatoonCharacters).SingleOrDefault(x => x.Id == id);
            if (territoryPlatoon == null)
            {
                return HttpNotFound();
            }

            var platoons = db.TerritoryPlatoons.Where(x => x.PhaseTerritory.TerritoryBattlePhase.TerritoryBattle_Id == territoryPlatoon.PhaseTerritory.TerritoryBattlePhase.TerritoryBattle_Id
                                                                        && x.PhaseTerritory.TerritoryBattlePhase.Phase <= territoryPlatoon.PhaseTerritory.TerritoryBattlePhase.Phase)
                                                                        .Select(s => new
                                                                        {
                                                                            Text = "Phase " + s.PhaseTerritory.TerritoryBattlePhase.Phase + " - " + s.PhaseTerritory.PhaseLocation + " - Platoon " + s.PlatoonNumber,
                                                                            Value = s.Id
                                                                        }).OrderBy(x => x.Text).AsEnumerable();

            SelectList selectList = new SelectList(platoons, "Value", "Text");

            PlatoonCopyModel model = new PlatoonCopyModel()
            {
                PlatoonOptions = selectList,
                CopyToPlatoonId = id
            };            

            return PartialView("_CopyPlatoon", model);
        }

        // POST: TerritoryPlatoons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CopyPlatoon(PlatoonCopyModel platoonCopyModel)
        {
            if (ModelState.IsValid)
            {
                TerritoryPlatoon territoryPlatoonFrom = db.TerritoryPlatoons.Include(x => x.PlatoonCharacters).SingleOrDefault(x => x.Id == platoonCopyModel.SelectedPlatoonId);
                TerritoryPlatoon territoryPlatoonTo = db.TerritoryPlatoons.Include(x => x.PlatoonCharacters).SingleOrDefault(x => x.Id == platoonCopyModel.CopyToPlatoonId);

                territoryPlatoonTo.PlatoonCharacters.Where(x => x.PlatoonPosition == 1).FirstOrDefault().Character_Id = territoryPlatoonFrom.PlatoonCharacters.Where(x => x.PlatoonPosition == 1).FirstOrDefault().Character_Id;
                territoryPlatoonTo.PlatoonCharacters.Where(x => x.PlatoonPosition == 2).FirstOrDefault().Character_Id = territoryPlatoonFrom.PlatoonCharacters.Where(x => x.PlatoonPosition == 2).FirstOrDefault().Character_Id;
                territoryPlatoonTo.PlatoonCharacters.Where(x => x.PlatoonPosition == 3).FirstOrDefault().Character_Id = territoryPlatoonFrom.PlatoonCharacters.Where(x => x.PlatoonPosition == 3).FirstOrDefault().Character_Id;
                territoryPlatoonTo.PlatoonCharacters.Where(x => x.PlatoonPosition == 4).FirstOrDefault().Character_Id = territoryPlatoonFrom.PlatoonCharacters.Where(x => x.PlatoonPosition == 4).FirstOrDefault().Character_Id;
                territoryPlatoonTo.PlatoonCharacters.Where(x => x.PlatoonPosition == 5).FirstOrDefault().Character_Id = territoryPlatoonFrom.PlatoonCharacters.Where(x => x.PlatoonPosition == 5).FirstOrDefault().Character_Id;
                territoryPlatoonTo.PlatoonCharacters.Where(x => x.PlatoonPosition == 6).FirstOrDefault().Character_Id = territoryPlatoonFrom.PlatoonCharacters.Where(x => x.PlatoonPosition == 6).FirstOrDefault().Character_Id;
                territoryPlatoonTo.PlatoonCharacters.Where(x => x.PlatoonPosition == 7).FirstOrDefault().Character_Id = territoryPlatoonFrom.PlatoonCharacters.Where(x => x.PlatoonPosition == 7).FirstOrDefault().Character_Id;
                territoryPlatoonTo.PlatoonCharacters.Where(x => x.PlatoonPosition == 8).FirstOrDefault().Character_Id = territoryPlatoonFrom.PlatoonCharacters.Where(x => x.PlatoonPosition == 8).FirstOrDefault().Character_Id;
                territoryPlatoonTo.PlatoonCharacters.Where(x => x.PlatoonPosition == 9).FirstOrDefault().Character_Id = territoryPlatoonFrom.PlatoonCharacters.Where(x => x.PlatoonPosition == 9).FirstOrDefault().Character_Id;
                territoryPlatoonTo.PlatoonCharacters.Where(x => x.PlatoonPosition == 10).FirstOrDefault().Character_Id = territoryPlatoonFrom.PlatoonCharacters.Where(x => x.PlatoonPosition == 10).FirstOrDefault().Character_Id;
                territoryPlatoonTo.PlatoonCharacters.Where(x => x.PlatoonPosition == 11).FirstOrDefault().Character_Id = territoryPlatoonFrom.PlatoonCharacters.Where(x => x.PlatoonPosition == 11).FirstOrDefault().Character_Id;
                territoryPlatoonTo.PlatoonCharacters.Where(x => x.PlatoonPosition == 12).FirstOrDefault().Character_Id = territoryPlatoonFrom.PlatoonCharacters.Where(x => x.PlatoonPosition == 12).FirstOrDefault().Character_Id;
                territoryPlatoonTo.PlatoonCharacters.Where(x => x.PlatoonPosition == 13).FirstOrDefault().Character_Id = territoryPlatoonFrom.PlatoonCharacters.Where(x => x.PlatoonPosition == 13).FirstOrDefault().Character_Id;
                territoryPlatoonTo.PlatoonCharacters.Where(x => x.PlatoonPosition == 14).FirstOrDefault().Character_Id = territoryPlatoonFrom.PlatoonCharacters.Where(x => x.PlatoonPosition == 14).FirstOrDefault().Character_Id;
                territoryPlatoonTo.PlatoonCharacters.Where(x => x.PlatoonPosition == 15).FirstOrDefault().Character_Id = territoryPlatoonFrom.PlatoonCharacters.Where(x => x.PlatoonPosition == 15).FirstOrDefault().Character_Id;

                db.Entry(territoryPlatoonTo).State = EntityState.Modified;
                db.SaveChanges();

                HttpContext.Cache.Remove("PlatoonAssignmentsByCharacter" + territoryPlatoonTo.PhaseTerritory.TerritoryBattlePhase_Id.ToString());
                HttpContext.Cache.Remove("PlatoonAssignments" + territoryPlatoonTo.PhaseTerritory.TerritoryBattlePhase_Id.ToString());
                HttpContext.Cache.Remove("PlatoonAssignmentsGrid" + territoryPlatoonTo.PhaseTerritory.TerritoryBattlePhase_Id.ToString());
                HttpContext.Cache.Remove("ShipPlatoonAssignments" + territoryPlatoonTo.PhaseTerritory.TerritoryBattlePhase_Id.ToString());
                HttpContext.Cache.Remove("ShipPlatoonAssignmentsGrid" + territoryPlatoonTo.PhaseTerritory.TerritoryBattlePhase_Id.ToString());

                db.BulkDelete(db.PhaseReports.Where(x => x.TerritoryBattlePhase_Id == territoryPlatoonTo.PhaseTerritory.TerritoryBattlePhase_Id).ToList());

                return RedirectToAction("Edit", "TerritoryPlatoons", new { id = territoryPlatoonTo.Id });
            }

            return RedirectToAction("");
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
