using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test1123
{
    public partial class Mainform : Form
    {
        public Mainform()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Database formDatabase = new Database();
            formDatabase.ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Matches formTeaminfo = new Matches();
            formTeaminfo.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PlayerStat playerstat = new PlayerStat();
            playerstat.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TeamStat teamstat = new TeamStat();
            teamstat.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ConferenceStat conferencestat = new ConferenceStat();
            conferencestat.ShowDialog();
        }
    }
}
