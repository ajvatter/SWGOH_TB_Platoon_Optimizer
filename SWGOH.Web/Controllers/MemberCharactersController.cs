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

namespace SWGOH.Web.Controllers
{
    public class MemberCharactersController : Controller
    {
        private SwgohDb db = new SwgohDb();

        // GET: MemberCharacters
        public ActionResult Index(Guid id)
        {
            var memberCharacters = db.MemberCharacters.Where(x => x.Member_Id == id).OrderBy(x => x.Character.Name);
            return View(memberCharacters.ToList());
        }

        // GET: MemberCharacters/Details/5
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
        public ActionResult DeleteConfirmed(Guid id)
        {
            MemberCharacter memberCharacter = db.MemberCharacters.Find(id);
            db.MemberCharacters.Remove(memberCharacter);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult CharCount(Guid id)
        {
            var memberCharacters = db.MemberCharacters.Where(x => x.Member.Guild_Id == id);
            var characters = db.Characters.Where(x => x.Id == x.Id).OrderBy(x => x.Name);
            List<CharCount> charCount = new List<CharCount>();

            foreach (var character in characters)
            {
                CharCount newCharCount = new CharCount();
                newCharCount.Id = character.Id;
                newCharCount.Name = character.DisplayName;
                newCharCount.OneStarCount = memberCharacters.Where(x => x.Character_Id == character.Id && x.Stars == 1).Count();
                newCharCount.TwoStarCount = memberCharacters.Where(x => x.Character_Id == character.Id && x.Stars == 2).Count();
                newCharCount.ThreeStarCount = memberCharacters.Where(x => x.Character_Id == character.Id && x.Stars == 3).Count();
                newCharCount.FourStarCount = memberCharacters.Where(x => x.Character_Id == character.Id && x.Stars == 4).Count();
                newCharCount.FiveStarCount = memberCharacters.Where(x => x.Character_Id == character.Id && x.Stars == 5).Count();
                newCharCount.SixStarCount = memberCharacters.Where(x => x.Character_Id == character.Id && x.Stars == 6).Count();
                newCharCount.SevenStarCount = memberCharacters.Where(x => x.Character_Id == character.Id && x.Stars == 7).Count();
                charCount.Add(newCharCount);
            }

            return View(charCount);
        }

        public ActionResult MembersWithCharacter(Guid id)
        {
            var character = db.Characters.Where(x => x.Id == id).FirstOrDefault();
            var membersCharacters = db.MemberCharacters.Where(x => x.Character_Id == character.Id).OrderBy(x => x.Member.Name);

            MembersWithCharacter membersWithCharacter = new MembersWithCharacter() { Character = character, MembersCharacters = new List<MemberCharacter>() };

            foreach (var member in membersCharacters)
            {
                membersWithCharacter.MembersCharacters.Add(member);
            }

            return View(membersWithCharacter);
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
