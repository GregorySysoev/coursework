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
    public partial class Bank : Form
    {
        OleDbConnection dbase;
        OleDbCommand dbcom;
        DateTime date = DateTime.Now;
        public void DBConnect()
        {
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:/Users/User/Documents/cursach.mdb";

            dbase = new OleDbConnection(connectionString);
            dbase.Open();
        }

        public Bank()
        {
            InitializeComponent();
            DBConnect();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool wasError = false;
            foreach (char c in maskedTextBox1.Text.ToString())
            {
                if (!((c == ',') || (c == ' ') || (c >= 'А' && c <= 'я')))
                {
                    wasError = true;
                    break;
                }
            }

            if ((wasError) || (maskedTextBox1.Text.Length == 4))
            {
                MessageBox.Show("Ошибка: некорретно введённое ФИО");
            } 
            else
            {
                string query = "INSERT INTO [Касса] ([ФИО кассира]) VALUES ('" +
                    maskedTextBox1.Text.ToString() + "')";
                dbcom = new OleDbCommand(query);
                dbcom.Connection = dbase;
                OleDbDataReader reader = dbcom.ExecuteReader();
                reader.Read();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool wasError = false;
            int valuta = 0;
            double kurs_pokupki = 0;
            double kurs_prodaji = 0;
            if (maskedTextBox2.Text.Length == 3) { valuta = Int32.Parse(maskedTextBox2.Text); }
            else { wasError = true;
                MessageBox.Show("Ошибка: неправильный код валюты");
            }
            if (!wasError && (maskedTextBox3.Text.Length == 5)) kurs_pokupki = double.Parse(maskedTextBox3.Text);
            else { wasError = true;
                MessageBox.Show("Ошибка: неправильный курс покупки");
            }
            if (!wasError && (maskedTextBox4.Text.Length == 5)) kurs_prodaji = double.Parse(maskedTextBox4.Text);
            else { wasError = true;
                MessageBox.Show("Ошибка: неправильный курс продажи");
            }

            if (!wasError && (valuta <= 1 || valuta >= 15))
            {
                wasError = true;
                MessageBox.Show("Ошибка: неправильный код валюты");
            } 

            if (!wasError && ((kurs_prodaji <= 0)))
            {
                wasError = true;
                MessageBox.Show("Ошибка: неправильный курс продажи");
            }

            if (!wasError && (kurs_pokupki <= 0))
            {
                wasError = true;
                MessageBox.Show("Ошибка: неправильный курс покупки");
            }


            if (!wasError) {
                string query1 = "SELECT * FROM [Курс валюты] WHERE " +
                    "([Курс валюты].[Дата] =#" + date.ToShortDateString().Replace('.','/') + 
                    "#) AND ([Курс валюты].[Код валюты] ="+ (maskedTextBox2.Text) + ")";
                dbcom = new OleDbCommand(query1);
                dbcom.Connection = dbase;
                OleDbDataReader reader = dbcom.ExecuteReader();
                
                if (!reader.Read().Equals(null))
                {
                    MessageBox.Show("Ошибка: курс валюты нельзя менять в течении дня");
                } else
                {
                    string query = "INSERT INTO [Курс валюты] ([Дата], [Код валюты], " +
                    "[Курс продажи], [Курс покупки])" +
                    " VALUES ('" + date.ToShortDateString() + "','"
                    + maskedTextBox2.Text + "','" + maskedTextBox4.Text + "','" +
                    maskedTextBox3.Text + "')";

                    dbcom = new OleDbCommand(query);
                    dbcom.Connection = dbase;
                    reader = dbcom.ExecuteReader();
                    reader.Read();
                    reader.Close();
                    //query1 = "UPDATE [Курс валюты] SET [Курс валюты].[Курс продажи] ='" +
                    //    maskedTextBox4.Text + "' , [Курс валюты].[Курс покупки] ='" +
                    //    maskedTextBox3.Text + "' WHERE ([Курс валюты].[Дата] =#" +
                    //    date.ToShortDateString().Replace('.', '/') + "# AND [Курс валюты].[Код валюты] =" + maskedTextBox2.Text +")";
                    //dbcom = new OleDbCommand(query1);
                    //dbcom.Connection = dbase;
                    //reader = dbcom.ExecuteReader();
                    //reader.Read();
                }
            }
        }

        private void maskedTextBox3_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}
