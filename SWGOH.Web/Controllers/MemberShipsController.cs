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
using System.Web.Caching;

namespace SWGOH.Web.Controllers
{
    public class MemberShipsController : Controller
    {
        private SwgohDb db = new SwgohDb();
        private ApplicationDbContext userDb = new ApplicationDbContext();

        // GET: MemberShips
        public ActionResult Index()
        {
            var memberShips = db.MemberShips.Include(m => m.Member).Include(m => m.Ship);
            return View(memberShips.ToList());
        }

        // GET: MemberShips/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberShip memberShip = db.MemberShips.Find(id);
            if (memberShip == null)
            {
                return HttpNotFound();
            }
            return View(memberShip);
        }

        // GET: MemberShips/Create
        public ActionResult Create()
        {
            ViewBag.Member_Id = new SelectList(db.Members, "Id", "Name");
            ViewBag.Ship_Id = new SelectList(db.Ships, "Id", "Name");
            return View();
        }

        // POST: MemberShips/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Level,Stars,Power,Ship_Id,Member_Id")] MemberShip memberShip)
        {
            if (ModelState.IsValid)
            {
                memberShip.Id = Guid.NewGuid();
                db.MemberShips.Add(memberShip);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Member_Id = new SelectList(db.Members, "Id", "Name", memberShip.Member_Id);
            ViewBag.Ship_Id = new SelectList(db.Ships, "Id", "Name", memberShip.Ship_Id);
            return View(memberShip);
        }

        // GET: MemberShips/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberShip memberShip = db.MemberShips.Find(id);
            if (memberShip == null)
            {
                return HttpNotFound();
            }
            ViewBag.Member_Id = new SelectList(db.Members, "Id", "Name", memberShip.Member_Id);
            ViewBag.Ship_Id = new SelectList(db.Ships, "Id", "Name", memberShip.Ship_Id);
            return View(memberShip);
        }

        // POST: MemberShips/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Level,Stars,Power,Ship_Id,Member_Id")] MemberShip memberShip)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memberShip).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Member_Id = new SelectList(db.Members, "Id", "Name", memberShip.Member_Id);
            ViewBag.Ship_Id = new SelectList(db.Ships, "Id", "Name", memberShip.Ship_Id);
            return View(memberShip);
        }

        // GET: MemberShips/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberShip memberShip = db.MemberShips.Find(id);
            if (memberShip == null)
            {
                return HttpNotFound();
            }
            return View(memberShip);
        }

        // POST: MemberShips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            MemberShip memberShip = db.MemberShips.Find(id);
            db.MemberShips.Remove(memberShip);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult ShipCount(Guid? id)
        {
            if (id == null)
            {
                if (User.Identity.Name != null && User.Identity.Name != "")
                {
                    id = userDb.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault().Guild_Id;
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            var memberShips = db.MemberShips.Where(x => x.Member.Guild_Id == id);
            var ships = db.Ships.Where(x => x.Id == x.Id).OrderBy(x => x.Name);
            //List<CharCount> charCount = new List<CharCount>();

            List<ShipCountModel> shipCount = (List<ShipCountModel>)HttpContext.Cache.Get("CharCount" + id.ToString());
            if (shipCount == null)
            {
                shipCount = new List<ShipCountModel>();
                foreach (var ship in ships)
                {
                    ShipCountModel newShipCount = new ShipCountModel();
                    newShipCount.Id = ship.Id;
                    newShipCount.Name = ship.DisplayName;
                    newShipCount.Alignment = ship.Alignment;
                    newShipCount.OneStarCount = memberShips.Where(x => x.Ship_Id == ship.Id && x.Stars == 1).Count();
                    newShipCount.TwoStarCount = memberShips.Where(x => x.Ship_Id == ship.Id && x.Stars == 2).Count();
                    newShipCount.ThreeStarCount = memberShips.Where(x => x.Ship_Id == ship.Id && x.Stars == 3).Count();
                    newShipCount.FourStarCount = memberShips.Where(x => x.Ship_Id == ship.Id && x.Stars == 4).Count();
                    newShipCount.FiveStarCount = memberShips.Where(x => x.Ship_Id == ship.Id && x.Stars == 5).Count();
                    newShipCount.SixStarCount = memberShips.Where(x => x.Ship_Id == ship.Id && x.Stars == 6).Count();
                    newShipCount.SevenStarCount = memberShips.Where(x => x.Ship_Id == ship.Id && x.Stars == 7).Count();
                    shipCount.Add(newShipCount);
                }

                HttpContext.Cache.Insert("CharCount" + id.ToString(), shipCount, null, Cache.NoAbsoluteExpiration, new TimeSpan(24, 0, 0));
            }

            return View(shipCount);
        }

        [Authorize]
        public ActionResult MembersWithShip(Guid id)
        {
            var guildId = userDb.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault().Guild_Id;
            var ship = db.Ships.Where(x => x.Id == id).FirstOrDefault();
            var membersShips = db.MemberShips.Where(x => x.Ship_Id == ship.Id && x.Member.Guild_Id == guildId).OrderBy(x => x.Member.Name);

            MembersWithShip membersWithShip = new MembersWithShip() { Ship = ship, MembersShips = new List<MemberShip>() };

            foreach (var member in membersShips)
            {
                membersWithShip.MembersShips.Add(member);
            }

            return View(membersWithShip);
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
