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
    public partial class Cashbox : Form
    {
        public int indexOfCash;
        DateTime date = DateTime.Now;

        public bool buy = true;

        OleDbConnection dbase;
        OleDbCommand dbcom;

        public void DBConnect()
        {
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:/Users/User/Documents/cursach.mdb";

            dbase = new OleDbConnection(connectionString);
            dbase.Open();
        }

        public Cashbox(int ind, string nameofCashMan)
        {
            InitializeComponent();
            indexOfCash = ind;
            DBConnect();
            this.label3.Text = ind.ToString();
            this.label4.Text = nameofCashMan;
            this.label5.Text = date.ToShortDateString();
            this.label6.Text = date.ToShortTimeString();
            this.textBox1.MaxLength = 10;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

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
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void Cashbox_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query;
            if (buy == true)
            {
                query = "INSERT INTO [Сделка] ([Номер кассы],[Номер паспорта клиента]," +
                    "[Дата сделки],[Время сделки],[Код проданной валюты],[Код купленной валюты]," +
                    "[Количество проданной валюты],[Количество купленной валюты]) VALUES" +
                    " (" + "'" +indexOfCash+ "'" + "," + "'" + Int64.Parse(textBox1.Text) + "'" + "," + "'" + date.ToShortDateString() + "'" + "," +
                    "'" + date.ToShortTimeString() + "'" + "," + "'0'" + ","+"'"+ textBox3.Text + "'" + "," + "'0'" + ","
                    + "'" + textBox2.Text +"'"+ ")";
            } else
            {
                query = "INSERT INTO [Сделка] ([Номер кассы],[Номер паспорта клиента]," +
                    "[Дата сделки],[Время сделки],[Код проданной валюты],[Код купленной валюты]," +
                    "[Количество проданной валюты],[Количество купленной валюты]) VALUES" +
                    " (" + "'" + indexOfCash + "'" + "," + "'" + Int64.Parse(textBox1.Text) + "'" + "," + "'" + date.ToShortDateString() + "'" + "," +
                    "'" + date.ToShortTimeString() + "'" + "," + "'" + textBox3.Text + "'" + "," + "'0'" + "," + "'" + textBox2.Text + "'" + ","
                    + "'0'" + ")";
            }

            dbcom = new OleDbCommand(query);
            dbcom.Connection = dbase;
            OleDbDataReader reader = dbcom.ExecuteReader();
            reader.Read();
            var result = reader;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            buy = true;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            buy = false;
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
