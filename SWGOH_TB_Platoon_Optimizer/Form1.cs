using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SWGOH_TB_Platoon_Optimizer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=SWGOH;Data Source=(LocalDb)\\V11.0";
            DataTable dtCharacters = DbProvider.FillDataTable(connectionString, "Select * FROM Characters");
            DataTable dtMembers = DbProvider.FillDataTable(connectionString, "Select * FROM GuildMembers");

            string Url = "http://swgoh.gg/g/17850/alderaanian-wolfcats/";
            HtmlWeb web = new HtmlWeb();
            HtmlAgilityPack.HtmlDocument doc = web.Load(Url);

            string guildeMemberTable = doc.DocumentNode.SelectNodes("/html/body/div[3]/div[2]/div[2]/ul/li[3]/div/table/tbody")[0].InnerHtml;

            Regex regexChar = new Regex(@"\n");
            Regex regex = new Regex(@"<tr>");
            string[] substrings = regex.Split(guildeMemberTable);

            List<string> listMembers = substrings.ToList();

            listMembers.Remove("\n");

            foreach (var member in listMembers)
            {
                Guid memberGuid = Guid.NewGuid();
                string[] memberSplit = member.Split('"');
                string href = "https://swgoh.gg" + memberSplit[1] + "collection/";
                string name = memberSplit[2].Substring(10).Replace("</strong>\n</a>\n</td>\n<td class=", "");

                if (dtMembers.AsEnumerable().Any(x => x.Field<String>("Name").Equals(name)))
                {
                    memberGuid = dtMembers.AsEnumerable().Where(x => x.Field<String>("Name").Equals(name)).FirstOrDefault().Field<Guid>("Id");
                }
                else
                {
                    DbProvider.EXEC(connectionString, "INSERT INTO [dbo].[GuildMembers] ([Id], [Name]) VALUES ('" + memberGuid + "','" + name + "')");
                }

                DataTable dtMembersChars = DbProvider.FillDataTable(connectionString, "Select * FROM GuildMember_MemberCharacter_Mapping where GuildMember_Id = '" + memberGuid + "'");

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

                MemberCharacter newMemberCharacter = new MemberCharacter();

                foreach (var item in listCharacters)
                {
                    if (item == "<div class=\"col-xs-6 col-sm-3 col-md-3 col-lg-2\">")
                    {
                        //newMemberCharacter = new MemberCharacter();
                    }
                    else if (item.Contains("<div class=\"char-portrait-full-level"))
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
                        var index = item.IndexOf("w\">");
                        var charName = item.Trim().Substring(index + 3);
                        charName = charName.Replace("</a></div>", "");
                        DataRow[] charNames = dtCharacters.AsEnumerable().Where(x => x.Field<String>("Name").Contains(charName)).ToArray();

                        if (charNames.Count() == 1)
                        {
                            newMemberCharacter.CharacterId = charNames.FirstOrDefault().Field<Guid>("Id");
                        }
                        else
                        {
                            //do something
                            if (charName.Contains("Fulcrum"))
                            {
                                newMemberCharacter.CharacterId = charNames.AsEnumerable().Where(x => x.Field<String>("Name").Equals("Ahsoka Tano (Fulcrum)")).FirstOrDefault().Field<Guid>("Id");
                            }
                            else if(charName.Equals("Ahsoka Tano"))
                            {
                                newMemberCharacter.CharacterId = charNames.AsEnumerable().Where(x => x.Field<String>("Name").Equals("Ahsoka Tano")).FirstOrDefault().Field<Guid>("Id");
                            }
                            else if (charName.Equals("Jawa"))
                            {
                                newMemberCharacter.CharacterId = charNames.AsEnumerable().Where(x => x.Field<String>("Name").Equals("Jawa")).FirstOrDefault().Field<Guid>("Id");
                            }
                            else if (charName.Equals("Han Solo"))
                            {
                                newMemberCharacter.CharacterId = charNames.AsEnumerable().Where(x => x.Field<String>("Name").Equals("Han Solo")).FirstOrDefault().Field<Guid>("Id");
                            }
                            else if (charName.Equals("StormTrooper"))
                            {
                                newMemberCharacter.CharacterId = charNames.AsEnumerable().Where(x => x.Field<String>("Name").Equals("Stormtrooper")).FirstOrDefault().Field<Guid>("Id");
                            }
                        }

                        if (newMemberCharacter.Stars != null)
                        {
                            if (dtMembersChars.AsEnumerable().Any(x => x.Field<Guid>("MemberCharacter_Id").Equals(newMemberCharacter.CharacterId)))
                            {
                                //update
                                DbProvider.EXEC(connectionString, "UPDATE [dbo].[MemberCharacters] SET [Level] = " + newMemberCharacter.Level + ",[Gear] = '" + newMemberCharacter.Gear + "',[Stars] = " + newMemberCharacter.Stars + ",[Power] = '" + newMemberCharacter.Power + "' WHERE Id = '" + newMemberCharacter.CharacterId + "'");
                            }
                            else
                            {
                                //insert
                                DbProvider.EXEC(connectionString, "INSERT INTO [dbo].[MemberCharacters] ([Id],[Level],[Gear],[Stars],[Power],[Character_Id]) VALUES('" + newMemberCharacter.Id + "'," + newMemberCharacter.Level + ",'" + newMemberCharacter.Gear + "'," + newMemberCharacter.Stars + ",'" + newMemberCharacter.Power + "','" + newMemberCharacter.CharacterId + "')");
                                DbProvider.EXEC(connectionString, "INSERT INTO [dbo].[GuildMember_MemberCharacter_Mapping] ([Id],[GuildMember_Id],[MemberCharacter_Id]) VALUES ('" + Guid.NewGuid() + "','" + memberGuid + "','" + newMemberCharacter.Id + "')");
                            }
                        }

                        newMemberCharacter = new MemberCharacter();
                    }
                }
            }
            Cursor.Current = Cursors.Default;
        }
    }
}
