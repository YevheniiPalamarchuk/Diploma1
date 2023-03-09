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
    public partial class ConferenceStat : Form
    {
        public ConferenceStat()
        {
            InitializeComponent();
        }
        public void dgvSize()
        {
            dgv1.Columns[0].Width = 200;
            dgv2.Columns[0].Width = 200;
            for (int i = 1; i < 8; i++)
            {
                dgv1.Columns[i].Width = 50;
                dgv2.Columns[i].Width = 50;
            }
        }

        private void ConferenceStat_Load(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(Program.connectionString))
            {
                connection.Open();
                string queryWest = "SELECT * FROM Western_Conference ORDER BY W DESC";
                string queryEast = "SELECT * FROM Eastern_Conference ORDER BY W DESC";
                label1.Text = "Western conference standings";
                label2.Text = "Eastern conference standings";
                using (SqlCommand command = new SqlCommand(queryWest, connection))
                {

                    SqlDataAdapter da = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgv1.DataSource = dt;
                    // Execute the SQL query for the current row of data
                    command.ExecuteNonQuery();
                }
                using (SqlCommand command = new SqlCommand(queryEast, connection))
                {

                    SqlDataAdapter da = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgv2.DataSource = dt;
                    // Execute the SQL query for the current row of data
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            dgvSize();
        }
    }
}
