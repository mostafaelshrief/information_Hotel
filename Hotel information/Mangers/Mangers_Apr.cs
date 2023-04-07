﻿using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel_information.Mangers
{
    public partial class Mangers_Apr : Form
    {
        public Mangers_Apr()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mostafa\source\repos\Hotel information\Hotel information\Database1.mdf;Integrated Security=True");
        private void Mangers_Apr_Load(object sender, EventArgs e)
        {
            populate();
        }

        private void populate()
        {
            Con.Open();
            string query = "select * from Manger_Apr";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder bulider = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text==""||textBox2.Text=="")
            {
                MessageBox.Show("Missing information");
            }
            else 
            {
                Con.Open();
                String query = "insert into Manger_Apr values(N'" + textBox1.Text + "','" + textBox2.Text + "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Item added successfully");
                Con.Close();
                populate();
            }
        }
        string updatePrice;
        int STRUpdateprice;
        private void button2_Click(object sender, EventArgs e)
        {
            Con.Open();
            string query1 = "select * from Manger_Apr where Name='" + textBox1 + "' ";
            SqlCommand cmd1 = new SqlCommand(query1, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd1);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                updatePrice = dr["commission"].ToString();

            }
            STRUpdateprice = Convert.ToInt32(updatePrice) + Convert.ToInt32(textBox2.Text);

            string query = "update Manger_Apr set commission='" + STRUpdateprice + "' where  Name='" + textBox1 + "';";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            cmd1.ExecuteNonQuery();
            MessageBox.Show("Data updateed successfully");
            Con.Close();
            populate();
        }
    }
}
