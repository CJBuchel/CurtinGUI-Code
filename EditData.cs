using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace GUI
{
    public partial class EditData : Form
    {
        public static string RobotServer = "";
        public static string VisionServer = "";
        public static string VisionLocalServer = "";

        MySqlCommand cmd;
        MySqlDataAdapter adpt;
        DataTable dt;

        public EditData()
        {
            InitializeComponent();
        }

        private void bunifuCustomLabel3_Click(object sender, EventArgs e)
        {

        }

        private void bunifuCustomLabel10_Click(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void bunifuCustomTextbox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            RobotServer = bunifuCustomTextbox1.Text;
            VisionServer = bunifuCustomTextbox2.Text;
            VisionLocalServer = bunifuCustomTextbox3.Text;

            this.Hide();
        }

        private void bunifuCustomLabel13_Click(object sender, EventArgs e)
        {
                    }

        private void bunifuCustomTextbox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void EditData_Load(object sender, EventArgs e)
        {

        }

        private void bunifuCustomTextbox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
