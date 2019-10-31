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
    public partial class Form4 : Form
    {
        int oldFlower,oldOrderQty,oldStockQty,newStockQty,newFlowerNewQty,cusIDForUpdateTotal;
        float cusTotalToDelete,oldCusTotal;
        public Form4()
        {
            InitializeComponent();

            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
            textBox6.ReadOnly = true;

            confirmUpdate.Enabled = false;
            confirmDelete.Enabled = false;
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

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void searchOrder_Click(object sender, EventArgs e)
        {
            int inOrderDBSerchFor = int.Parse(textBox1.Text);
            Class1 order = new Class1();
            dataGridView1.DataSource = order.dataOnGrid("SELECT * FROM orderDetails where orderID='"+ inOrderDBSerchFor + "' ;");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Class1 order = new Class1();
            object ordersInDB = order.dataOnGrid("SELECT * FROM orderDetails;");
            dataGridView1.DataSource = ordersInDB;
        }

        private void update_Click(object sender, EventArgs e)
        {
            textBox7.ReadOnly = true;
            textBox5.ReadOnly = true;
            int updateThis = int.Parse(textBox7.Text);
            Class1 updateFlowerDataSet = new Class1();
            List<string>[] list = updateFlowerDataSet.dataSeterForOrder("select * from orderDetails where orderID='" + updateThis + "'");
            textBox2.Text = list[0][0];
            textBox3.Text = list[1][0];
            textBox4.Text = list[2][0];
            textBox5.Text = list[3][0];
            textBox6.Text = list[4][0];
            cusIDForUpdateTotal =int.Parse(list[1][0]);
            oldCusTotal = float.Parse(list[4][0]);
            dataGridView1.DataSource = new Class1().dataOnGrid("select * From orderDetails where orderID='" + updateThis + "';");

            confirmUpdate.Enabled = true;

            delete.Enabled = false;
            stockManagement.Enabled = false;
            clientManagement.Enabled = false;

            oldFlower = int.Parse(list[2][0]);
            oldOrderQty = int.Parse(list[3][0]);
            List<string>[] list1 = new Class1().dataSeter("select * from flowerDetails where flowerID='"+oldFlower+"'");
            oldStockQty= int.Parse(list1[3][0]);
            newStockQty = oldStockQty + oldOrderQty;
        }

        private void confirmUpdate_Click(object sender, EventArgs e)
        {
            float total;
            int flowerID = int.Parse(textBox4.Text);
            int qty = int.Parse(textBox4.Text);
            float newCusTotal = float.Parse(textBox6.Text);

            List<string>[] list2 = new Class1().dataSeter("select * from flowerDetails where flowerID='" + flowerID + "'");
            total =qty* float.Parse(list2[2][0]);
            if (flowerID==oldFlower) { qty =qty-oldOrderQty; }
            List<string>[] list = new Class1().dataSeter("select * from flowerDetails where flowerID='" + flowerID + "'");
            int available =int.Parse(list[3][0]);
            if (available>qty) {
                int updateThis = int.Parse(textBox7.Text);
                int cusID = int.Parse(textBox7.Text);

                new Class1().Update("UPDATE orderDetails SET cusID='" + cusID + "', flowerID='" + flowerID + "', salesQty='" + qty + "',cusTotal='" + total + "' WHERE orderId='" + updateThis + "';");
                dataGridView1.DataSource = new Class1().dataOnGrid("select * From orderDetails where orderID='" + updateThis + "';");
                new Class1().Update("UPDATE flowerDetails SET stockQty='"+newStockQty+"'  where flowerID='"+oldFlower+"'");

                Class1 updateCustomerDataSet = new Class1();
                List<string>[] list1 = updateCustomerDataSet.dataSeterForClient("select * from customerDetails where orderID='" + cusIDForUpdateTotal + "'");
                float cusTotal = float.Parse(list1[10][0]);
                cusTotal = cusTotal - oldCusTotal+ newCusTotal;
                new Class1().Update("UPDATE customerDetails SET cusTotal = '" + cusTotal + "' where cusID='" + cusIDForUpdateTotal + "';");

                int availableNow = int.Parse(list2[3][0]);
                newFlowerNewQty = availableNow - qty;
                new Class1().Update("UPDATE flowerDetails SET stockQty='" + newFlowerNewQty + "'  where flowerID='" + flowerID + "'");

                confirmUpdate.Enabled = false;
                textBox7.ReadOnly = false;
                textBox5.ReadOnly = false;
                delete.Enabled = true;
                stockManagement.Enabled = true;
                clientManagement.Enabled = true;

                textBox7.Clear();
                textBox2.Clear();
                textBox3.Clear();
                textBox4.Clear();
                textBox5.Clear();
                textBox6.Clear();
            }
            else { 
                MessageBox.Show("Stock is not available"); 
            }
        }

        private void delete_Click(object sender, EventArgs e)
        {
            textBox8.ReadOnly = true;
            textBox2.ReadOnly = true;
            textBox3.ReadOnly = true;
            textBox4.ReadOnly = true;
            textBox5.ReadOnly = true;
            textBox6.ReadOnly = true;
            
            int deleteThis = int.Parse(textBox8.Text);
            Class1 deleteOrderDataSet = new Class1();
            List<string>[] list = deleteOrderDataSet.dataSeterForOrder("select * from orderDetails where orderID='" + deleteThis + "'");
            textBox2.Text = list[0][0];
            textBox3.Text = list[1][0];
            textBox4.Text = list[2][0];
            textBox5.Text = list[3][0];
            textBox6.Text = list[4][0];
            cusIDForUpdateTotal = int.Parse(list[1][0]);
            cusTotalToDelete = float.Parse(list[4][0]);
            dataGridView1.DataSource = new Class1().dataOnGrid("select * From orderDetails where orderID='" + deleteThis + "';");

            confirmDelete.Enabled = true;

            update.Enabled = false;
            stockManagement.Enabled = false;
            clientManagement.Enabled = false;
        }

        private void confirmDelete_Click(object sender, EventArgs e)
        {
            int deleteThisData = int.Parse(textBox8.Text);

            new Class1().Delete("DELETE FROM orderDetails WHERE orderId='" + deleteThisData + "';");
            dataGridView1.DataSource = new Class1().dataOnGrid("select * From orderDetails ;");
            Class1 updateCustomerDataSet = new Class1();
            List<string>[] list = updateCustomerDataSet.dataSeterForClient("select * from customerDetails where orderID='" + cusIDForUpdateTotal + "'");
            float cusTotal = float.Parse(list[10][0]);
            cusTotal = cusTotal - cusTotalToDelete;
            new Class1().Update("UPDATE customerDetails SET cusTotal = '"+cusTotal+"' where cusID='"+ cusIDForUpdateTotal + "';");

            confirmDelete.Enabled = false;
            textBox8.ReadOnly = false;
            textBox2.ReadOnly = false;
            textBox3.ReadOnly = false;
            textBox4.ReadOnly = false;
            textBox5.ReadOnly = false;
            textBox6.ReadOnly = false;
            update.Enabled = true;
            stockManagement.Enabled = true;
            clientManagement.Enabled = true;

            textBox8.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
        }

        private void clientManagement_Click(object sender, EventArgs e)
        {
            this.Hide();
            new Form5().Show();
        }
    }
}
