using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using MySql.Data.MySqlClient;
using NetworkTables;
using NetworkTables.Tables;
namespace GUI
{

    public partial class Main : Form
    {
        MJPEGStream localhost;
        MJPEGStream curtinvision;

        string RobotServertxt = "localhost";
        string VisionServertxt = "curtinvision.local:1181";
        string VisionLocalServertxt = "localhost:1181";

        public Main()
        {
            InitializeComponent();

            localhost = new MJPEGStream("http://" + VisionLocalServertxt + "/stream.mjpg");
            localhost.NewFrame += localhost_NewFrame;

            curtinvision = new MJPEGStream("http://" + VisionServertxt + "/stream.mjpg");
            curtinvision.NewFrame += curtinvision_NewFrame;
        }


        void localhost_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap lcl = (Bitmap)eventArgs.Frame.Clone();
            pictureBox2.Image = lcl;
            pictureBox3.Image = lcl;
        }

        void curtinvision_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bmp = (Bitmap)eventArgs.Frame.Clone();
            pictureBox2.Image = bmp;
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            EditData AD = new EditData();
            AD.Show();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
           
        }

        private void bunifuFlatButton3_Click(object sender, EventArgs e)
        {
         
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
       
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
   
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            
        }


        
        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
         
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuFlatButton5_Click(object sender, EventArgs e)
        {
  
        }

        private void dataGridView3_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bunifuFlatButton6_Click(object sender, EventArgs e)
        {
            AddUser AU = new AddUser();
            AU.Show();
        }

        private void bunifuFlatButton7_Click(object sender, EventArgs e)
        {
            localhost.Start();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton8_Click(object sender, EventArgs e)
        {
            curtinvision.Start();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void bunifuGauge1_Load(object sender, EventArgs e)
        {

        }
    }
}
