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

            model.Items = db.Characters.OrderBy(x => x.DisplayName);
            //model.Items = db.Characters.Where(x => x.Alignment == Alignment.LightSide).OrderBy(x => x.DisplayName);           
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
                    var pcUpdate = new List<PlatoonCharacter>();

                    var character1 = territoryPlatoonModel.Character1;
                    var character2 = territoryPlatoonModel.Character2;
                    var character3 = territoryPlatoonModel.Character3;
                    var character4 = territoryPlatoonModel.Character4;
                    var character5 = territoryPlatoonModel.Character5;
                    var character6 = territoryPlatoonModel.Character6;
                    var character7 = territoryPlatoonModel.Character7;
                    var character8 = territoryPlatoonModel.Character8;
                    var character9 = territoryPlatoonModel.Character9;
                    var character10 = territoryPlatoonModel.Character10;
                    var character11 = territoryPlatoonModel.Character11;
                    var character12 = territoryPlatoonModel.Character12;
                    var character13 = territoryPlatoonModel.Character13;
                    var character14 = territoryPlatoonModel.Character14;
                    var character15 = territoryPlatoonModel.Character15;
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
                    var psUpdate = new List<PlatoonShip>();

                    var ship1 = territoryPlatoonModel.Ship1;
                    var ship2 = territoryPlatoonModel.Ship2;
                    var ship3 = territoryPlatoonModel.Ship3;
                    var ship4 = territoryPlatoonModel.Ship4;
                    var ship5 = territoryPlatoonModel.Ship5;
                    var ship6 = territoryPlatoonModel.Ship6;
                    var ship7 = territoryPlatoonModel.Ship7;
                    var ship8 = territoryPlatoonModel.Ship8;
                    var ship9 = territoryPlatoonModel.Ship9;
                    var ship10 = territoryPlatoonModel.Ship10;
                    var ship11 = territoryPlatoonModel.Ship11;
                    var ship12 = territoryPlatoonModel.Ship12;
                    var ship13 = territoryPlatoonModel.Ship13;
                    var ship14 = territoryPlatoonModel.Ship14;
                    var ship15 = territoryPlatoonModel.Ship15;
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

                var tbp = db.TerritoryBattlePhases.Find(territoryPlatoon.PhaseTerritory.TerritoryBattlePhase_Id);

                tbp.RefreshReport = true;

                db.Entry(tbp).State = EntityState.Modified;
                db.SaveChanges();

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

            var tbp = db.TerritoryBattlePhases.Find(platoon.PhaseTerritory.TerritoryBattlePhase_Id);

            tbp.RefreshReport = true;

            db.Entry(tbp).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Details", "TerritoryBattlePhases", new { id = tbpId });
        }

        public ActionResult CopyPlatoon(Guid id)
        {
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

                territoryPlatoonTo.PlatoonCharacters.FirstOrDefault(x => x.PlatoonPosition == 1).Character_Id = territoryPlatoonFrom.PlatoonCharacters.FirstOrDefault(x => x.PlatoonPosition == 1).Character_Id;
                territoryPlatoonTo.PlatoonCharacters.FirstOrDefault(x => x.PlatoonPosition == 2).Character_Id = territoryPlatoonFrom.PlatoonCharacters.FirstOrDefault(x => x.PlatoonPosition == 2).Character_Id;
                territoryPlatoonTo.PlatoonCharacters.FirstOrDefault(x => x.PlatoonPosition == 3).Character_Id = territoryPlatoonFrom.PlatoonCharacters.FirstOrDefault(x => x.PlatoonPosition == 3).Character_Id;
                territoryPlatoonTo.PlatoonCharacters.FirstOrDefault(x => x.PlatoonPosition == 4).Character_Id = territoryPlatoonFrom.PlatoonCharacters.FirstOrDefault(x => x.PlatoonPosition == 4).Character_Id;
                territoryPlatoonTo.PlatoonCharacters.FirstOrDefault(x => x.PlatoonPosition == 5).Character_Id = territoryPlatoonFrom.PlatoonCharacters.FirstOrDefault(x => x.PlatoonPosition == 5).Character_Id;
                territoryPlatoonTo.PlatoonCharacters.FirstOrDefault(x => x.PlatoonPosition == 6).Character_Id = territoryPlatoonFrom.PlatoonCharacters.FirstOrDefault(x => x.PlatoonPosition == 6).Character_Id;
                territoryPlatoonTo.PlatoonCharacters.FirstOrDefault(x => x.PlatoonPosition == 7).Character_Id = territoryPlatoonFrom.PlatoonCharacters.FirstOrDefault(x => x.PlatoonPosition == 7).Character_Id;
                territoryPlatoonTo.PlatoonCharacters.FirstOrDefault(x => x.PlatoonPosition == 8).Character_Id = territoryPlatoonFrom.PlatoonCharacters.FirstOrDefault(x => x.PlatoonPosition == 8).Character_Id;
                territoryPlatoonTo.PlatoonCharacters.FirstOrDefault(x => x.PlatoonPosition == 9).Character_Id = territoryPlatoonFrom.PlatoonCharacters.FirstOrDefault(x => x.PlatoonPosition == 9).Character_Id;
                territoryPlatoonTo.PlatoonCharacters.FirstOrDefault(x => x.PlatoonPosition == 10).Character_Id = territoryPlatoonFrom.PlatoonCharacters.FirstOrDefault(x => x.PlatoonPosition == 10).Character_Id;
                territoryPlatoonTo.PlatoonCharacters.FirstOrDefault(x => x.PlatoonPosition == 11).Character_Id = territoryPlatoonFrom.PlatoonCharacters.FirstOrDefault(x => x.PlatoonPosition == 11).Character_Id;
                territoryPlatoonTo.PlatoonCharacters.FirstOrDefault(x => x.PlatoonPosition == 12).Character_Id = territoryPlatoonFrom.PlatoonCharacters.FirstOrDefault(x => x.PlatoonPosition == 12).Character_Id;
                territoryPlatoonTo.PlatoonCharacters.FirstOrDefault(x => x.PlatoonPosition == 13).Character_Id = territoryPlatoonFrom.PlatoonCharacters.FirstOrDefault(x => x.PlatoonPosition == 13).Character_Id;
                territoryPlatoonTo.PlatoonCharacters.FirstOrDefault(x => x.PlatoonPosition == 14).Character_Id = territoryPlatoonFrom.PlatoonCharacters.FirstOrDefault(x => x.PlatoonPosition == 14).Character_Id;
                territoryPlatoonTo.PlatoonCharacters.FirstOrDefault(x => x.PlatoonPosition == 15).Character_Id = territoryPlatoonFrom.PlatoonCharacters.FirstOrDefault(x => x.PlatoonPosition == 15).Character_Id;

                db.Entry(territoryPlatoonTo).State = EntityState.Modified;
                db.SaveChanges();

                var tbp = db.TerritoryBattlePhases.Find(territoryPlatoonTo.PhaseTerritory.TerritoryBattlePhase_Id);

                tbp.RefreshReport = true;

                db.Entry(tbp).State = EntityState.Modified;
                db.SaveChanges();

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
