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
    public partial class TeamStat : Form
    {
        public string Teamname { get; set; }
        public TeamStat()
        {
            InitializeComponent();
        }
        public void dgvSize()
        {
            dgv1.Columns[0].Width = 200;
            dgv1.Columns[1].Width = 100;
            for (int i = 2; i < 10; i++)
                dgv1.Columns[i].Width = 50;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void TeamStat_Load(object sender, EventArgs e)
        {
            string txtTeam = txtTeamname.Text;
            using (SqlConnection connection = new SqlConnection(Program.connectionString))
            {
                connection.Open();
                if (Teamname != null)
                {
                    string query = "SELECT * FROM NBA_Stats2 WHERE Player = @Value1";


                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (String.IsNullOrEmpty(Teamname))
                            command.Parameters.AddWithValue("@Value1", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value1", Teamname);

                        SqlDataAdapter da = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgv1.DataSource = dt;
                        // Execute the SQL query for the current row of data
                        command.ExecuteNonQuery();
                    }
                }
                else if (txtTeamname == null)
                {
                    string query = "SELECT * FROM Teams ORDER BY Name";


                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (String.IsNullOrEmpty(Teamname))
                            command.Parameters.AddWithValue("@Value1", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value1", Teamname);

                        SqlDataAdapter da = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgv1.DataSource = dt;
                        // Execute the SQL query for the current row of data
                        command.ExecuteNonQuery();
                    }
                }
                else
                {
                    string query = "SELECT * FROM Teams WHERE Name LIKE '%' + @Value1 + '%'";


                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Value1", txtTeam);

                        SqlDataAdapter da = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgv1.DataSource = dt;
                        // Execute the SQL query for the current row of data
                        command.ExecuteNonQuery();
                    }
                }
                connection.Close();
            }
            dgvSize();
        }

        private void txtTeamname_TextChanged(object sender, EventArgs e)
        {
            string txtTeam = txtTeamname.Text;
            using (SqlConnection connection = new SqlConnection(Program.connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM Teams WHERE Name LIKE '%' + @Value1 + '%'";


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Value1", txtTeam);

                    SqlDataAdapter da = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgv1.DataSource = dt;
                    // Execute the SQL query for the current row of data
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
    }
}
