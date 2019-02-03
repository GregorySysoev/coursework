using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cursachpoBD
{
    public partial class Authorization : Form
    {
        public string indexOfCashB;

        OleDbConnection dbase;
        OleDbCommand dbcom;

        public void DBConnect()
        {
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:/Users/User/Documents/cursach.mdb";

            dbase = new OleDbConnection(connectionString);
            dbase.Open();
        }

        public Authorization()
        {
            InitializeComponent();
            DBConnect();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            int abc = 0;
            if ((int.TryParse(textBox3.Text, out abc)) && (abc == 54321))
            {
                Bank bank = new Bank();
                bank.ShowDialog();
            } else
            {
                MessageBox.Show("Ошибка: неверный пароль для Банка");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {  
            int ind = -1;


            if ((int.TryParse(indexOfCashB, out ind) != false) && (indexOfCashB != null))
            {
                string query = "SELECT * FROM [Касса] WHERE [Касса].[№ кассы] = " + ind;
                dbcom = new OleDbCommand(query);
                dbcom.Connection= dbase;
                OleDbDataReader reader = dbcom.ExecuteReader();
                if (reader.Read())
                {
                    int abc = 1;
                    if ((int.TryParse(textBox2.Text, out abc)) && (abc == 12345)) {
                        Cashbox cashbox = new Cashbox(Int32.Parse(reader["№ кассы"].ToString()), reader["ФИО кассира"].ToString());
                        cashbox.ShowDialog();
                    } else
                    {
                        MessageBox.Show("Ошибка: неверный пароль для Кассы");
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка: кассы с данным номером не существует");
                }

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Analyst analyst = new Analyst();
            analyst.ShowDialog();
        }
        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var text = textBox1.Text;
            indexOfCashB = text;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
