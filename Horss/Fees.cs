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
    public partial class Fees : Form
    {
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Spectacle\Documents\HostelMgmt.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False");

        public Fees()
        {
            InitializeComponent();
        }

        public void fillUsnCb()
        {
            if (Con.State == ConnectionState.Closed)
                Con.Open();

            string query = "SELECT Studusn FROM Student_tbl";
            SqlCommand cmd = new SqlCommand(query, Con);
            SqlDataReader rdr = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Columns.Add("Studusn", typeof(string));
            dt.Load(rdr);
            UsnCb.ValueMember = "Studusn";
            UsnCb.DataSource = dt;

            Con.Close();
        }

        void FillFeesDGV()
        {
            Con.Open();
            string myquery = "Select * from Fees_tbl";
            SqlDataAdapter da = new SqlDataAdapter(myquery, Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            paymentDGV.DataSource = ds.Tables[0];
            Con.Close();
        }

        string studname, roomname;
        public void fetchhdata()
        {
            if (Con.State == ConnectionState.Closed)
                Con.Open();

            string query = "SELECT * FROM Student_tbl WHERE Studusn = @usn";
            SqlCommand cmd = new SqlCommand(query, Con);
            cmd.Parameters.AddWithValue("@usn", UsnCb.SelectedValue.ToString());

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                studname = dr["StudName"].ToString();
                roomname = dr["RoomID"].ToString();
                StudentNameTb.Text = studname;
                RoomNumTb.Text = roomname;
            }

            Con.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 Home = new Form1();
            Home.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            PaymentIdTb.Text = paymentDGV.SelectedRows[0].Cells[0].Value.ToString();
            UsnCb.Text = paymentDGV.SelectedRows[0].Cells[1].Value.ToString();
            StudentNameTb.Text = paymentDGV.SelectedRows[0].Cells[2].Value.ToString();
            RoomNumTb.Text = paymentDGV.SelectedRows[0].Cells[3].Value.ToString();
            AmountTb.Text = paymentDGV.SelectedRows[0].Cells[4].Value.ToString();
           // University.Text = StudentDGV.SelectedRows[0].Cells[5].Value.ToString();
        }

        private void UsnCb_SelectionChangeCommitted(object sender, EventArgs e)
        {
            fetchhdata();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (PaymentIdTb.Text == "" || StudentNameTb.Text == "" || UsnCb.Text == "" || AmountTb.Text == "")
            {
                MessageBox.Show("No Empty fields Accepted");
            }
            else
            {
                string paymentperiod = Period.Value.Month.ToString() + Period.Value.Year.ToString();

                if (Con.State == ConnectionState.Closed)
                    Con.Open();

                string checkQuery = "SELECT COUNT(*) FROM Fees_tbl WHERE Studusn = @usn AND PaymentMonth = @month";
                SqlCommand checkCmd = new SqlCommand(checkQuery, Con);
                checkCmd.Parameters.AddWithValue("@usn", UsnCb.SelectedValue.ToString());
                checkCmd.Parameters.AddWithValue("@month", paymentperiod);

                int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("There is No Due for this Month");
                }
                else
                {
                    string insertQuery = "INSERT INTO Fees_tbl VALUES (@payid, @usn, @name, @room, @month, @amount)";
                    SqlCommand insertCmd = new SqlCommand(insertQuery, Con);
                    insertCmd.Parameters.AddWithValue("@payid", PaymentIdTb.Text);
                    insertCmd.Parameters.AddWithValue("@usn", UsnCb.SelectedValue.ToString());
                    insertCmd.Parameters.AddWithValue("@name", StudentNameTb.Text);
                    insertCmd.Parameters.AddWithValue("@room", RoomNumTb.Text);
                    insertCmd.Parameters.AddWithValue("@month", paymentperiod);
                    insertCmd.Parameters.AddWithValue("@amount", AmountTb.Text);

                    insertCmd.ExecuteNonQuery();
                    MessageBox.Show("Payment Successfully Added");
                }

                Con.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (PaymentIdTb.Text == "")
            {
                MessageBox.Show("Enter the payment ID");

            }
            else
            {


                Con.Open();
                String query = "delete from Fees_tbl where PaymentId = '"+PaymentIdTb.Text+ "'";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("payment Sucessfully Deleted");
                Con.Close();
                FillFeesDGV();


            }
        }

        private void Fees_Load(object sender, EventArgs e)
        {
            fillUsnCb();
            FillFeesDGV();
        }
    }
}
