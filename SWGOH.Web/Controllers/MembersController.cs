﻿using AutoMapper;
using EntityFramework.BulkExtensions;
using Nop.Web.Framework.Kendoui;
using SWGOH.Entities;
using SWGOH.Web.DataContexts;
using SWGOH.Web.Models;
using SWGOH.Web.ViewModels;
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

        [Authorize]
        public ActionResult Index(Guid? id)
        {            
            return View();
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
            var phaseReportChars = db.PhaseReports.Where(x => x.MemberCharacter.Member_Id == id).ToList();
            db.BulkDelete(phaseReportChars);
            db = new SwgohDb();
            var phaseReportShips = db.PhaseReports.Where(x => x.MemberShip.Member_Id == id).ToList();
            db.BulkDelete(phaseReportShips);
            db = new SwgohDb();

            Member member = db.Members.Find(id);
            Guild guild = db.Guilds.Find(member.Guild_Id);
            guild.CharacterPower = guild.CharacterPower - member.CharacterPower;
            guild.ShipPower = guild.ShipPower - member.ShipPower;
            db.Entry(guild).State = EntityState.Modified;
            db.Members.Remove(member);
            db.Entry(guild).State = EntityState.Modified;
            db.SaveChanges();

            var tbps = db.TerritoryBattlePhases.Where(x => x.TerritoryBattle.Guild_Id == guild.Id);

            foreach (var tbp in tbps)
            {
                tbp.RefreshReport = true;
            }

            db.BulkUpdate(tbps);

            return RedirectToAction("Details", "Guilds", new { id = member.Guild_Id });
        }

        [HttpPost]
        public virtual ActionResult MemberList(DataSourceRequest command, Member model)
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

            var members = db.Members.Where(x => x.Guild_Id == id).ToList();
            var gridModel = new DataSourceResult();
            gridModel.Data = members.Select(x =>
            {
                var characterModel = Mapper.Map<Member, MemberModel>(x);

                return characterModel;
            }).OrderBy(x => x.DisplayName);
            gridModel.Total = members.Count();

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
