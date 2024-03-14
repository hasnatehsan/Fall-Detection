using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using Fall_fall.Model;
//using Npgsql;
namespace Fall_fall
{
    public partial class frmRegister : Form
    {
        public frmRegister()
        {
            InitializeComponent();
        }
     
        //NpgsqlConnection conn = new  NpgsqlConnection(ConfigurationManager.AppSettings.Get("MyConnection"));
        //NpgsqlCommand cmd = new NpgsqlCommand();
        //NpgsqlDataAdapter da = new NpgsqlDataAdapter();
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            txtComPassword.Text = "";
            txtUsername.Focus();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void registrationButton_Click(object sender, EventArgs e)
        {
            PersonModel p = new PersonModel();

            

            //txtUsername.Text = "";
            //txtComPassword.Text = "";
            if (txtUsername.Text == "" && txtPassword.Text == "" && txtComPassword.Text == "")
            {
                MessageBox.Show("Username and Password fields are empty", "Sign Up Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (txtPassword.Text == txtComPassword.Text)
            {
                try
                {
                    p.UserName = txtUsername.Text;
                    p.Password = txtComPassword.Text;
                    p.AgeGroup = comboBox1.SelectedIndex;

                    SqliteDataAccess.SavePerson(p);

                    txtUsername.Text = "";
                    txtPassword.Text = "";
                    txtComPassword.Text = "";
                    MessageBox.Show("Your account has been Successfully Created", "Registration Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Passwords does not match, Please Re-enter", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtComPassword.Text = "";
                txtPassword.Text = "";
                txtPassword.Focus();

            }
        }

        private void checkboxShowPass_CheckedChanged(object sender, EventArgs e)
        {
            if (checkboxShowPass.Checked)
            {
                txtPassword.PasswordChar = '\0';
                txtComPassword.PasswordChar = '\0';

            }
            else
            {
                txtPassword.PasswordChar = '*';
                txtComPassword.PasswordChar = '*';
            }
        }

        //private void label6_Click_1(object sender, EventArgs e)
        //{

        //    new LoginForm().Show();
        //    this.Hide();
    

        private void label6_Click_1(object sender, EventArgs e)
        {
            LoginForm frm = new LoginForm();
            frm.Show();
            this.Hide();

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
