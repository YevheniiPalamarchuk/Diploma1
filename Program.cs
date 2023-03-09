using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test1123
{
    static class Program
    {
        public static String connectionString = @"Data Source=LAPTOP-49SE81AP\SQLEXPRESS;Initial Catalog=Diploma;Integrated Security=True";
        public static String teamShort(string teamFullName)
        {
            string teamShortName = "No team info";
            switch (teamFullName)
            {
                case "Atlanta Hawks":
                    teamShortName = "ATL";
                    break;
                case "Boston Celtics":
                    teamShortName = "BOS";
                    break;
                case "Brooklyn Nets":
                    teamShortName = "BRK";
                    break;
                case "Charlotte Hornets":
                    teamShortName = "CHO";
                    break;
                case "Chicago Bulls":
                    teamShortName = "CHI";
                    break; ;
                case "Cleveland Cavaliers":
                    teamShortName = "CLE";
                    break;
                case "Dallas Mavericks":
                    teamShortName = "DAL";
                    break;
                case "Denver Nuggets":
                    teamShortName = "DEN";
                    break;
                case "Detroit Pistons":
                    teamShortName = "DET";
                    break;
                case "Golden State Warriors":
                    teamShortName = "GSW";
                    break;
                case "Houston Rockets":
                    teamShortName = "HOU";
                    break;
                case "Indiana Pacers":
                    teamShortName = "IND";
                    break;
                case "Los Angeles Clippers":
                    teamShortName = "LAC";
                    break;
                case "Los Angeles Lakers":
                    teamShortName = "LAL";
                    break;
                case "Memphis Grizzlies":
                    teamShortName = "MEM";
                    break;
                case "Miami Heat":
                    teamShortName = "MIA";
                    break;
                case "Milwaukee Bucks":
                    teamShortName = "MIL";
                    break;
                case "Minnesota Timberwolves":
                    teamShortName = "MIN";
                    break;
                case "New Orleans Pelicans":
                    teamShortName = "NOP";
                    break;
                case "New York Knicks":
                    teamShortName = "NYK";
                    break;
                case "Oklahoma City Thunder":
                    teamShortName = "OKC";
                    break;
                case "Orlando Magic":
                    teamShortName = "ORL";
                    break;
                case "Philadelphia 76ers":
                    teamShortName = "PHI";
                    break;
                case "Phoenix Suns":
                    teamShortName = "PHO";
                    break;
                case "Portland Trail Blazers":
                    teamShortName = "POR";
                    break;
                case "Sacramento Kings":
                    teamShortName = "SAC";
                    break;
                case "San Antonio Spurs":
                    teamShortName = "SAS";
                    break;
                case "Toronto Raptors":
                    teamShortName = "TOR";
                    break;
                case "Utah Jazz":
                    teamShortName = "UTA";
                    break;
                case "Washington Wizards":
                    teamShortName = "WAS";
                    break;
            }
            return teamShortName;
        }
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Mainform());
        }
    }
}
