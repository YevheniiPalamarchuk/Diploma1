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
    public partial class PlayerStat : Form
    {
        public string Playername { get; set; }
        public string Teamname { get; set; }
        public void dgvSize()
        {
            dgv1.Columns[0].Width = 180;
            for (int i = 1; i < 29; i++)
                dgv1.Columns[i].Width = 45;
        }
        public PlayerStat()
        {
            InitializeComponent();          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = Playername;
        }

        private void Playerstat_Load(object sender, EventArgs e)
        {
            string txtPlayer = txtPlayername.Text;
            using (SqlConnection connection = new SqlConnection(Program.connectionString))
            {            
                connection.Open();
                if (Playername != null)
                {
                    string query = "SELECT * FROM NBA_Stats2 WHERE Player = @Value1 AND Tm = @Value2 ORDER BY Player";


                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (String.IsNullOrEmpty(Playername))
                            command.Parameters.AddWithValue("@Value1", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value1", Playername);
                        if (String.IsNullOrEmpty(Teamname))
                            command.Parameters.AddWithValue("@Value2", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value2", Teamname);

                        SqlDataAdapter da = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgv1.DataSource = dt;
                        // Execute the SQL query for the current row of data
                        command.ExecuteNonQuery();
                    }
                }
                else if (txtPlayer == null)
                {
                    string query = "SELECT * FROM NBA_Stats2 ORDER BY Player";


                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (String.IsNullOrEmpty(Playername))
                            command.Parameters.AddWithValue("@Value1", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value1", Playername);
                        if (String.IsNullOrEmpty(Teamname))
                            command.Parameters.AddWithValue("@Value2", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value2", Teamname);

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
                    string query = "SELECT * FROM NBA_Stats2 WHERE Player LIKE '%' + @Value1 + '%' ORDER BY Player";


                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Value1", txtPlayer);

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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            label1.Text = Playername;
            string txtPlayer = txtPlayername.Text;
            using (SqlConnection connection = new SqlConnection(Program.connectionString))
            {
                connection.Open();

                string query = "SELECT * FROM NBA_Stats2 WHERE Player LIKE '%' + @Value1 + '%' ORDER BY Player";


                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Value1", txtPlayer);

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
