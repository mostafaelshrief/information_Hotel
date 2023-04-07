﻿using Hotel_information.ExpensesFolder;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hotel_information
{
    public partial class Expenses_Jan : Form
    {
        public Expenses_Jan()
        {
            InitializeComponent();
        }
        private void Expenses_Load(object sender, EventArgs e)
        {
            populate();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mostafa\source\repos\Hotel information\Hotel information\Database1.mdf;Integrated Security=True");

        private void populate()
        {
            Con.Open();
            String query = "select * from ExpensesTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder bulider = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (TypeCB.SelectedItem.ToString() == "" || PriceTb.Text == "" || CommentTB.Text == "" )
            {
                MessageBox.Show("Missing information");
            }
            else
            {
                
                Con.Open();
                String query = "insert into ExpensesTbl values(N'" + TypeCB.SelectedItem.ToString() + "','" + PriceTb.Text + "','" + CommentTB.Text + "','" + label12.Text + "')";
                SqlCommand cmd = new SqlCommand("INSERT INTO ExpensesTbl (Type,Price,Comment) VALUES " +
                    "(@Type,@Price,@Comment)", Con);
                cmd.Parameters.AddWithValue("@Price", PriceTb.Text);
                cmd.Parameters.AddWithValue("@Type", TypeCB.SelectedItem.ToString());
                cmd.Parameters.AddWithValue("@Comment", CommentTB.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Item successfully Added");

                Con.Close();
                populate();


            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                String query = "delete from ExpensesTbl where Type=N'" + TypeCB.SelectedItem.ToString() + "';";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Item Deleted successfully");
                Con.Close();
                populate();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
        string updatePrice,updateTotal;
        int STRUpdateprice,STRUpdateTotal;
        private void button2_Click(object sender, EventArgs e)
        {
            Con.Open();
            string query1 = "select * from ExpensesTbl where Type='" + TypeCB.SelectedItem.ToString() + "' ";
            SqlCommand cmd1 = new SqlCommand(query1, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd1);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                updatePrice = dr["Price"].ToString();                
            }

            STRUpdateprice = Convert.ToInt32(updatePrice) + Convert.ToInt32(PriceTb.Text);
            
            
            string query = "update ExpensesTbl set Price='" + STRUpdateprice + "',Comment='" + CommentTB.Text + "' where  Type='" + TypeCB.SelectedItem.ToString() + "';";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            cmd1.ExecuteNonQuery();

            MessageBox.Show("Data updateed successfully");

            Con.Close();
            populate();
            /* Con.Open();
             string query1 = "select * from ExpensesTbl where (Type=N'" + TypeCB.SelectedItem.ToString() + "' AND Price='" + PriceTb.Text + "');";
             SqlCommand cmd1 = new SqlCommand(query1, Con);
             DataTable dt = new DataTable();
             SqlDataAdapter sda = new SqlDataAdapter(cmd1);
             sda.Fill(dt);
             foreach (DataRow dr in dt.Rows)
             {
                 updatePrice = dr["Price"].ToString();

             }

             STRUpdateprice = Convert.ToInt32(updatePrice) + Convert.ToInt32(PriceTb.Text);
             double totin = 0.0;
             if (dataGridView1.Rows[0].Cells[3].Value == "Null")
             {
                 label12.Text = totin.ToString();

             }
             else
             {
                 for (int i = 0; i < dataGridView1.Rows.Count; i++)
                 {
                     totin += Convert.ToInt32(dataGridView1.Rows[i].Cells[3].Value);
                 }
                 label12.Text = totin.ToString();
             }
             string query = "update ExpensesTbl set Price='" + STRUpdateprice + "',Comment='" + CommentTB.Text + "',Total='" + label12.Text + "' where Type=N'" + TypeCB.SelectedIndex.ToString() + "';";
             SqlCommand cmd = new SqlCommand(query, Con);
             cmd.ExecuteNonQuery();
             cmd1.ExecuteNonQuery();
             MessageBox.Show("Data updateed successfully");
             Con.Close();
             populate();*/
        }

        private void button4_Click(object sender, EventArgs e)
        {
            double totin = 0.0;
            if (dataGridView1.Rows[0].Cells[1].Value == "Null")
            {
                label12.Text = totin.ToString();

            }
            else
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    totin += Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value)*-1;
                }
                label12.Text = totin.ToString();
            }
            Con.Open();
            string query = "update Total_JanTbl set Total='" + totin + "' where  Name='" + label1.Text+ "';";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            Con.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
           
        }
    }
}
