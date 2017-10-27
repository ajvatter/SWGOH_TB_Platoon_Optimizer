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
using SWGOH.Web.Models;
using System.Web.Caching;
using System.Data.SqlClient;
using System.Configuration;
using Nop.Web.Framework.Kendoui;

namespace SWGOH.Web.Controllers
{
    public class MemberCharactersController : Controller
    {
        private SwgohDb db = new SwgohDb();
        private ApplicationDbContext userDb = new ApplicationDbContext();

        // GET: MemberCharacters
        public ActionResult Index(Guid id)
        {
            var memberCharacters = db.MemberCharacters.Where(x => x.Member_Id == id).OrderBy(x => x.Character.Name);
            return View(memberCharacters.ToList());
        }

        // GET: MemberCharacters/Details/5
        [Authorize]
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberCharacter memberCharacter = db.MemberCharacters.Find(id);
            if (memberCharacter == null)
            {
                return HttpNotFound();
            }
            return View(memberCharacter);
        }

        // GET: MemberCharacters/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.Character_Id = new SelectList(db.Characters, "Id", "Name");
            ViewBag.Member_Id = new SelectList(db.Members, "Id", "Name");
            return View();
        }

        // POST: MemberCharacters/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create([Bind(Include = "Id,Level,Gear,Stars,Power,Character_Id,Member_Id")] MemberCharacter memberCharacter)
        {
            if (ModelState.IsValid)
            {
                memberCharacter.Id = Guid.NewGuid();
                db.MemberCharacters.Add(memberCharacter);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Character_Id = new SelectList(db.Characters, "Id", "Name", memberCharacter.Character_Id);
            ViewBag.Member_Id = new SelectList(db.Members, "Id", "Name", memberCharacter.Member_Id);
            return View(memberCharacter);
        }

        // GET: MemberCharacters/Edit/5
        [Authorize]
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberCharacter memberCharacter = db.MemberCharacters.Find(id);
            if (memberCharacter == null)
            {
                return HttpNotFound();
            }
            ViewBag.Character_Id = new SelectList(db.Characters, "Id", "Name", memberCharacter.Character_Id);
            ViewBag.Member_Id = new SelectList(db.Members, "Id", "Name", memberCharacter.Member_Id);
            return View(memberCharacter);
        }

        // POST: MemberCharacters/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Level,Gear,Stars,Power,Character_Id,Member_Id")] MemberCharacter memberCharacter)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memberCharacter).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Character_Id = new SelectList(db.Characters, "Id", "Name", memberCharacter.Character_Id);
            ViewBag.Member_Id = new SelectList(db.Members, "Id", "Name", memberCharacter.Member_Id);
            return View(memberCharacter);
        }

        // GET: MemberCharacters/Delete/5
        [Authorize]
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberCharacter memberCharacter = db.MemberCharacters.Find(id);
            if (memberCharacter == null)
            {
                return HttpNotFound();
            }
            return View(memberCharacter);
        }

        // POST: MemberCharacters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult DeleteConfirmed(Guid id)
        {
            MemberCharacter memberCharacter = db.MemberCharacters.Find(id);
            db.MemberCharacters.Remove(memberCharacter);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult CharCount(Guid? id)
        {          
            return View();
        }

        [Authorize]
        public ActionResult MembersWithCharacter(Guid id)
        {
            var guildId = userDb.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault().Guild_Id;
            var character = db.Characters.Where(x => x.Id == id).FirstOrDefault();
            var membersCharacters = db.MemberCharacters
                .Where(x => x.Character_Id == character.Id && x.Member.Guild_Id == guildId)
                .OrderByDescending(x => x.Level)
                .ThenByDescending(x => x.Gear);

            MembersWithCharacter membersWithCharacter = new MembersWithCharacter() { Character = character, MembersCharacters = new List<MemberCharacter>() };

            foreach (var member in membersCharacters)
            {
                membersWithCharacter.MembersCharacters.Add(member);
            }


            return View(membersWithCharacter);
        }

        [HttpPost]
        public virtual ActionResult CharCountData(DataSourceRequest command, CharCount model)
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

            var memberCharacters = db.MemberCharacters.Where(x => x.Member.Guild_Id == id);
            var characters = db.Characters.Where(x => x.Id == x.Id).OrderBy(x => x.Name);
            var gridModel = new DataSourceResult();

            DataSet ds = new DataSet("Counts");               

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SwgohDb"].ConnectionString))
            {
                SqlCommand sqlComm = new SqlCommand("CharCounts", conn);
                sqlComm.Parameters.AddWithValue("@Guild_Id", id);

                sqlComm.CommandType = CommandType.StoredProcedure;

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
