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
    public partial class AddData : Form
    {
        MySqlCommand cmd;
        MySqlDataAdapter adpt;
        DataTable dt;

        public AddData()
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
            if (bunifuCustomTextbox1.Text != "" && bunifuCustomTextbox2.Text != "" && bunifuCustomTextbox3.Text != "" && bunifuCustomTextbox4.Text != "" && bunifuCustomTextbox5.Text != "" && bunifuCustomTextbox6.Text != "" && bunifuCustomTextbox7.Text != "" && bunifuCustomTextbox8.Text != "" && bunifuCustomTextbox9.Text != "")
            {
                MySqlConnection con = new MySqlConnection("server=localhost;user id=root;database=skynet;port=3306;persistsecurityinfo=True");
                con.Open();
                MySqlDataAdapter sda = new MySqlDataAdapter("INSERT INTO schoolimport (TeacherTitle, TeacherSurname, SubjectName, StudentFirstName, StudentSurname, StudentYear, ParentFirstName, ParentSurname, ParentEmail)" +
                    " values ('" + bunifuCustomTextbox1.Text + "', '" + bunifuCustomTextbox2.Text + "', '" + bunifuCustomTextbox3.Text + "', '" + bunifuCustomTextbox4.Text + "', '" + bunifuCustomTextbox5.Text + "', " + bunifuCustomTextbox6.Text + ", '" + bunifuCustomTextbox7.Text + "', '" + bunifuCustomTextbox8.Text + "', '" + bunifuCustomTextbox9.Text + "');", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                this.Hide();
            }
            else
            {
                MessageBox.Show("You Must Input Values In (ALL) Boxes");
            }
        }
    }
}
