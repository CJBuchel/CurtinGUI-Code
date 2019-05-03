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
    public partial class AddUser : Form
    {
        MySqlCommand cmd;
        MySqlDataAdapter adpt;
        DataTable dt;

        public AddUser()
        {
            InitializeComponent();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void bunifuFlatButton2_Click(object sender, EventArgs e)
        {
            if (bunifuCustomTextbox4.Text != "true")
            {
                if (bunifuCustomTextbox4.Text != "false")
                {
                    MessageBox.Show("Invalid answer " + bunifuCustomTextbox4.Text);
                }
                else
                {
                    if (bunifuCustomTextbox2.Text != bunifuCustomTextbox3.Text)
                    {

                        MessageBox.Show("Passwords Do Not Match");
                    }
                    else if (bunifuCustomTextbox2.Text == bunifuCustomTextbox3.Text)
                    {
                        MySqlConnection con = new MySqlConnection("server=localhost;user id=root;database=skynet;port=3306;persistsecurityinfo=True");
                        con.Open();
                        //MySqlCommand cmd = new MySqlCommand("select * from users where Username='" + textBox1.Text + "' and Password='" + textBox2.Text + "'", con);
                        //MySqlDataAdapter sda = new MySqlDataAdapter("INSERT INTO users WHERE Username='" + bunifuCustomTextbox1.Text + "' and Password='" + bunifuCustomTextbox2.Text + "'", con);
                        MySqlDataAdapter sda = new MySqlDataAdapter("INSERT INTO users (USERNAME, PASSWORD, isParent) values ('" + bunifuCustomTextbox1.Text + "', '" + bunifuCustomTextbox2.Text + "', " + bunifuCustomTextbox4.Text + ");", con);
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        this.Hide();
                    }
                }
            }
            else
            {
                if (bunifuCustomTextbox2.Text != bunifuCustomTextbox3.Text)
                {

                    MessageBox.Show("Passwords Do Not Match");
                }
                else if (bunifuCustomTextbox2.Text == bunifuCustomTextbox3.Text)
                {
                    MySqlConnection con = new MySqlConnection("server=localhost;user id=root;database=skynet;port=3308;persistsecurityinfo=True");
                    con.Open();
                    //MySqlCommand cmd = new MySqlCommand("select * from users where Username='" + textBox1.Text + "' and Password='" + textBox2.Text + "'", con);
                    //MySqlDataAdapter sda = new MySqlDataAdapter("INSERT INTO users WHERE Username='" + bunifuCustomTextbox1.Text + "' and Password='" + bunifuCustomTextbox2.Text + "'", con);
                    MySqlDataAdapter sda = new MySqlDataAdapter("INSERT INTO users (USERNAME, PASSWORD, isParent) values ('" + bunifuCustomTextbox1.Text + "', '" + bunifuCustomTextbox2.Text + "', " + bunifuCustomTextbox4.Text + ");", con);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    this.Hide();
                }
            }
        }

        private void bunifuCheckbox1_OnChange(object sender, EventArgs e)
        {
            if (bunifuCheckbox1.Checked)
            {
                bunifuCustomTextbox2.UseSystemPasswordChar = false;
                bunifuCustomTextbox3.UseSystemPasswordChar = false;
            }
            else
            {
                bunifuCustomTextbox2.UseSystemPasswordChar = true;
                bunifuCustomTextbox3.UseSystemPasswordChar = true;
            }
        }

        private void bunifuCustomTextbox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuCustomTextbox2_TextChanged(object sender, EventArgs e)
        {
            bunifuCustomTextbox2.UseSystemPasswordChar = true;
            bunifuCustomTextbox3.UseSystemPasswordChar = true;
        }

        private void bunifuCustomTextbox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void bunifuCustomTextbox4_TextChanged(object sender, EventArgs e)
        {
     
        }
    }
}
  
