using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Timers;

namespace Horss
{
    public partial class Rooms : Form
    {
        public Rooms()
        {
            InitializeComponent();
        }
        SqlConnection Con = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Spectacle\Documents\HostelMgmt.mdf;Integrated Security=True;Connect Timeout=30;Encrypt=False");

        void FillRoomDGV()
        {
            Con.Open();
            string myquery = "Select * from Room_tbl";
            SqlDataAdapter da = new SqlDataAdapter(myquery,Con);
            SqlCommandBuilder builder = new SqlCommandBuilder(da);
            var ds = new DataSet();
            da.Fill(ds);
            RoomDGV.DataSource = ds.Tables[0];




            Con.Close();
        }

        private void label8_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form1 Home = new Form1();
            Home.Show();
            this.Hide();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            RoomNumtb.Text = RoomDGV.SelectedRows[0].Cells[0].Value.ToString();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        string RoomBooked;
        private void button1_Click(object sender, EventArgs e)
        {
            
            if (RoomNumtb.Text =="")
            {
                MessageBox.Show("Enter room number");

            }
            else
            {
                if (YesRadio.Checked == true)
                {
                    RoomBooked = "busy";
                        }
                else
                {
                    RoomBooked = "Free";

                }


                Con.Open();
                String query = "insert into Room_tbl Values(" + RoomNumtb.Text + ", '" + RoomStatusCb.SelectedItem.ToString() +"','"+ RoomBooked+ "')";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Room Sucessfully Added");
                Con.Close();
                FillRoomDGV();


            }
          
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (RoomNumtb.Text == "")
            {
                MessageBox.Show("Enter room number");

            }
            else
            {
                

                Con.Open();
                String query = "delete from Room_tbl where RoomNum = "+RoomNumtb.Text+"";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Room Sucessfully Deleted");
                Con.Close();
                FillRoomDGV();


            }

        }

        private void Rooms_Load(object sender, EventArgs e)
        {
            FillRoomDGV();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (RoomNumtb.Text == "")
            {
                MessageBox.Show("Enter room number");

            }
            else
            {
                if (YesRadio.Checked == true)
                {
                    RoomBooked = "busy";
                }
                else
                {
                    RoomBooked = "Free";

                }



                Con.Open();
                String query = "update Room_tbl set RoomStatus = "+RoomStatusCb.SelectedItem+",Booked =  '"+ RoomBooked+ "' where RoomNum = "+RoomNumtb.Text+"";
                SqlCommand cmd = new SqlCommand(query, Con);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Room Sucessfully Updated");
                Con.Close();
                FillRoomDGV();


            }

        }

        private void RoomStatusCb_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
