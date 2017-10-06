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
using System.Data.SqlClient;
using System.Configuration;
using Nop.Web.Framework.Kendoui;

namespace SWGOH.Web.Controllers
{
    public class MemberShipsController : Controller
    {
        private SwgohDb db = new SwgohDb();
        private ApplicationDbContext userDb = new ApplicationDbContext();

        // GET: MemberShips
        public ActionResult Index(Guid id)
        {
            var memberShips = db.MemberShips.Where(x => x.Member_Id == id).OrderBy(x => x.Ship.Name);
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
            return View();
        }

        [HttpPost]
        [Authorize]
        public ActionResult ShipCountData(DataSourceRequest command, CharCount model)
        {
            Guid id;
            if (User.Identity.Name != null && User.Identity.Name != "")
            {
                id = userDb.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault().Guild_Id;
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }

            var memberShips = db.MemberShips.Where(x => x.Member.Guild_Id == id);
            var ships = db.Ships.Where(x => x.Id == x.Id).OrderBy(x => x.Name);
            var gridModel = new DataSourceResult();

            DataSet ds = new DataSet("Counts");

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SwgohDb"].ConnectionString))
            {
                SqlCommand sqlComm = new SqlCommand("ShipCounts", conn);
                sqlComm.Parameters.AddWithValue("@Guild_Id", id);

                sqlComm.CommandType = CommandType.StoredProcedure;

                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = sqlComm;

                da.Fill(ds);

                gridModel.Data = ds.Tables[0].AsEnumerable().Select(
                    dataRow => new ShipCountModel
                    {
                        Id = dataRow.Field<Guid>("Ship_Id"),
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
