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

namespace GUI
{
    public partial class Main : Form
    {

        public Main()
        {
            InitializeComponent();
        }

        void entity101vision_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            Bitmap bmp = (Bitmap)eventArgs.Frame.Clone();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            AddData AD = new AddData();
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
    }
}
