﻿using Microsoft.Data.SqlClient;
using System.Data;

namespace Hotel_information.ExpensesFolder
{
    public partial class Expenses_Jun : Form
    {
        public Expenses_Jun()
        {
            InitializeComponent();
        }

        private void Expenses_Jun_Load(object sender, EventArgs e)
        {
            populate();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mostafa\source\repos\Hotel information\Hotel information\Database1.mdf;Integrated Security=True");

        private void populate()
        {
            Con.Open();
            String query = "select * from Expenses_JunTbl";
            SqlDataAdapter sda = new SqlDataAdapter(query, Con);
            SqlCommandBuilder bulider = new SqlCommandBuilder(sda);
            var ds = new DataSet();
            sda.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
            Con.Close();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            Month_Report_Expenses hotel_Page = new Month_Report_Expenses();
            hotel_Page.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (TypeCB.SelectedItem.ToString() == "" || PriceTb.Text == "" || CommentTB.Text == "")
            {
                MessageBox.Show("Missing information");
            }
            else
            {

                Con.Open();
                String query = "insert into Expenses_DecTbl values(N'" + TypeCB.SelectedItem.ToString() + "','" + PriceTb.Text + "','" + CommentTB.Text + "','" + label12.Text + "')";
                SqlCommand cmd = new SqlCommand("INSERT INTO Expenses_JunTbl (Type,Price,Comment) VALUES " +
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
        string updatePrice, updateTotal;
        int STRUpdateprice, STRUpdateTotal;

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
                    totin += Convert.ToInt32(dataGridView1.Rows[i].Cells[1].Value) * -1;
                }
                label12.Text = totin.ToString();
            }
            Con.Open();
            string query = "update Total_JunTbl set Total='" + label12.Text + "' where  Name='" + label1.Text + "';";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            Con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                Con.Open();
                String query = "delete from Expenses_JunTbl where Type=N'" + TypeCB.SelectedItem.ToString() + "';";
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

        private void button2_Click(object sender, EventArgs e)
        {
            Con.Open();
            string query1 = "select * from Expenses_JunTbl where Type='" + TypeCB.SelectedItem.ToString() + "' ";
            SqlCommand cmd1 = new SqlCommand(query1, Con);
            DataTable dt = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(cmd1);
            sda.Fill(dt);
            foreach (DataRow dr in dt.Rows)
            {
                updatePrice = dr["Price"].ToString();

            }
            STRUpdateprice = Convert.ToInt32(updatePrice) + Convert.ToInt32(PriceTb.Text);

            string query = "update Expenses_JunTbl set Price='" + STRUpdateprice + "',Comment='" + CommentTB.Text + "' where  Type='" + TypeCB.SelectedItem.ToString() + "';";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.ExecuteNonQuery();
            cmd1.ExecuteNonQuery();

            MessageBox.Show("Data updateed successfully");

            Con.Close();
            populate();
        }
    }
}
