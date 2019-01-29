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
    
    public partial class Analyst : Form
    {
        OleDbConnection dbase;
        OleDbCommand dbcom;
        OleDbCommand dbcom1;

        int buyVal = 0;
        int sellVal = 0;

        public void DBConnect()
        {
            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:/Users/User/Documents/cursach.mdb";

            dbase = new OleDbConnection(connectionString);
            dbase.Open();
        }

        public Analyst()
        {
            InitializeComponent();
            DBConnect();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            char[] myChar = { ' ', '0', ':' };
            string from = textBox2.Text;
            string to = textBox3.Text;
            string query = "SELECT * FROM [Сделка] WHERE " + 
                " [Сделка].[Код купленной валюты] = " + textBox1.Text +
                " AND  [Сделка].[Дата сделки] between #" + from + "# AND #" + to + "#";
            dbcom = new OleDbCommand(query);
            dbcom.Connection = dbase;
            OleDbDataReader reader = dbcom.ExecuteReader();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (Int32.Parse(reader.GetValue(5).ToString()) == 0)
                    {
                        query = "SELECT * FROM [Курс валюты] WHERE [Курс валюты].[Код валюты] = " +
                            "" + reader.GetValue(6).ToString() + " AND [Курс валюты].[Дата] = #" +
                            reader.GetValue(3).ToString().Replace('.', '/').TrimEnd(myChar) + "#";
                    } else
                    {
                        query = "SELECT * FROM [Курс валюты] WHERE [Курс валюты].[Код валюты] = " +
                            "" + reader.GetValue(5).ToString() + " AND [Курс валюты].[Дата] = #" +
                            reader.GetValue(3).ToString().Replace('.', '/').TrimEnd(myChar) + "#";
                    }
                    dbcom1 = new OleDbCommand(query);
                    dbcom1.Connection = dbase;
                    OleDbDataReader red = dbcom1.ExecuteReader();
                    dataGridView1.Rows.Add(red.GetValue(0).ToString());
                }
            }
            reader.Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void buttonExit_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Analyst_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Analyst repeat = new Analyst();
            this.Close();
            repeat.ShowDialog();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
    

}
