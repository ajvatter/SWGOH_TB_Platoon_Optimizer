using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
                string Url = "http://swgoh.gg/g/17850/alderaanian-wolfcats/";
                HtmlWeb web = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument doc = web.Load(Url);

            string guildeMemberTable = doc.DocumentNode.SelectNodes("/html/body/div[3]/div[2]/div[2]/ul/li[3]/div/table/tbody")[0].InnerHtml;

            Regex regex = new Regex(@"<tr>");
            string[] substrings = regex.Split(guildeMemberTable);

            List<string> listMembers = substrings.ToList();

            listMembers.Remove("\n");

            DataTable dtGuildMembers = new DataTable();
            dtGuildMembers.Columns.Add("URL");
            dtGuildMembers.Columns.Add("CharacterContent");

            foreach (var member in listMembers)
            {                
                string href = "https://swgoh.gg" + member.Split('"')[1] + "collection/";
                DataRow row = dtGuildMembers.NewRow();
                row[0] = href;
                
                HtmlWeb webMember = new HtmlWeb();
                HtmlAgilityPack.HtmlDocument docMember = web.Load(href);
                row[1] = docMember.DocumentNode.SelectNodes("/html/body/div[3]/div[2]/div[2]/ul/li[3]")[0].InnerHtml;

                dtGuildMembers.Rows.Add(row);
            }

            dgvMembers.DataSource = dtGuildMembers;
        }
    }
}
