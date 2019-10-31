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
    public partial class stockManagementFrame : Form
    {
        public stockManagementFrame()
        {
            InitializeComponent();
            confirmUpdate.Enabled = false;
            confirmDelete.Enabled = false;
        }

        private void stockManagementFrame_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void signOut_Click(object sender, EventArgs e)
        {
            this.Hide();
            new signIn().Show();
        }

        private void viewAllFlowerInDB_Click(object sender, EventArgs e)
        {
            Class1 flower = new Class1();
            object flowersInDB = flower.dataOnGrid("SELECT * FROM flowerDetails ;");
            dataGridView1.DataSource = flowersInDB;
        }

        private void orderManagement_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Form4().Show();
        }

        private void add_Click(object sender, EventArgs e)
        {
            String name = textBox2.Text;
            String colour = textBox3.Text;
            int qty = int.Parse(textBox4.Text);
            float uPrice = float.Parse(textBox5.Text);

            new Class1().Insert("insert into flowerDetails(flowerName,flowerColour,unitPrice,stockQty) values('"+name+"','"+colour+"','"+uPrice+"','"+qty+"');");

            dataGridView1.DataSource = new Class1().dataOnGrid("select * From flowerDetails where flowerID=last_insert_id();");
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();

        }

        private void searchForTable_Click(object sender, EventArgs e)
        {
           int searchFor = int.Parse(textBox1.Text);
            dataGridView1.DataSource = new Class1().dataOnGrid("select * From flowerDetails where flowerID='"+searchFor+"';");
            textBox1.Clear();
        }

        private void updateSelect_Click(object sender, EventArgs e)
        {
            textBox7.ReadOnly = true;
            int updateThis = int.Parse(textBox7.Text);
            Class1 updateFlowerDataSet = new Class1();
            List<string>[] list = updateFlowerDataSet.dataSeter("select * from flowerDetails where flowerID='"+updateThis+"'");
            textBox2.Text = list[0][0];
            textBox3.Text = list[1][0];
            textBox4.Text = list[2][0];
            textBox5.Text = list[3][0];
            dataGridView1.DataSource = new Class1().dataOnGrid("select * From flowerDetails where flowerID='" + updateThis + "';");

            confirmUpdate.Enabled = true;

            add.Enabled = false;
            deleteThis.Enabled = false;
            orderManagement.Enabled = false;
            clientManagement.Enabled = false;
        }

        private void confirmUpdate_Click(object sender, EventArgs e)
        {
            int updateThis = int.Parse(textBox7.Text);
            String name = textBox2.Text;
            String colour = textBox3.Text;
            int qty = int.Parse(textBox4.Text);
            float price = float.Parse(textBox5.Text);

            new Class1().Update("UPDATE flowerDetails SET flowerName ='"+name+"', flowerColour='"+colour+"', unitPrice='"+price+"', stockQty='"+qty+"' WHERE flowerId='"+updateThis+"';");
            dataGridView1.DataSource = new Class1().dataOnGrid("select * From flowerDetails where flowerID='" + updateThis + "';");

            confirmUpdate.Enabled = false;
            textBox7.ReadOnly = false;
            add.Enabled = true;
            deleteThis.Enabled = true;
            orderManagement.Enabled = true;
            clientManagement.Enabled = true;

            textBox7.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }

        private void deleteThis_Click(object sender, EventArgs e)
        {
            textBox6.ReadOnly = true;
            int deleteThisData = int.Parse(textBox6.Text);
            Class1 updateFlowerDataSet = new Class1();
            List<string>[] list = updateFlowerDataSet.dataSeter("select * from flowerDetails where flowerID='" + deleteThisData + "';");
            textBox2.Text = list[0][0];
            textBox3.Text = list[1][0];
            textBox4.Text = list[2][0];
            textBox5.Text = list[3][0];
            dataGridView1.DataSource = new Class1().dataOnGrid("select * From flowerDetails where flowerID='" + deleteThisData + "';");

            confirmDelete.Enabled = true;

            add.Enabled = false;
            updateSelect.Enabled = false;
            orderManagement.Enabled = false;
            clientManagement.Enabled = false;
        }

        private void confirmDelete_Click(object sender, EventArgs e)
        {
            int deleteThisData = int.Parse(textBox6.Text);

            new Class1().Update("DELETE FROM flowerDetails WHERE flowerId='" + deleteThisData + "';");
            dataGridView1.DataSource = new Class1().dataOnGrid("select * From flowerDetails ;");

            confirmDelete.Enabled = false;
            textBox6.ReadOnly = false;
            add.Enabled = true;
            updateSelect.Enabled = true;
            orderManagement.Enabled = true;
            clientManagement.Enabled = true;

            textBox6.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
        }

        private void clientManagement_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Form5().Show();
        }
    }
}
