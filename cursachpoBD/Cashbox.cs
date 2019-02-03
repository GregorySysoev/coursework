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

            bool wasError = false;

            int tb3 = 0;
            int tb2 = 0;
            long tb1 = 0;

            bool e3 = int.TryParse(textBox3.Text, out tb3);
            bool e2 = int.TryParse(textBox2.Text, out tb2);
            bool e1 = Int64.TryParse(textBox1.Text, out tb1);
            
            
            if (!e3 ||(tb3 < 2) || (tb3 > 15) || (textBox3.Text.Length != 3))
            {
                MessageBox.Show("Ошибка: неверный код валюты");
                wasError = true;
            }

            if (!e2 || (tb2 <= 0) || (tb2 > 500000))
            {
                MessageBox.Show("Ошибка: недопустимое количество валюты");
                wasError = true;
            }

            if (!e1 || (textBox1.Text.Length != 10))
            {
                MessageBox.Show("Ошибка: неправильно введенный паспорт");
                wasError = true;
            }




            if (radioButton1.Checked)
            {
                buy = true;
            } else
            {
                buy = false;
            }






            if (!wasError)
            {
                if (buy == true)
                {
                    query = "INSERT INTO [Сделка] ([Номер кассы],[Номер паспорта клиента]," +
                        "[Дата сделки],[Время сделки],[Код проданной валюты],[Код купленной валюты]," +
                        "[Количество проданной валюты],[Количество купленной валюты]) VALUES" +
                        " (" + "'" + indexOfCash + "'" + "," + "'" + (textBox1.Text) + "'" + "," + "'" + date.ToShortDateString() + "'" + "," +
                        "'" + date.ToShortTimeString() + "'" + "," + "'0'" + "," + "'" + textBox3.Text + "'" + "," + "'0'" + ","
                        + "'" + textBox2.Text + "'" + ")";
                }
                else
                {
                    query = "INSERT INTO [Сделка] ([Номер кассы],[Номер паспорта клиента]," +
                        "[Дата сделки],[Время сделки],[Код проданной валюты],[Код купленной валюты]," +
                        "[Количество проданной валюты],[Количество купленной валюты]) VALUES" +
                        " (" + "'" + indexOfCash + "'" + "," + "'" + (textBox1.Text) + "'" + "," + "'" + date.ToShortDateString() + "'" + "," +
                        "'" + date.ToShortTimeString() + "'" + "," + "'" + textBox3.Text + "'" + "," + "'0'" + "," + "'" + textBox2.Text + "'" + ","
                        + "'0'" + ")";
                }

                dbcom = new OleDbCommand(query);
                dbcom.Connection = dbase;
                OleDbDataReader reader = dbcom.ExecuteReader();
                reader.Read();
                var result = reader;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
