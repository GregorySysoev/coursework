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
            this.dataGridView1.Rows.Clear();
            DateTime from = DateTime.Parse(maskedTextBox1.Text);
            DateTime to = DateTime.Parse(maskedTextBox2.Text);
            double res = 0;

            int rol = 0;

            if (from <= to) {
                for (int i = 2; i < 15; i++)
                {
                    string qry = "SELECT * FROM [Валюта] WHERE [Валюта].[Код валюты] =" + i;
                    dbcom = new OleDbCommand(qry);
                    dbcom.Connection = dbase;
                    OleDbDataReader qryReader = dbcom.ExecuteReader();
                    qryReader.Read();

                    int rowIdVal = dataGridView1.Rows.Add();
                    DataGridViewRow rowVal = dataGridView1.Rows[rowIdVal];
                    rowVal.Cells["Column1"].Value = "Валюта: " + qryReader.GetString(1);

                    qryReader.Close();
                    
                    double buyResult = 0;
                    string zaprosBuy = "SELECT * FROM[Сделка] WHERE " +
                    " [Сделка].[Код купленной валюты] = " + i +
                    " AND  [Сделка].[Дата сделки] between #" +
                    from.ToShortDateString().Replace('.', '/') +
                    "# AND #" + to.ToShortDateString().Replace('.', '/') + "#";
                    dbcom = new OleDbCommand(zaprosBuy);
                    dbcom.Connection = dbase;
                    OleDbDataReader rdrBuy = dbcom.ExecuteReader();

                    double sumInRow = 0;
                    while (rdrBuy.Read())
                    {
                        int rowId = dataGridView1.Rows.Add();
                        DataGridViewRow row = dataGridView1.Rows[rowId];

                        string getFIO = "SELECT * FROM [Касса] WHERE [Касса].[№ кассы] =" + rdrBuy[1];
                        dbcom1 = new OleDbCommand(getFIO);
                        dbcom1.Connection = dbase;
                        OleDbDataReader dr = dbcom1.ExecuteReader();

                        dr.Read();

                        string kostyl = DateTime.Parse(rdrBuy[3].ToString()).ToShortDateString();
                        string kostyl2 = kostyl[3].ToString() + kostyl[4].ToString() + "/"
                            + kostyl[0].ToString() + kostyl[1].ToString() +"/" + kostyl[6].ToString()+
                            kostyl[7].ToString() + kostyl[8].ToString() + kostyl[9].ToString();

                        string getKurs = "SELECT * FROM [Курс валюты] WHERE ([Курс валюты].[Код валюты] =" +
                            rdrBuy[6] + ") AND ([Курс валюты].[Дата] =#" + 
                            kostyl2 + "#)";

                        dbcom1 = new OleDbCommand(getKurs);
                        dbcom1.Connection = dbase;
                        OleDbDataReader dr1 = dbcom1.ExecuteReader();

                        dr1.Read();
                        row.Cells["Column1"].Value = dr[0];
                        row.Cells["S1"].Value = 0;
                        row.Cells["S2"].Value = 0;
                        row.Cells["S3"].Value = rdrBuy[8].ToString();
                        row.Cells["S4"].Value = dr1[3].ToString();
                        row.Cells["S6"].Value = rdrBuy[3].ToString();
                        row.Cells["S7"].Value = int.Parse(row.Cells["S3"].Value.ToString())
                            * double.Parse(row.Cells["S4"].Value.ToString());
                        sumInRow+= int.Parse(row.Cells["S3"].Value.ToString())
                            * double.Parse(row.Cells["S4"].Value.ToString());
                        buyResult += double.Parse(row.Cells["S7"].Value.ToString());

                        rol++;
                    }

                    //from HERE!

                    double sellResult = 0;
                    string zaprosBuy1 = "SELECT * FROM[Сделка] WHERE " +
                    " [Сделка].[Код проданной валюты] = " + i +
                    " AND  [Сделка].[Дата сделки] between #" +
                    from.ToShortDateString().Replace('.', '/') +
                    "# AND #" + to.ToShortDateString().Replace('.', '/') + "#";
                    dbcom = new OleDbCommand(zaprosBuy1);
                    dbcom.Connection = dbase;
                    OleDbDataReader rdrBuy1 = dbcom.ExecuteReader();

                    while(rdrBuy1.Read())
                    {
                        int rowId = dataGridView1.Rows.Add();
                        DataGridViewRow row = dataGridView1.Rows[rowId];

                        string getFIO = "SELECT * FROM [Касса] WHERE [Касса].[№ кассы] =" + rdrBuy1[1];
                        dbcom1 = new OleDbCommand(getFIO);
                        dbcom1.Connection = dbase;
                        OleDbDataReader dr = dbcom1.ExecuteReader();

                        dr.Read();

                        string kostyl = DateTime.Parse(rdrBuy1[3].ToString()).ToShortDateString();
                        string kostyl2 = kostyl[3].ToString() + kostyl[4].ToString() + "/"
                            + kostyl[0].ToString() + kostyl[1].ToString() + "/" + kostyl[6].ToString() +
                            kostyl[7].ToString() + kostyl[8].ToString() + kostyl[9].ToString();

                        string getKurs = "SELECT * FROM [Курс валюты] WHERE [Курс валюты].[Код валюты] =" +
                            rdrBuy1[5] + " AND [Курс валюты].[Дата] =#" +
                            kostyl2 + "#";

                        dbcom1 = new OleDbCommand(getKurs);
                        dbcom1.Connection = dbase;
                        OleDbDataReader dr1 = dbcom1.ExecuteReader();

                        dr1.Read();
                        row.Cells["Column1"].Value = dr[0];
                        row.Cells["S1"].Value = rdrBuy1[7].ToString();
                        row.Cells["S2"].Value = dr1[2].ToString();
                        row.Cells["S3"].Value = 0;
                        row.Cells["S4"].Value = 0;
                        row.Cells["S6"].Value = rdrBuy1[3].ToString();
                        row.Cells["S7"].Value = int.Parse(row.Cells["S1"].Value.ToString())
                            * double.Parse(row.Cells["S2"].Value.ToString());
                        sellResult += double.Parse(row.Cells["S7"].Value.ToString());
                    }
                    //string zaprosSell = "SELECT * FROM[Сделка] WHERE " +
                    //" [Сделка].[Код проданной валюты] = " + i +
                    //" AND  [Сделка].[Дата сделки] between #" +
                    //from.ToShortDateString().Replace(',', '/') +
                    //"# AND #" + to.ToShortDateString().Replace(',', '/') + "#";
                    //dbcom = new OleDbCommand(zaprosBuy);
                    //dbcom.Connection = dbase;
                    //OleDbDataReader rdrSell = dbcom.ExecuteReader();

                   
                    //while (rdrSell.Read())
                    //{

                    //}


                    int rowId1 = dataGridView1.Rows.Add();
                    DataGridViewRow row1 = dataGridView1.Rows[rowId1];
                    row1.Cells["Column1"].Value = "Итого по валюте:";
                    row1.Cells["S7"].Value = sellResult - buyResult;

                    res += sellResult - buyResult;
                    //вставить выручку
                    //приписать дату

                    //rdrSell.Close();
                    rdrBuy.Close();
                }

                label8.Text = res.ToString();

            } else
            {
                MessageBox.Show("Ошибка: неправильный временной промежуток");
            }

            //char[] myChar = { ' ', '0', ':' };
            
            //string query = "SELECT * FROM [Сделка] WHERE " + 
            //    " [Сделка].[Код купленной валюты] = " + textBox1.Text +
            //    " AND  [Сделка].[Дата сделки] between #" + from + "# AND #" + to + "#";
            //dbcom = new OleDbCommand(query);
            //dbcom.Connection = dbase;
            //OleDbDataReader reader = dbcom.ExecuteReader();

            //if (reader.HasRows)
            //{
            //    while (reader.Read())
            //    {
            //        if (Int32.Parse(reader.GetValue(5).ToString()) == 0)
            //        {
            //            query = "SELECT * FROM [Курс валюты] WHERE [Курс валюты].[Код валюты] = " +
            //                "" + reader.GetValue(6).ToString() + " AND [Курс валюты].[Дата] = #" +
            //                reader.GetValue(3).ToString().Replace('.', '/').TrimEnd(myChar) + "#";
            //        } else
            //        {
            //            query = "SELECT * FROM [Курс валюты] WHERE [Курс валюты].[Код валюты] = " +
            //                "" + reader.GetValue(5).ToString() + " AND [Курс валюты].[Дата] = #" +
            //                reader.GetValue(3).ToString().Replace('.', '/').TrimEnd(myChar) + "#";
            //        }
            //        dbcom1 = new OleDbCommand(query);
            //        dbcom1.Connection = dbase;
            //        OleDbDataReader red = dbcom1.ExecuteReader();
            //        dataGridView1.Rows.Add(red.GetValue(0).ToString());
            //    }
            //}
            //reader.Close();
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
