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
            TerritoryPlatoon territoryPlatoon = db.TerritoryPlatoons.Include(x => x.PlatoonCharacters).SingleOrDefault(x => x.Id == id);
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
            TerritoryPlatoon territoryPlatoon = db.TerritoryPlatoons.Include(x => x.PlatoonCharacters).SingleOrDefault(x => x.Id == id);
            if (territoryPlatoon == null)
            {
                return HttpNotFound();
            }
            TerritoryPlatoonModel model = Mapper.Map<TerritoryPlatoon, TerritoryPlatoonModel>(territoryPlatoon);

            model.Items = db.Characters.Where(x => x.Alignment == Alignment.LightSide).OrderBy(x => x.DisplayName);           

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
                TerritoryPlatoon territoryPlatoon = db.TerritoryPlatoons.Include(x => x.PlatoonCharacters).SingleOrDefault(x => x.Id == territoryPlatoonModel.Id);
                //territoryPlatoon.IsClosed = territoryPlatoonModel.IsClosed;

                //db.Entry(territoryPlatoon).State = EntityState.Modified;
                //db.SaveChanges();

                List<PlatoonCharacter> pcUpdate = new List<PlatoonCharacter>();

                var character1 = territoryPlatoon.PlatoonCharacters.Where(x => x.Id ==  territoryPlatoonModel.Character1.Id).FirstOrDefault();
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
               
                return RedirectToAction("Details", "TerritoryBattlePhases", new { id = territoryPlatoon.PhaseTerritory.TerritoryBattlePhase.Id });
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
            platoon.IsClosed = true;

            db.Entry(platoon).State = EntityState.Modified;
            db.SaveChanges();
            
            return RedirectToAction("Details", "TerritoryBattlePhases", new { id = platoon.PhaseTerritory.TerritoryBattlePhase.Id });
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
