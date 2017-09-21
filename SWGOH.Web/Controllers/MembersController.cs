using SWGOH.Entities;
using SWGOH.Web.DataContexts;
using SWGOH.Web.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace SWGOH.Web.Controllers
{
    public class MembersController : Controller
    {
        private SwgohDb db = new SwgohDb();
        private ApplicationDbContext userDb = new ApplicationDbContext();

        // GET: Members
        //public ActionResult Index()
        //{
        //    var members = db.Members.Include(m => m.Guild);
        //    return View(members.ToList());
        //}

        [Authorize]
        public ActionResult Index(Guid? id)
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
            var members = db.Members.Where(m => m.Guild.Id == id).OrderBy(x => x.Name);
            return View(members.ToList());
        }

        // GET: Members/Details/5
        [Authorize]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Members/Create
        [Authorize(Roles = "Administrators")]    
        public ActionResult Create()
        {
            ViewBag.Guild_Id = new SelectList(db.Guilds, "Id", "Name");
            return View();
        }

        // POST: Members/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrators")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,UrlExt,Guild_Id")] Member member)
        {
            if (ModelState.IsValid)
            {
                member.Id = Guid.NewGuid();
                db.Members.Add(member);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Guild_Id = new SelectList(db.Guilds, "Id", "Name", member.Guild_Id);
            return RedirectToAction("Details", "Guilds", new { id = member.Guild_Id });
        }

        // GET: Members/Edit/5
        [Authorize(Roles = "Administrators")]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            ViewBag.Guild_Id = new SelectList(db.Guilds, "Id", "Name", member.Guild_Id);
            return View(member);
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [Authorize(Roles = "Administrators")]
        public ActionResult Edit(Member updateMember)
        {
            if (ModelState.IsValid)
            {
                db.Entry(updateMember).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Guilds", new { id = updateMember.Guild_Id });
            }
            ViewBag.Guild_Id = new SelectList(db.Guilds, "Id", "Name", updateMember.Guild_Id);
            return RedirectToAction("Details", "Guilds", new { id = updateMember.Guild_Id });
        }

        // GET: Members/Delete/5
        [Authorize(Roles = "Officers")]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = db.Members.Find(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Officers")]
        public ActionResult DeleteConfirmed(Guid id)
        {            
            Member member = db.Members.Find(id);
            Guild guild = db.Guilds.Find(member.Guild_Id);
            guild.CharacterPower = guild.CharacterPower - member.CharacterPower;
            guild.ShipPower = guild.ShipPower - member.ShipPower;
            db.Entry(guild).State = EntityState.Modified;
            db.Members.Remove(member);
            db.SaveChanges();

            HttpContext.Cache.Remove("CharCount" + member.Guild_Id.ToString());

            return RedirectToAction("Details", "Guilds", new { id = member.Guild_Id });
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
