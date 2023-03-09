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
    public partial class Matches : Form
    {
        public void dgvSize()
        {
            dgv5.Columns[0].Width = 200;
            for (int i = 1; i < 8; i++)
            {
                dgv5.Columns[i].Width = 50;
            }
            dgv3.Columns[0].Width = 180;
            dgv4.Columns[0].Width = 180;
        }
        public Matches()
        {
            InitializeComponent();

            using (SqlConnection connection = new SqlConnection(Program.connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Schedule ORDER BY date";
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgv1.DataSource = dt;
                    // Execute the SQL query for the current row of data
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }

        }
        




        

        public void schedule_refresh()
        {
            using (SqlConnection connection = new SqlConnection(Program.connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Schedule AS S Where S.date BETWEEN @Value1 AND @Value2 ORDER BY date";
                DateTime value1 = dateTimePicker1.Value;
                DateTime value2 = dateTimePicker2.Value;

                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.Add("@Value1", SqlDbType.DateTime);
                    cmd.Parameters.Add("@Value2", SqlDbType.DateTime);
                    cmd.Parameters["@Value1"].Value = value1;
                    cmd.Parameters["@Value2"].Value = value2;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgv1.DataSource = dt;
                    // Execute the SQL query for the current row of data
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
            }
        }
        private void dgv1_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgv1.SelectedRows[0];
                using (SqlConnection connection = new SqlConnection(Program.connectionString))
                {
                    connection.Open();
                    string query = "SELECT Name, Shortname FROM Teams WHERE Name = @Value1 OR Name = @Value2";
                    string value1 = selectedRow.Cells[2].Value.ToString();
                    string value2 = selectedRow.Cells[3].Value.ToString();


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

                        SqlDataAdapter da = new SqlDataAdapter(command);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgv2.DataSource = dt;
                        // Execute the SQL query for the current row of data
                        command.ExecuteNonQuery();
                    }
                    //string query2 = "SELECT * FROM Eastern_Conference WHERE @Value1 IN (SELECT Name FROM Eastern_Conference) UNION SELECT * FROM Western_conference WHERE @Value1 IN (SELECT Name FROM Western_Conference) ORDER BY W DESC";
                    //using (SqlCommand command = new SqlCommand(query2, connection))
                    //{
                    //    command.Parameters.AddWithValue("@Value1", value1);

                    //    SqlDataAdapter da = new SqlDataAdapter(command);
                    //    DataTable dt = new DataTable();
                    //    da.Fill(dt);
                    //    dgv5.DataSource = dt;
                    //    // Execute the SQL query for the current row of data
                    //    command.ExecuteNonQuery();
                    //}
                    connection.Close();
                }
            }

        }
        private void dgv2_DataSourceChanged(object sender, EventArgs e)
        {
            if (dgv2.Rows.Count > 2)
            {
                using (SqlConnection connection = new SqlConnection(Program.connectionString))
                {
                    connection.Open();
                    string query = "SELECT Player FROM NBA_Stats2 WHERE Tm = @Value1";
                    string value1 = dgv2.Rows[0].Cells[1].Value.ToString();
                    string value2 = dgv2.Rows[1].Cells[1].Value.ToString();


                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (String.IsNullOrEmpty(value1))
                            command.Parameters.AddWithValue("@Value1", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value1", value1);

                        SqlDataAdapter da = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgv3.DataSource = dt;
                        // Execute the SQL query for the current row of data
                        command.ExecuteNonQuery();
                    }
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        if (String.IsNullOrEmpty(value2))
                            command.Parameters.AddWithValue("@Value1", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value1", value2);

                        SqlDataAdapter da = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgv4.DataSource = dt;
                        // Execute the SQL query for the current row of data
                        command.ExecuteNonQuery();
                    }
                    dgvSize();
                    connection.Close();
                }
            }
        }
        private void dgv2_SelectionChanged(object sender, EventArgs e)
        {
            if (dgv2.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow1 = dgv1.SelectedRows[0];
                DataGridViewRow selectedRow2 = dgv2.SelectedRows[0];
                using (SqlConnection connection = new SqlConnection(Program.connectionString))
                {
                    connection.Open();
                    string queryConf = "SELECT COUNT(Name) FROM Eastern_Conference WHERE Name = @Value1";
                    string valueConf = selectedRow2.Cells[0].Value.ToString();
                    int count = -1;

                    using (SqlCommand command = new SqlCommand(queryConf, connection))
                    {
                        if (String.IsNullOrEmpty(valueConf))
                            command.Parameters.AddWithValue("@Value1", DBNull.Value);
                        else
                            command.Parameters.AddWithValue("@Value1", valueConf);

                        // Execute the SQL query for the current row of data
                        command.ExecuteNonQuery();
                        count = (int)command.ExecuteScalar();
                        label2.Text = count.ToString();
                        if (count == 1)
                            label2.Text = "Eastern conference";
                        else if (count == 0)
                            label2.Text = "Western conference";
                    }
                    string query2 = "SELECT * FROM Eastern_Conference WHERE @Value1 IN (SELECT Name FROM Eastern_Conference) UNION SELECT * FROM Western_conference WHERE @Value1 IN (SELECT Name FROM Western_Conference) ORDER BY W DESC";
                    using (SqlCommand command = new SqlCommand(query2, connection))
                    {
                        command.Parameters.AddWithValue("@Value1", valueConf);

                        SqlDataAdapter da = new SqlDataAdapter(command);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dgv5.DataSource = dt;
                        // Execute the SQL query for the current row of data
                        command.ExecuteNonQuery();
                    }                    
                    connection.Close();
                }                    
            }
                
        }

        private void dgv3_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            schedule_refresh();
        }

        private void dgv3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string value1 = "No name";
            string value2 = "No name";
            if (dgv3.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgv3.SelectedRows[0];
                DataGridViewRow selectedRowTeam = dgv2.SelectedRows[0];
                value1 = selectedRow.Cells[0].Value.ToString();
                value2 = selectedRowTeam.Cells[0].Value.ToString();
                value2 = Program.teamShort(value2);
            }


            PlayerStat formPlayerstat = new PlayerStat();
            formPlayerstat.Playername = value1;
            formPlayerstat.Teamname = value2;
            formPlayerstat.ShowDialog();
        }

        private void dgv3_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            
        }

        private void dgv2_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void button1_Click_3(object sender, EventArgs e)
        {
            ConferenceStat conferencestat = new ConferenceStat();
            conferencestat.ShowDialog();
        }
    }
}
