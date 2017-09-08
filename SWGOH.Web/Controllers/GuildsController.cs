using HtmlAgilityPack;
using SWGOH.Entities;
using SWGOH.Web.DataContexts;
using SWGOH.Web.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace SWGOH.Web.Controllers
{
    public class GuildsController : Controller
    {
        private SwgohDb db = new SwgohDb();
        ApplicationDbContext userDb = new ApplicationDbContext();

        // GET: Guilds
        [Authorize(Roles = "Administrators")]
        public ActionResult Index()
        {
            return View(db.Guilds.ToList().OrderBy(x => x.Name));
        }

        // GET: Guilds/Details/5
        public ActionResult Details(Guid? id)
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
            Guild guild = db.Guilds.Find(id);
            if (guild == null)
            {
                return HttpNotFound();
            }
            guild.Members = db.Members.Where(x => x.Guild.Id == guild.Id).ToList();
            return View(guild);
        }

        // GET: Guilds/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Guilds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Guild guild)
        {
            if (ModelState.IsValid)
            {
                guild.Id = Guid.NewGuid();
                guild.LastScrape = DateTime.Now.AddHours(-2);
                db.Guilds.Add(guild);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(guild);
        }

        // GET: Guilds/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guild guild = db.Guilds.Find(id);
            if (guild == null)
            {
                return HttpNotFound();
            }
            return View(guild);
        }

        // POST: Guilds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateInput(false)]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Guild guild)
        {
            if (ModelState.IsValid)
            {
                db.Entry(guild).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(guild);
        }

        // GET: Guilds/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guild guild = db.Guilds.Find(id);
            if (guild == null)
            {
                return HttpNotFound();
            }
            return View(guild);
        }

        // POST: Guilds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Guild guild = db.Guilds.Find(id);
            db.Guilds.Remove(guild);
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

        public ActionResult GetRoster(Guid id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Guild guild = db.Guilds.Find(id);
            if (guild == null)
            {
                return HttpNotFound();
            }

            if (DateTime.Now < guild.LastScrape.AddHours(1) && !guild.LastScrape.Equals(DateTime.Parse("1900-01-01 00:00:00.000")) && !User.IsInRole("Administrators"))
            {
                ViewBag.Error = "Please wait until " + guild.LastScrape.AddHours(1).ToShortTimeString() + " to run again.";
                return RedirectToAction("Details", new { id = id });
            }
            if (User.IsInRole("Administrators"))
            {
                guild.LastScrape = DateTime.Now;
                db.Entry(guild).State = EntityState.Modified;
                db.SaveChanges();
            }            

            IEnumerable<Character> characters = db.Characters.ToList();
            IEnumerable<Member> members = db.Members.Where(x => x.Guild_Id == id);

            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load(guild.UrlExt);

            string guildMemberTable = doc.DocumentNode.SelectNodes("/html/body/div[3]/div[2]/div[2]/ul/li[3]/div/table/tbody")[0].InnerHtml;

            Regex regexChar = new Regex(@"\n");
            Regex regex = new Regex(@"<tr>");
            string[] substrings = regex.Split(guildMemberTable);

            List<string> listMembers = substrings.ToList();

            listMembers.Remove("\n");

            List<Member> newMembers = new List<Member>();
            List<MemberCharacter> memberCharactersAdd = new List<MemberCharacter>();

            List<Member> memberDelete = new List<Member>();

            List<string> memberExt = new List<string>();

            foreach (var member in listMembers)
            {
                Member guildMember = new Member();
                string[] memberSplit = member.Split('"');
                memberExt.Add("https://swgoh.gg" + memberSplit[1] + "collection/");
            }


            foreach (var member in members)
            {
                if (!memberExt.Any(x => x.Contains(member.UrlExt)))
                {
                    memberDelete.Add(db.Members.Where(x => x.UrlExt == member.UrlExt).FirstOrDefault());
                    db.SaveChanges();
                    continue;
                }
            }

            db.BulkDelete(memberDelete);

            foreach (var member in listMembers)
            {
                Member guildMember = new Member();
                string[] memberSplit = member.Split('"');
                string href = "https://swgoh.gg" + memberSplit[1] + "collection/";
                string name = memberSplit[2].Substring(10).Replace("</strong>\n</a>\n</td>\n<td class=", "");

                //if (members.Any(x => x.UrlExt == href))
                //{
                //    db.Members.Remove(db.Members.Where(x => x.UrlExt == href).FirstOrDefault());
                //    db.SaveChanges();
                //    continue;
                //}

                if (members.Any(x => x.UrlExt.Equals(href)))
                {
                    guildMember = members.Where(x => x.UrlExt.Equals(href)).FirstOrDefault();
                }
                else
                {
                    guildMember.Id = Guid.NewGuid();
                    guildMember.Name = name;
                    guildMember.DisplayName = HttpUtility.HtmlDecode(name);
                    guildMember.UrlExt = href;
                    guildMember.Guild_Id = guild.Id;
                    newMembers.Add(guildMember);
                }

                List<MemberCharacter> memberCharacters = db.MemberCharacters.Where(x => x.Member_Id.Equals(guildMember.Id)).ToList();

                HtmlWeb webMember = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument docMember = web.Load(href);
                string charHtml = docMember.DocumentNode.SelectNodes("/html/body/div[3]/div[2]/div[2]/ul/li[3]")[0].InnerHtml;

                string[] character = regexChar.Split(charHtml);
                List<string> listCharacters = character.ToList();

                listCharacters.RemoveAll(x => x.Equals(""));
                listCharacters.RemoveAll(x => x.Equals("<div class=\"row\">"));
                listCharacters.RemoveAll(x => x.Equals("</a>"));
                listCharacters.RemoveAll(x => x.Equals("</div>"));
                listCharacters.RemoveAll(x => x.Equals("</span>"));
                listCharacters.RemoveAll(x => x.Equals("<span class=\"collection-char-gp-label-value\">"));
                listCharacters.RemoveAll(x => x.Equals("<div class=\"collection-char-gp-progress\">"));
                listCharacters.RemoveAll(x => x.Equals("<div class=\"char-portrait-full-gear\"></div>"));
                listCharacters.RemoveAll(x => x.Equals("<span class=\"collection-char-gp-label-percent\">%</span>"));
                listCharacters.RemoveAll(x => x.Equals("<div class=\"collection-char-gp-label\">"));
                listCharacters.RemoveAll(x => x.Equals("<div class=\"collection-char collection-char-light-side\">"));
                listCharacters.RemoveAll(x => x.Equals("<div class=\"collection-char collection-char-dark-side\">"));
                listCharacters.RemoveAll(x => x.Equals("<div class=\"col-xs-6 col-sm-3 col-md-3 col-lg-2\">"));

                MemberCharacter newMemberCharacter = new MemberCharacter();
                newMemberCharacter.Member_Id = guildMember.Id;
                newMemberCharacter.Id = Guid.NewGuid();

                foreach (var item in listCharacters)
                {
                    if (item.Contains("<div class=\"char-portrait-full-level"))
                    {
                        newMemberCharacter.Level = Convert.ToInt16(item.Trim().Substring(38, 2).Replace("<", ""));
                    }
                    else if (item.Contains("<div class=\"char-portrait-full-gear-level\">"))
                    {
                        newMemberCharacter.Gear = item.Trim().Substring(43, 2).Replace("<", "");
                    }
                    else if (item.Contains("<div class=\"collection-char-gp\""))
                    {
                        newMemberCharacter.Power = item.Trim().Substring(83).Replace("\">", "");
                    }
                    else if (item.Contains("<div class=\"star") && !item.Contains("inactive"))
                    {
                        newMemberCharacter.Stars = Convert.ToInt16(item.Trim().Substring(21, 1));
                    }
                    else if (item.Contains("<div class=\"collection-char-name\">"))
                    {
                        string charName = Regex.Replace(item, "<.*?>", String.Empty);
                        IEnumerable<Character> charNames = characters.Where(x => x.Name.Equals(charName.Trim())).ToList();

                        if (charNames.Count() == 1)
                        {
                            newMemberCharacter.Character_Id = charNames.FirstOrDefault().Id;
                        }
                        else
                        {
                            if (charName.Contains("Fulcrum"))
                            {
                                newMemberCharacter.Character_Id = charNames.Where(x => x.Name.Equals("Ahsoka Tano (Fulcrum)")).FirstOrDefault().Id;
                            }
                            else if (charName.Equals("Ahsoka Tano") || charName.Equals("hsoka Tano"))
                            {
                                newMemberCharacter.Character_Id = charNames.Where(x => x.Name.Equals("Ahsoka Tano")).FirstOrDefault().Id;
                            }
                            else if (charName.Equals("Jawa"))
                            {
                                newMemberCharacter.Character_Id = charNames.Where(x => x.Name.Equals("Jawa")).FirstOrDefault().Id;
                            }
                            else if (charName.Equals("Han Solo"))
                            {
                                newMemberCharacter.Character_Id = charNames.Where(x => x.Name.Equals("Han Solo")).FirstOrDefault().Id;
                            }
                            else if (charName.Equals("Stormtrooper"))
                            {
                                newMemberCharacter.Character_Id = charNames.Where(x => x.Name.Equals("Stormtrooper")).FirstOrDefault().Id;
                            }
                            else
                            {
                                newMemberCharacter.Character_Id = charNames.Where(x => x.Name.Substring(1).Contains(charName)).FirstOrDefault().Id;
                            }
                        }

                        if (newMemberCharacter.Stars != null)
                        {
                            if (memberCharacters.Any(x => x.Character_Id.Equals(newMemberCharacter.Character_Id)))
                            {
                                MemberCharacter memberCharacterUpdate = memberCharacters.Where(x => x.Character_Id.Equals(newMemberCharacter.Character_Id)).FirstOrDefault();

                                memberCharacterUpdate.Level = newMemberCharacter.Level;
                                memberCharacterUpdate.Power = newMemberCharacter.Power;
                                memberCharacterUpdate.Stars = newMemberCharacter.Stars;

                                memberCharacters.Remove(memberCharacters.Where(x => x.Character_Id.Equals(newMemberCharacter.Character_Id)).FirstOrDefault());
                                memberCharacters.Add(memberCharacterUpdate);
                            }
                            else
                            {
                                memberCharactersAdd.Add(newMemberCharacter);

                            }
                        }
                        newMemberCharacter = new MemberCharacter();
                        newMemberCharacter.Member_Id = guildMember.Id;
                        newMemberCharacter.Id = Guid.NewGuid();
                    }
                }
                db.BulkUpdate(memberCharacters);
            }
            db.BulkInsert(newMembers);
            db.BulkInsert(memberCharactersAdd);

            return RedirectToAction("Details", new { id = id });
        }
    }
}
