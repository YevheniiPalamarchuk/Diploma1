using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Xml.Linq;
using HtmlAgilityPack;
using System.Net;
using System.Data.SqlClient;
using System.Globalization;

namespace test1123
{
    public partial class Database : Form
    {
        public Database()
        {
            InitializeComponent();
        }
        //public string teamShort(string teamFullName)
        //{
        //    string teamShortName = "No team info";
        //    switch (teamFullName)
        //    {
        //        case "Atlanta Hawks":
        //            teamShortName = "ATL";
        //            break;
        //        case "Boston Celtics":
        //            teamShortName = "BOS";
        //            break;
        //        case "Brooklyn Nets":
        //            teamShortName = "BRK";
        //            break;
        //        case "Charlotte Hornets":
        //            teamShortName = "CHO";
        //            break;
        //        case "Chicago Bulls":
        //            teamShortName = "CHI";
        //            break;;
        //        case "Cleveland Cavaliers":
        //            teamShortName = "CLE";
        //            break;
        //        case "Dallas Mavericks":
        //            teamShortName = "DAL";
        //            break;
        //        case "Denver Nuggets":
        //            teamShortName = "DEN";
        //            break;
        //        case "Detroit Pistons":
        //            teamShortName = "DET";
        //            break;
        //        case "Golden State Warriors":
        //            teamShortName = "GSW";
        //            break;
        //        case "Houston Rockets":
        //            teamShortName = "HOU";
        //            break;
        //        case "Indiana Pacers":
        //            teamShortName = "IND";
        //            break;
        //        case "Los Angeles Clippers":
        //            teamShortName = "LAC";
        //            break;
        //        case "Los Angeles Lakers":
        //            teamShortName = "LAL";
        //            break;
        //        case "Memphis Grizzlies":
        //            teamShortName = "MEM";
        //            break;
        //        case "Miami Heat":
        //            teamShortName = "MIA";
        //            break;
        //        case "Milwaukee Bucks":
        //            teamShortName = "MIL";
        //            break;
        //        case "Minnesota Timberwolves":
        //            teamShortName = "MIN";
        //            break;
        //        case "New Orleans Pelicans":
        //            teamShortName = "NOP";
        //            break;
        //        case "New York Knicks":
        //            teamShortName = "NYK";
        //            break;
        //        case "Oklahoma City Thunder":
        //            teamShortName = "OKC";
        //            break;
        //        case "Orlando Magic":
        //            teamShortName = "ORL";
        //            break;
        //        case "Philadelphia 76ers":
        //            teamShortName = "PHI";
        //            break;
        //        case "Phoenix Suns":
        //            teamShortName = "PHO";
        //            break;
        //        case "Portland Trail Blazers":
        //            teamShortName = "POR";
        //            break;
        //        case "Sacramento Kings":
        //            teamShortName = "SAC";
        //            break;
        //        case "San Antonio Spurs":
        //            teamShortName = "SAS";
        //            break;
        //        case "Toronto Raptors":
        //            teamShortName = "TOR";
        //            break;
        //        case "Utah Jazz":
        //            teamShortName = "UTA";
        //            break;
        //        case "Washington Wizards":
        //            teamShortName = "WAS";
        //            break;
        //    }
        //    return teamShortName;
        //}

        private void button1_MouseHover(object sender, EventArgs e)
        {
            button1.Text = "Hello";
            button1.ForeColor = Color.Red;
        }

        string connectionString = @"Data Source=LAPTOP-49SE81AP\SQLEXPRESS;Initial Catalog=Diploma;Integrated Security=True";
        
        // Teams
        private void button1_Click(object sender, EventArgs e)
        {
            var url = "https://www.basketball-reference.com/leagues/NBA_2023.html";
            var web = new HtmlWeb();
            var doc = web.Load(url);


            // Eastern Conference
            var easternNode = doc.DocumentNode.SelectSingleNode("//div[@id='all_confs_standings_E']");
            var easternTable = easternNode.SelectSingleNode(".//table[@id='confs_standings_E']");
            var easternRows = easternTable.SelectNodes(".//tbody/tr");

            string[,] tableDataEast = new string[easternRows.Count, 8];
            int jE = 0;

            foreach (var row in easternRows)
            {
                var teamName = row.SelectSingleNode(".//th[@data-stat='team_name']//a").InnerText.Trim();
                var cells = row.SelectNodes(".//td");
                tableDataEast[jE, 0] = teamName;
                listBox1.Items.Add(teamName);
                if (cells != null)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        
                        tableDataEast[jE, i+1] = cells[i].InnerText.Trim();
                        if (tableDataEast[jE, i + 1] == "&mdash;")
                        {
                            tableDataEast[jE, i + 1] = "---";
                        }
                        if (tableDataEast[jE, i + 1] == "")
                            tableDataEast[jE, i + 1] = "0.000";
                        if (tableDataEast[jE, i + 1].StartsWith("."))
                            tableDataEast[jE, i + 1] = "0" + tableDataEast[jE, i + 1];
                        listBox1.Items.Add(tableDataEast[jE, i+1].ToString());
                    }
                    jE++;
                }
            }

            // Western Conference
            var westernNode = doc.DocumentNode.SelectSingleNode("//div[@id='all_confs_standings_W']");
            var westernTable = westernNode.SelectSingleNode(".//table[@id='confs_standings_W']");
            var westernRows = westernTable.SelectNodes(".//tbody/tr");

            string[,] tableDataWest = new string[westernRows.Count, 8];
            int jW = 0;
            foreach (var row in westernRows)
            {
                var teamName = row.SelectSingleNode(".//th[@data-stat='team_name']//a").InnerText;
                var cells = row.SelectNodes(".//td");
                tableDataWest[jW, 0] = teamName;
                listBox1.Items.Add(teamName);
                if (cells != null)
                {
                    for (int i = 0; i < 7; i++)
                    {

                        tableDataWest[jW, i + 1] = cells[i].InnerText.Trim();
                        if (tableDataWest[jW, i + 1] == "&mdash;")
                        {
                            tableDataWest[jW, i + 1] = "---";
                        }
                        if (tableDataWest[jW, i + 1] == "")
                            tableDataWest[jW, i + 1] = "0.000";
                        if (tableDataWest[jW, i + 1].StartsWith("."))
                            tableDataWest[jW, i + 1] = "0" + tableDataWest[jW, i + 1];
                        listBox1.Items.Add(tableDataWest[jW, i + 1].ToString());
                    }
                    jW++;
                }
            }
            // Eastern Conference teams    
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string queryClear = "DELETE FROM Eastern_Conference";
                using (SqlCommand command = new SqlCommand(queryClear, connection))
                {
                    command.ExecuteNonQuery();
                }

                string query = "INSERT INTO Eastern_Conference (Name, W, L, [WL/P], GB, [PS/G], [PA/G], SRS) VALUES (@Value1, @Value2, @Value3, @Value4, @Value5, @Value6, @Value7, @Value8)";

                // Create a loop to iterate over each row in the 2D array
                for (int row = 0; row < tableDataEast.GetLength(0); row++)
                {
                    // Retrieve the data from each cell in the current row
                    string value1 = tableDataEast[row, 0];
                    string value2 = tableDataEast[row, 1];
                    string value3 = tableDataEast[row, 2];
                    string value4 = tableDataEast[row, 3];
                    string value5 = tableDataEast[row, 4];
                    string value6 = tableDataEast[row, 5];
                    string value7 = tableDataEast[row, 6];
                    string value8 = tableDataEast[row, 7];                    

                    // Create a new SqlCommand object with the SQL query and the current row's data
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (String.IsNullOrEmpty(value1))
                            command.Parameters.AddWithValue("@Value1", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value1", value1);

                        if (String.IsNullOrEmpty(value2))
                            command.Parameters.AddWithValue("@Value2", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value2", value2);

                        if (String.IsNullOrEmpty(value3))
                            command.Parameters.AddWithValue("@Value3", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value3", value3);

                        if (String.IsNullOrEmpty(value4))
                            command.Parameters.AddWithValue("@Value4", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value4", value4);

                        if (String.IsNullOrEmpty(value5))
                            command.Parameters.AddWithValue("@Value5", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value5", value5);

                        if (String.IsNullOrEmpty(value6))
                            command.Parameters.AddWithValue("@Value6", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value6", value6);

                        if (String.IsNullOrEmpty(value7))
                            command.Parameters.AddWithValue("@Value7", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value7", value7);

                        if (String.IsNullOrEmpty(value8))
                            command.Parameters.AddWithValue("@Value8", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value8", value8);
                      

                        // Execute the SQL query for the current row of data
                        command.ExecuteNonQuery();
                    }
                }


                string queryDeleteNulls = "DELETE FROM Eastern_Conference WHERE Name IS NULL";
                using (SqlCommand command = new SqlCommand(queryDeleteNulls, connection))
                {
                    command.ExecuteNonQuery();
                }
            }

            // Western Conference teams    
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open(); 

                string queryClear = "DELETE FROM Western_Conference";
                using (SqlCommand command = new SqlCommand(queryClear, connection))
                {
                    command.ExecuteNonQuery();
                }

                string query = "INSERT INTO Western_Conference (Name, W, L, [WL/P], GB, [PS/G], [PA/G], SRS) VALUES (@Value1, @Value2, @Value3, @Value4, @Value5, @Value6, @Value7, @Value8)";

                // Create a loop to iterate over each row in the 2D array
                for (int row = 0; row < tableDataWest.GetLength(0); row++)
                {
                    // Retrieve the data from each cell in the current row
                    string value1 = tableDataWest[row, 0];
                    string value2 = tableDataWest[row, 1];
                    string value3 = tableDataWest[row, 2];
                    string value4 = tableDataWest[row, 3];
                    string value5 = tableDataWest[row, 4];
                    string value6 = tableDataWest[row, 5];
                    string value7 = tableDataWest[row, 6];
                    string value8 = tableDataWest[row, 7];

                    // Create a new SqlCommand object with the SQL query and the current row's data
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (String.IsNullOrEmpty(value1))
                            command.Parameters.AddWithValue("@Value1", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value1", value1);

                        if (String.IsNullOrEmpty(value2))
                            command.Parameters.AddWithValue("@Value2", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value2", value2);

                        if (String.IsNullOrEmpty(value3))
                            command.Parameters.AddWithValue("@Value3", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value3", value3);

                        if (String.IsNullOrEmpty(value4))
                            command.Parameters.AddWithValue("@Value4", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value4", value4);

                        if (String.IsNullOrEmpty(value5))
                            command.Parameters.AddWithValue("@Value5", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value5", value5);

                        if (String.IsNullOrEmpty(value6))
                            command.Parameters.AddWithValue("@Value6", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value6", value6);

                        if (String.IsNullOrEmpty(value7))
                            command.Parameters.AddWithValue("@Value7", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value7", value7);

                        if (String.IsNullOrEmpty(value8))
                            command.Parameters.AddWithValue("@Value8", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value8", value8);


                        // Execute the SQL query for the current row of data
                        command.ExecuteNonQuery();
                    }
                }


                string queryDeleteNulls = "DELETE FROM Western_Conference WHERE Name IS NULL";
                using (SqlCommand command = new SqlCommand(queryDeleteNulls, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        // Schedule
        private void button2_Click(object sender, EventArgs e)
        {
            var web = new HtmlWeb();
            //October
            var urlOct = "https://www.basketball-reference.com/leagues/NBA_2023_games-october.html";
            var docOct = web.Load(urlOct);
            //November
            var urlNov = "https://www.basketball-reference.com/leagues/NBA_2023_games-november.html";
            var docNov = web.Load(urlNov);
            //December
            var urlDec = "https://www.basketball-reference.com/leagues/NBA_2023_games-december.html";
            var docDec = web.Load(urlDec);
            //January
            var urlJan = "https://www.basketball-reference.com/leagues/NBA_2023_games-january.html";
            var docJan = web.Load(urlJan);
            //February
            var urlFeb = "https://www.basketball-reference.com/leagues/NBA_2023_games-february.html";
            var docFeb = web.Load(urlFeb);
            //March
            var urlMar = "https://www.basketball-reference.com/leagues/NBA_2023_games-march.html";
            var docMar = web.Load(urlMar);
            //April
            var urlApr = "https://www.basketball-reference.com/leagues/NBA_2023_games-april.html";
            var docApr = web.Load(urlApr);

            var tableOct = docOct.DocumentNode.SelectSingleNode("//table[@id='schedule']"); 
            var rowsOct = tableOct.SelectNodes(".//tbody/tr");
            var tableNov = docNov.DocumentNode.SelectSingleNode("//table[@id='schedule']");
            var rowsNov = tableNov.SelectNodes(".//tbody/tr");
            var tableDec = docDec.DocumentNode.SelectSingleNode("//table[@id='schedule']");
            var rowsDec = tableDec.SelectNodes(".//tbody/tr");
            var tableJan = docJan.DocumentNode.SelectSingleNode("//table[@id='schedule']");
            var rowsJan = tableJan.SelectNodes(".//tbody/tr");
            var tableFeb = docFeb.DocumentNode.SelectSingleNode("//table[@id='schedule']");
            var rowsFeb = tableFeb.SelectNodes(".//tbody/tr");
            var tableMar = docMar.DocumentNode.SelectSingleNode("//table[@id='schedule']");
            var rowsMar = tableMar.SelectNodes(".//tbody/tr");
            var tableApr = docApr.DocumentNode.SelectSingleNode("//table[@id='schedule']");
            var rowsApr = tableApr.SelectNodes(".//tbody/tr");

            int j = 0;
            int rowsAll = rowsOct.Count + rowsNov.Count + rowsDec.Count + rowsJan.Count + rowsFeb.Count + rowsMar.Count + rowsApr.Count;
            string[,] tableData= new string[rowsAll, 4];
            //string[,] tableDataOct = new string[rowsOct.Count, 4];
            //string[,] tableDataNov = new string[rowsNov.Count, 4];
            //string[,] tableDataDec = new string[rowsDec.Count, 4];
            //string[,] tableDataJan = new string[rowsJan.Count, 4];
            //string[,] tableDataFeb = new string[rowsFeb.Count, 4];
            //string[,] tableDataMar = new string[rowsMar.Count, 4];
            //string[,] tableDataApr = new string[rowsApr.Count, 4];
            foreach (var row in rowsOct)
            {
                var date = row.SelectSingleNode(".//th[@data-stat='date_game']//a").InnerText.Trim();
                var cells = row.SelectNodes(".//td");
                var time = cells[0].InnerText.Trim();
                var awayTeam = cells[1].InnerText.Trim();
                var homeTeam = cells[3].InnerText.Trim();
                var location = cells[8].InnerText.Trim();
                var testDate = date + ", " + time + "m";
                DateTime dateSql = DateTime.ParseExact(testDate, "ddd, MMM d, yyyy, h:mmtt", CultureInfo.InvariantCulture);
                string formattedDate = dateSql.ToString("yyyy-MM-dd HH:mm:ss.fff");
                //DateTime dateForSQL = dateSql;
                tableData[j, 0] = formattedDate;
                tableData[j, 1] = location;
                tableData[j, 2] = awayTeam;
                tableData[j, 3] = homeTeam;
                if (location == "")
                {
                    tableData[j, 1] = "No arena info";
                }
                //tableDataOct[j, 0] = testDate;
                //tableDataOct[j, 1] = location;
                //tableDataOct[j, 2] = awayTeam;
                //tableDataOct[j, 3] = homeTeam;           
                for (int i = 0; i < 4; i++)
                {
                    listBox1.Items.Add(tableData[j, i]);
                }
                j++;
            }
            foreach (var row in rowsNov)
            {
                var date = row.SelectSingleNode(".//th[@data-stat='date_game']//a").InnerText.Trim();
                var cells = row.SelectNodes(".//td");
                var time = cells[0].InnerText.Trim();
                var awayTeam = cells[1].InnerText.Trim();
                var homeTeam = cells[3].InnerText.Trim();
                var location = cells[8].InnerText.Trim();
                var testDate = date + ", " + time + "m";
                DateTime dateSql = DateTime.ParseExact(testDate, "ddd, MMM d, yyyy, h:mmtt", CultureInfo.InvariantCulture);
                string formattedDate = dateSql.ToString("yyyy-MM-dd HH:mm:ss.fff");
                //DateTime dateForSQL = dateSql;
                tableData[j, 0] = formattedDate;
                tableData[j, 1] = location;
                tableData[j, 2] = awayTeam;
                tableData[j, 3] = homeTeam;
                if (location == "")
                {
                    tableData[j, 1] = "No arena info";
                }
                for (int i = 0; i < 4; i++)
                {
                    listBox1.Items.Add(tableData[j, i]);
                }
                j++;
            }
            foreach (var row in rowsDec)
            {
                var date = row.SelectSingleNode(".//th[@data-stat='date_game']//a").InnerText.Trim();
                var cells = row.SelectNodes(".//td");
                var time = cells[0].InnerText.Trim();
                var awayTeam = cells[1].InnerText.Trim();
                var homeTeam = cells[3].InnerText.Trim();
                var location = cells[8].InnerText.Trim();
                var testDate = date + ", " + time + "m";
                DateTime dateSql = DateTime.ParseExact(testDate, "ddd, MMM d, yyyy, h:mmtt", CultureInfo.InvariantCulture);
                string formattedDate = dateSql.ToString("yyyy-MM-dd HH:mm:ss.fff");
                //DateTime dateForSQL = dateSql;
                tableData[j, 0] = formattedDate;
                tableData[j, 1] = location;
                tableData[j, 2] = awayTeam;
                tableData[j, 3] = homeTeam;
                if (location == "")
                {
                    tableData[j, 1] = "No arena info";
                }
                for (int i = 0; i < 4; i++)
                {
                    listBox1.Items.Add(tableData[j, i]);
                }
                j++;
            }
            foreach (var row in rowsJan)
            {
                var date = row.SelectSingleNode(".//th[@data-stat='date_game']//a").InnerText.Trim();
                var cells = row.SelectNodes(".//td");
                var time = cells[0].InnerText.Trim();
                var awayTeam = cells[1].InnerText.Trim();
                var homeTeam = cells[3].InnerText.Trim();
                var location = cells[8].InnerText.Trim();
                var testDate = date + ", " + time + "m";
                DateTime dateSql = DateTime.ParseExact(testDate, "ddd, MMM d, yyyy, h:mmtt", CultureInfo.InvariantCulture);
                string formattedDate = dateSql.ToString("yyyy-MM-dd HH:mm:ss.fff");
                //DateTime dateForSQL = dateSql;
                tableData[j, 0] = formattedDate;
                tableData[j, 1] = location;
                tableData[j, 2] = awayTeam;
                tableData[j, 3] = homeTeam;
                if (location == "")
                {
                    tableData[j, 1] = "No arena info";
                }
                for (int i = 0; i < 4; i++)
                {
                    listBox1.Items.Add(tableData[j, i]);
                }
                j++;
            }
            foreach (var row in rowsFeb)
            {
                var date = row.SelectSingleNode(".//th[@data-stat='date_game']//a").InnerText.Trim();
                var cells = row.SelectNodes(".//td");
                var time = cells[0].InnerText.Trim();
                var awayTeam = cells[1].InnerText.Trim();
                var homeTeam = cells[3].InnerText.Trim();
                var location = cells[8].InnerText.Trim();
                var testDate = date + ", " + time + "m";
                DateTime dateSql = DateTime.ParseExact(testDate, "ddd, MMM d, yyyy, h:mmtt", CultureInfo.InvariantCulture);
                string formattedDate = dateSql.ToString("yyyy-MM-dd HH:mm:ss.fff");
                //DateTime dateForSQL = dateSql;
                tableData[j, 0] = formattedDate;
                tableData[j, 1] = location;
                tableData[j, 2] = awayTeam;
                tableData[j, 3] = homeTeam;
                if (location == "")
                {
                    tableData[j, 1] = "No arena info";
                }
                for (int i = 0; i < 4; i++)
                {
                    listBox1.Items.Add(tableData[j, i]);
                }
                j++;
            }
            foreach (var row in rowsMar)
            {
                var date = row.SelectSingleNode(".//th[@data-stat='date_game']//a").InnerText.Trim();
                var cells = row.SelectNodes(".//td");
                var time = cells[0].InnerText.Trim();
                var awayTeam = cells[1].InnerText.Trim();
                var homeTeam = cells[3].InnerText.Trim();
                var location = cells[8].InnerText.Trim();
                var testDate = date + ", " + time + "m";
                DateTime dateSql = DateTime.ParseExact(testDate, "ddd, MMM d, yyyy, h:mmtt", CultureInfo.InvariantCulture);
                string formattedDate = dateSql.ToString("yyyy-MM-dd HH:mm:ss.fff");
                //DateTime dateForSQL = dateSql;
                tableData[j, 0] = formattedDate;
                tableData[j, 1] = location;
                tableData[j, 2] = awayTeam;
                tableData[j, 3] = homeTeam;
                if (location == "")
                {
                    tableData[j, 1] = "No arena info";
                }
                for (int i = 0; i < 4; i++)
                {
                    listBox1.Items.Add(tableData[j, i]);
                }
                j++;
            }
            foreach (var row in rowsApr)
            {
                var date = row.SelectSingleNode(".//th[@data-stat='date_game']//a").InnerText.Trim();
                var cells = row.SelectNodes(".//td");
                var time = cells[0].InnerText.Trim();
                var awayTeam = cells[1].InnerText.Trim();
                var homeTeam = cells[3].InnerText.Trim();
                var location = cells[8].InnerText.Trim();
                var testDate = date + ", " + time + "m";
                DateTime dateSql = DateTime.ParseExact(testDate, "ddd, MMM d, yyyy, h:mmtt", CultureInfo.InvariantCulture);
                string formattedDate = dateSql.ToString("yyyy-MM-dd HH:mm:ss.fff");
                //DateTime dateForSQL = dateSql;
                tableData[j, 0] = formattedDate;
                tableData[j, 1] = location;
                tableData[j, 2] = awayTeam;
                tableData[j, 3] = homeTeam;
                if (location == "")
                {
                    tableData[j, 1] = "No arena info";
                }
                for (int i = 0; i < 4; i++)
                {
                    listBox1.Items.Add(tableData[j, i]);
                }
                j++;
            }
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string queryClear = "DELETE FROM Schedule";
                using (SqlCommand command = new SqlCommand(queryClear, connection))
                {
                    command.ExecuteNonQuery();
                }
                string query = "INSERT INTO Schedule (date, arena, team1, team2) VALUES (@Value1, @Value2, @Value3, @Value4)";
                for (int row = 0; row < tableData.GetLength(0); row++)
                {
                    // Retrieve the data from each cell in the current row
                    string value1 = tableData[row, 0];
                    string value2 = tableData[row, 1];
                    string value3 = tableData[row, 2];
                    string value4 = tableData[row, 3];

                    // Create a new SqlCommand object with the SQL query and the current row's data
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (String.IsNullOrEmpty(value1))
                            command.Parameters.AddWithValue("@Value1", DBNull.Value);
                        else
                        {
                            command.Parameters.Add("@Value1", SqlDbType.DateTime);
                            command.Parameters["@Value1"].Value = Convert.ToDateTime(value1);
                        }

                        if (String.IsNullOrEmpty(value2))
                            command.Parameters.AddWithValue("@Value2", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value2", value2);

                        if (String.IsNullOrEmpty(value3))
                            command.Parameters.AddWithValue("@Value3", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value3", value3);

                        if (String.IsNullOrEmpty(value4))
                            command.Parameters.AddWithValue("@Value4", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value4", value4);
                      


                        // Execute the SQL query for the current row of data
                        command.ExecuteNonQuery();


                    }
                }

                string queryDeleteNulls = "DELETE FROM Schedule WHERE date IS NULL";
                using (SqlCommand command = new SqlCommand(queryDeleteNulls, connection))
                {
                    command.ExecuteNonQuery();
                }
            }    
        }

        // Players
        private void button3_Click(object sender, EventArgs e)
        {
            var url = "https://www.basketball-reference.com/leagues/NBA_2023_per_game.html";
            var web = new HtmlWeb();
            var doc = web.Load(url);

            var table = doc.DocumentNode.SelectSingleNode("//table[@id='per_game_stats']");
            var rows = table.SelectNodes(".//tr");

            string[,] tableData = new string[rows.Count, 29];

            int j = 0;
            foreach (var row in rows)
            {
                var cells = row.SelectNodes(".//td");
                if (cells != null)
                {
                    for (int i = 0; i < 29; i++)
                    {
                        tableData[j, i] = cells[i].InnerText.Trim();
                        if (tableData[j, i] == "")
                            tableData[j, i] = "0.000";
                        if (tableData[j, i].StartsWith("."))
                            tableData[j, i] = "0" + tableData[j, i];
                    }
                    j++;
                }
            }

            //string connectionString = @"Data Source=LAPTOP-49SE81AP\SQLEXPRESS;Initial Catalog=Diploma;Integrated Security=True";

            // Establish a connection to the SQL database
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Define the SQL query to insert data into the database               

                string queryClear = "DELETE FROM Nba_Stats2";
                using (SqlCommand command = new SqlCommand(queryClear, connection))
                {
                    command.ExecuteNonQuery();
                }

                string query = "INSERT INTO NBA_Stats2 (Player, Pos, Age, Tm, G, GS, MP, FG, FGA, FGP, [3P], [3PA], [3PP], [2P], [2PA], [2PP], eFGP, FT, FTA, FTP, ORB, DRB, TRB, AST, STL, BLK, TOV, PF, PTS) VALUES (@Value1, @Value2, @Value3, @Value4, @Value5, @Value6, @Value7, @Value8, @Value9, @Value10, @Value11, @Value12, @Value13, @Value14, @Value15, @Value16, @Value17, @Value18, @Value19, @Value20, @Value21, @Value22, @Value23, @Value24, @Value25, @Value26, @Value27, @Value28, @Value29)";

                // Create a loop to iterate over each row in the 2D array
                for (int row = 0; row < tableData.GetLength(0); row++)
                {
                    // Retrieve the data from each cell in the current row
                    string value1 = tableData[row, 0];
                    string value2 = tableData[row, 1];
                    string value3 = tableData[row, 2];
                    string value4 = tableData[row, 3];
                    string value5 = tableData[row, 4];
                    string value6 = tableData[row, 5];
                    string value7 = tableData[row, 6];
                    string value8 = tableData[row, 7];
                    string value9 = tableData[row, 8];
                    string value10 = tableData[row, 9];
                    string value11 = tableData[row, 10];
                    string value12 = tableData[row, 11];
                    string value13 = tableData[row, 12];
                    string value14 = tableData[row, 13];
                    string value15 = tableData[row, 14];
                    string value16 = tableData[row, 15];
                    string value17 = tableData[row, 16];
                    string value18 = tableData[row, 17];
                    string value19 = tableData[row, 18];
                    string value20 = tableData[row, 19];
                    string value21 = tableData[row, 20];
                    string value22 = tableData[row, 21];
                    string value23 = tableData[row, 22];
                    string value24 = tableData[row, 23];
                    string value25 = tableData[row, 24];
                    string value26 = tableData[row, 25];
                    string value27 = tableData[row, 26];
                    string value28 = tableData[row, 27];
                    string value29 = tableData[row, 28];

                    // Create a new SqlCommand object with the SQL query and the current row's data
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (String.IsNullOrEmpty(value1))
                            command.Parameters.AddWithValue("@Value1", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value1", value1);

                        if (String.IsNullOrEmpty(value2))
                            command.Parameters.AddWithValue("@Value2", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value2", value2);

                        if (String.IsNullOrEmpty(value3))
                            command.Parameters.AddWithValue("@Value3", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value3", value3);

                        if (String.IsNullOrEmpty(value4))
                            command.Parameters.AddWithValue("@Value4", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value4", value4);

                        if (String.IsNullOrEmpty(value5))
                            command.Parameters.AddWithValue("@Value5", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value5", value5);

                        if (String.IsNullOrEmpty(value6))
                            command.Parameters.AddWithValue("@Value6", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value6", value6);

                        if (String.IsNullOrEmpty(value7))
                            command.Parameters.AddWithValue("@Value7", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value7", value7);

                        if (String.IsNullOrEmpty(value8))
                            command.Parameters.AddWithValue("@Value8", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value8", value8);

                        if (String.IsNullOrEmpty(value9))
                            command.Parameters.AddWithValue("@Value9", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value9", value9);

                        if (String.IsNullOrEmpty(value10))
                            command.Parameters.AddWithValue("@Value10", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value10", value10);

                        if (String.IsNullOrEmpty(value11))
                            command.Parameters.AddWithValue("@Value11", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value11", value11);

                        if (String.IsNullOrEmpty(value12))
                            command.Parameters.AddWithValue("@Value12", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value12", value12);

                        if (String.IsNullOrEmpty(value13))
                            command.Parameters.AddWithValue("@Value13", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value13", value13);

                        if (String.IsNullOrEmpty(value14))
                            command.Parameters.AddWithValue("@Value14", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value14", value14);

                        if (String.IsNullOrEmpty(value15))
                            command.Parameters.AddWithValue("@Value15", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value15", value15);

                        if (String.IsNullOrEmpty(value16))
                            command.Parameters.AddWithValue("@Value16", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value16", value16);

                        if (String.IsNullOrEmpty(value17))
                            command.Parameters.AddWithValue("@Value17", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value17", value17);

                        if (String.IsNullOrEmpty(value18))
                            command.Parameters.AddWithValue("@Value18", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value18", value18);

                        if (String.IsNullOrEmpty(value19))
                            command.Parameters.AddWithValue("@Value19", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value19", value19);

                        if (String.IsNullOrEmpty(value20))
                            command.Parameters.AddWithValue("@Value20", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value20", value20);

                        if (String.IsNullOrEmpty(value21))
                            command.Parameters.AddWithValue("@Value21", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value21", value21);

                        if (String.IsNullOrEmpty(value22))
                            command.Parameters.AddWithValue("@Value22", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value22", value22);

                        if (String.IsNullOrEmpty(value23))
                            command.Parameters.AddWithValue("@Value23", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value23", value23);

                        if (String.IsNullOrEmpty(value24))
                            command.Parameters.AddWithValue("@Value24", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value24", value24);

                        if (String.IsNullOrEmpty(value25))
                            command.Parameters.AddWithValue("@Value25", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value25", value25);

                        if (String.IsNullOrEmpty(value26))
                            command.Parameters.AddWithValue("@Value26", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value26", value26);

                        if (String.IsNullOrEmpty(value27))
                            command.Parameters.AddWithValue("@Value27", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value27", value27);

                        if (String.IsNullOrEmpty(value28))
                            command.Parameters.AddWithValue("@Value28", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value28", value28);

                        if (String.IsNullOrEmpty(value29))
                            command.Parameters.AddWithValue("@Value29", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value29", value29);

                        // Execute the SQL query for the current row of data
                        command.ExecuteNonQuery();
                    }
                }


                string queryDeleteNulls = "DELETE FROM NBA_Stats2 WHERE Player IS NULL";
                using (SqlCommand command = new SqlCommand(queryDeleteNulls, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var url = "https://www.basketball-reference.com/teams/";
            var web = new HtmlWeb();
            var doc = web.Load(url);

            var table = doc.DocumentNode.SelectSingleNode("//table[@id='teams_active']");
            var rows = table.SelectNodes(".//tr[@class='full_table']");

            string[,] tableData = new string[rows.Count, 10];

            int j = 0;
            foreach (var row in rows)
            {
                var teamName = row.SelectSingleNode(".//th[@data-stat='franch_name']//a").InnerText;
                var cells = row.SelectNodes(".//td");
                tableData[j, 0] = teamName;
                tableData[j, 1] = Program.teamShort(teamName);
                listBox1.Items.Add(tableData[j, 0]);
                listBox1.Items.Add(tableData[j, 1]);
                if (cells != null)
                {
                    for (int i = 4; i < 12; i++)
                    {
                        tableData[j, i -2] = cells[i].InnerText.Trim();             
                        if (tableData[j, i - 2] == "")
                            tableData[j, i - 2] = "0.000";
                        if (tableData[j, i - 2].StartsWith("."))
                            tableData[j, i - 2] = "0" + tableData[j, i - 2];
                        listBox1.Items.Add(tableData[j, i - 2].ToString());
                    }
                    j++;
                }
            }

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string queryClear = "DELETE FROM Teams";
                using (SqlCommand command = new SqlCommand(queryClear, connection))
                {
                    command.ExecuteNonQuery();
                }

                string query = "INSERT INTO Teams (Name, Shortname, G, W, L, [W/LP], Plyfs, Div, Conf, Champ) VALUES (@Value1, @Value2, @Value3, @Value4, @Value5, @Value6, @Value7, @Value8, @Value9, @Value10)";

                // Create a loop to iterate over each row in the 2D array
                for (int row = 0; row < tableData.GetLength(0); row++)
                {
                    // Retrieve the data from each cell in the current row
                    string value1 = tableData[row, 0];
                    string value2 = tableData[row, 1];
                    string value3 = tableData[row, 2];
                    string value4 = tableData[row, 3];
                    string value5 = tableData[row, 4];
                    string value6 = tableData[row, 5];
                    string value7 = tableData[row, 6];
                    string value8 = tableData[row, 7];
                    string value9 = tableData[row, 8];
                    string value10 = tableData[row, 9];
                    //string value10 = tableData[row, 9];
                    //string value11 = tableData[row, 10];
                    //string value12 = tableData[row, 11];
                    //string value13 = tableData[row, 12];

                    // Create a new SqlCommand object with the SQL query and the current row's data
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (String.IsNullOrEmpty(value1))
                            command.Parameters.AddWithValue("@Value1", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value1", value1);

                        if (String.IsNullOrEmpty(value2))
                            command.Parameters.AddWithValue("@Value2", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value2", value2);

                        if (String.IsNullOrEmpty(value3))
                            command.Parameters.AddWithValue("@Value3", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value3", value3);

                        if (String.IsNullOrEmpty(value4))
                            command.Parameters.AddWithValue("@Value4", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value4", value4);

                        if (String.IsNullOrEmpty(value5))
                            command.Parameters.AddWithValue("@Value5", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value5", value5);

                        if (String.IsNullOrEmpty(value6))
                            command.Parameters.AddWithValue("@Value6", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value6", value6);

                        if (String.IsNullOrEmpty(value7))
                            command.Parameters.AddWithValue("@Value7", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value7", value7);

                        if (String.IsNullOrEmpty(value8))
                            command.Parameters.AddWithValue("@Value8", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value8", value8);
                        if (String.IsNullOrEmpty(value9))
                            command.Parameters.AddWithValue("@Value9", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value9", value9);
                        if (String.IsNullOrEmpty(value10))
                            command.Parameters.AddWithValue("@Value10", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value10", value10);


                        // Execute the SQL query for the current row of data
                        command.ExecuteNonQuery();
                    }
                }


                string queryDeleteNulls = "DELETE FROM Teams WHERE Name IS NULL";
                using (SqlCommand command = new SqlCommand(queryDeleteNulls, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
