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
    public partial class Form5 : Form
    {
        public Form5()
        {
            InitializeComponent();
            confrimUpdate.Enabled = false;
            confirmDelete.Enabled = false;
        }

        private void Form5_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void signOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            new signIn().Show();
        }

        private void stockManagement_Click(object sender, EventArgs e)
        {
            this.Hide();
            new stockManagementFrame().Show();
        }

        private void orderManagement_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Form4().Show();
        }

        private void viewClients_Click(object sender, EventArgs e)
        {
            Class1 client = new Class1();
            object clientsInDB = client.dataOnGrid("SELECT * FROM customerDetails;");
            dataGridView1.DataSource = clientsInDB;
        }

        private void search_Click(object sender, EventArgs e)
        {
            int searchFor = int.Parse(textBox1.Text);
            dataGridView1.DataSource = new Class1().dataOnGrid("select * From customerDetails where cusID='" + searchFor + "';");
        }

        private void add_Click(object sender, EventArgs e)
        {
            String lastName = textBox5.Text;
            String firstName = textBox6.Text;
            String email = textBox7.Text;
            int phone = int.Parse(textBox8.Text);
            String date = textBox9.Text;
            int age = int.Parse(textBox10.Text);
            String address = textBox11.Text;
            String un = textBox12.Text;
            String pass = textBox13.Text;

            new Class1().Insert("insert into customerDetails(cusLastName,cusFirstName,cusEMail,cusPhoneNo,cusDOB,cusAge,cusAddress,cusUserName,cusPassword) values('" + lastName + "','" + firstName + "','" + email + "','"+phone+"','"+date+"','"+age+"','"+address+"','"+un+"','"+pass+"');");

            dataGridView1.DataSource = new Class1().dataOnGrid("select * From customerDetails where cusID=last_insert_id();");
        }

        private void update_Click(object sender, EventArgs e)
        {
            textBox2.ReadOnly = true;
            int updateThis = int.Parse(textBox2.Text);
            Class1 updateCustomerDataSet = new Class1();
            List<string>[] list = updateCustomerDataSet.dataSeterForClient("select * from customerDetails where cusID='" + updateThis + "'");
            textBox5.Text = list[1][0];
            textBox6.Text = list[2][0];
            textBox7.Text = list[3][0];
            textBox8.Text = list[4][0];
            textBox9.Text = list[5][0];
            textBox10.Text = list[6][0];
            textBox11.Text = list[7][0];
            textBox12.Text = list[8][0];
            textBox13.Text = list[9][0];
            textBox14.Text = list[10][0];
            dataGridView1.DataSource = new Class1().dataOnGrid("select * From customerDetails where cusID='" + updateThis + "';");

            confrimUpdate.Enabled = true;

            delete.Enabled = false;
            stockManagement.Enabled = false;
            orderManagement.Enabled = false;
            add.Enabled = false;
        }

        private void confrimUpdate_Click(object sender, EventArgs e)
        {

            int updateThis = int.Parse(textBox2.Text);
            String lastName = textBox5.Text;
            String firstName = textBox6.Text;
            String email = textBox7.Text;
            int phone = int.Parse(textBox8.Text);
            String date = textBox9.Text;
            int age = int.Parse(textBox10.Text);
            String address = textBox11.Text;
            String un = textBox12.Text;
            String pass = textBox13.Text;
            float total = float.Parse(textBox14.Text);

            new Class1().Update("UPDATE customerDetails SET cusLastName='" + lastName + "', cusFirstName='" + firstName + "',cusEMail='" + email + "',cusPhoneNo='" + phone + "',cusDOB='" + date + "',cusAge='" + age + "',cusAddress='" + address + "',cusUserName='" + un + "',cusPassword='" + pass + "',cusTotal='" + total + "' WHERE cusId='" + updateThis + "';");
            dataGridView1.DataSource = new Class1().dataOnGrid("select * From customerDetails where cusID='" + updateThis + "';");

            confrimUpdate.Enabled = false;
            textBox2.ReadOnly = false;
            delete.Enabled = true;
            stockManagement.Enabled = true;
            orderManagement.Enabled = true;

            textBox2.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            textBox11.Clear();
            textBox12.Clear();
            textBox13.Clear();
            textBox14.Clear();
        }

        private void delete_Click(object sender, EventArgs e)
        {
            textBox3.ReadOnly = true;
            int deleteThis = int.Parse(textBox3.Text);
            Class1 deleteCustomerDataSet = new Class1();
            List<string>[] list = deleteCustomerDataSet.dataSeterForClient("select * from customerDetails where cusID='" + deleteThis + "'");
            textBox5.Text = list[1][0];
            textBox6.Text = list[2][0];
            textBox7.Text = list[3][0];
            textBox8.Text = list[4][0];
            textBox9.Text = list[5][0];
            textBox10.Text = list[6][0];
            textBox11.Text = list[7][0];
            textBox12.Text = list[8][0];
            textBox13.Text = list[9][0];
            textBox14.Text = list[10][0];
            dataGridView1.DataSource = new Class1().dataOnGrid("select * From customerDetails where cusID='" + deleteThis + "';");

            confirmDelete.Enabled = true;

            update.Enabled = false;
            stockManagement.Enabled = false;
            orderManagement.Enabled = false;
            add.Enabled = false;
        }

        private void confirmDelete_Click(object sender, EventArgs e)
        {
            int deleteThisData = int.Parse(textBox3.Text);

            new Class1().Update("DELETE FROM customerDetails WHERE cusId='" + deleteThisData + "';");
            dataGridView1.DataSource = new Class1().dataOnGrid("select * From customerDetails ;");

            confirmDelete.Enabled = false;
            textBox3.ReadOnly = false;
            update.Enabled = true;
            stockManagement.Enabled = true;
            orderManagement.Enabled = true;
            add.Enabled = true;

            textBox3.Clear();
            textBox5.Clear();
            textBox6.Clear();
            textBox7.Clear();
            textBox8.Clear();
            textBox9.Clear();
            textBox10.Clear();
            textBox11.Clear();
            textBox12.Clear();
            textBox13.Clear();
            textBox14.Clear();
        }
    }
}
