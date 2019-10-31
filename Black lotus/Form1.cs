using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Black_lotus
{
    public partial class signIn : Form
    {
        public signIn()
        {
            InitializeComponent();
        }

        private void signInButton_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string password = textBox2.Text;
            Class1 signinObj = new Class1();
            if (signinObj.Select(username, password) == true)
            {
                this.Hide();
                new stockManagementFrame().Show();
            }
            else {
                MessageBox.Show("Invalid username/password, please try again");
            }

        }

        private void signIn_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void forgotPasswordLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            new Form3().Show();
        }
    }
}
