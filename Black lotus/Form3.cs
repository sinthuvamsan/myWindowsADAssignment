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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void back_Click(object sender, EventArgs e)
        {
            this.Hide();
            new signIn().Show();
        }

        private void changePassword_Click(object sender, EventArgs e)
        {
            string email = textBox1.Text;
            string phoneNo = textBox2.Text;
            string password = textBox3.Text;
            string cpassword = textBox4.Text;
            Class1 updatePasswordObj = new Class1();
            List<string>[] list = updatePasswordObj.selectForUpdate(email,phoneNo);
            Console.WriteLine(list[0]);
            Console.ReadLine();
            if (string.Equals(list[0][0], email) && string.Equals(list[1][0], phoneNo)) {
                if (string.Equals(password, cpassword))
                {
                    updatePasswordObj.Update("UPDATE `adminDetails` SET `adminPassword` = `"+password+"' WHERE adminEMail='"+email+"';");
                }
                else
                {
                    MessageBox.Show("Passwords dont match, please try again");
                }
            }
            else {
                MessageBox.Show("Invalid data, please try again");
            }
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
