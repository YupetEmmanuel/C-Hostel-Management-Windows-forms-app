using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Horss
{
    public partial class Employee : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Spectacle\Documents\HostelMgmt.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False");

        public Employee()
        {
            InitializeComponent();
        }

        void FillEmployeeDGV()
        {
            Con.Open();
            string myquery = "Select * from Employee_tbl";
            SqlDataAdapter da = new SqlDataAdapter(myquery, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            EmployeeDGV.DataSource = ds.Tables[0];
            Con.Close();
        }


        private void button4_Click(object sender, EventArgs e)
        {
            Form1 Home = new Form1();
            Home.Show();
            this.Hide();

        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void Employee_Load(object sender, EventArgs e)
        {
            FillEmployeeDGV();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (EmpIdTb.Text == "" || EmpPhoneTb.Text == "")
            {
                MessageBox.Show("No Empty filleds Accepted");

            }
            else
            {



                Con.Open();
                String query = "insert into Employee_tbl Values('" + EmpIdTb.Text + "','" + EmpNameTb.Text + "', '" + EmpPhoneTb.Text + "','" + EmpAddTb.Text + "','" + EmpPositionCb.SelectedItem.ToString() + "','" + EmpStatusCb.SelectedItem.ToString() + "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Employee Sucessfully Added");
                Con.Close();

                FillEmployeeDGV();
            }
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            EmpIdTb.Text = EmployeeDGV.SelectedRows[0].Cells[0].Value.ToString();
            EmpNameTb.Text = EmployeeDGV.SelectedRows[0].Cells[1].Value.ToString();
            EmpPhoneTb.Text = EmployeeDGV.SelectedRows[0].Cells[2].Value.ToString();
            EmpAddTb.Text = EmployeeDGV.SelectedRows[0].Cells[3].Value.ToString();
            EmpPositionCb.SelectedItem = EmployeeDGV.SelectedRows[0].Cells[4].Value.ToString();
            EmpStatusCb.SelectedItem = EmployeeDGV.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (EmpIdTb.Text == "")
            {
                MessageBox.Show("Enter the Employee Id");

            }
            else
            {


                Con.Open();
                String query = "delete from Employee_tbl where EmpId = '" + EmpIdTb.Text + "'";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Employee Sucessfully Deleted");
                Con.Close();
                FillEmployeeDGV();


            }
        }
    }
}

