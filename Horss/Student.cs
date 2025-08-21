using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Horss
{
    public partial class Student : Form
    {

        public Student()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Spectacle\Documents\HostelMgmt.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False");

        void FillStudentDGV()
        {
            Con.Open();
            string myquery = "Select * from Student_tbl";
            SqlDataAdapter da = new SqlDataAdapter(myquery, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            StudentDGV.DataSource = ds.Tables[0];
            Con.Close();
        }  
        
        void FillRoomComboBox()
        {
            Con.Open();
            string query = "Select * from Room_tbl";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataReader rdr;
            rdr = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("RoomNum", typeof(int) );
           
            dt.Load(rdr);
            StudRoomCb.ValueMember = "RoomNum";
            StudRoomCb.DataSource = dt;
            Con.Close();

        }
        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Student_Load(object sender, EventArgs e)
        {
            FillStudentDGV();
            FillRoomComboBox();

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

        private void button1_Click(object sender, EventArgs e)
        {

            if (StudUsn.Text == "" || StudName.Text == "" || FatherName.Text == "" || MotherName.Text == ""|| Address.Text == ""|| University.Text == "")
            {
                MessageBox.Show("No Empty filleds Accepted");

            }
            else
            {



                Con.Open();
                String query = "insert into Student_tbl Values('" + StudUsn.Text + "','"+StudName.Text+"', '" + FatherName.Text + "','" +MotherName.Text+"','"+Address.Text+"','"+University.Text+"','"+StudRoomCb.SelectedValue.ToString()+"')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student Sucessfully Added");
                Con.Close();
               
                FillStudentDGV();
                FillRoomComboBox();


            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (StudUsn.Text == "")
            {
                MessageBox.Show("Enter room number");

            }
            else
            {


                Con.Open();
                String query = "delete from Student_tbl where Studusn = '" + StudUsn.Text + "'";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Student Sucessfully Deleted");
                Con.Close();
                FillStudentDGV();


            }
        }

        private void StudentDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            StudUsn.Text = StudentDGV.SelectedRows[0].Cells[0].Value.ToString();
            StudName.Text = StudentDGV.SelectedRows[0].Cells[1].Value.ToString();
            FatherName.Text = StudentDGV.SelectedRows[0].Cells[2].Value.ToString();
            MotherName.Text = StudentDGV.SelectedRows[0].Cells[3].Value.ToString();
            Address.Text = StudentDGV.SelectedRows[0].Cells[4].Value.ToString();
            University.Text = StudentDGV.SelectedRows[0].Cells[5].Value.ToString();



        }
    }
}
