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
    public partial class Login : Form
    {
        MySqlCommand cmd;
        MySqlDataAdapter adpt;
        DataTable dt;

        public Login()
        {
            InitializeComponent();
            textBox2.UseSystemPasswordChar = true;
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "curtinfrc")
            {
                if (textBox2.Text == "admin")
                {
                    this.Hide();
                    Main ss = new Main();
                    ss.Show();
                }
                else if (textBox2.Text == "easteregg")
                {
                    easteregg ee = new easteregg();
                    ee.Show();
                }
                else
                {
                    MessageBox.Show("Password Is Incorrect");
                }
            }
            else
            {
                MessageBox.Show("Username and/or Password Is Incorrect");
            }
            
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bunifuCheckbox1_OnChange(object sender, EventArgs e)
        {
            if (bunifuCheckbox1.Checked)
            {
                textBox2.UseSystemPasswordChar = false;
            }
            else
            {
                textBox2.UseSystemPasswordChar = true;
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
