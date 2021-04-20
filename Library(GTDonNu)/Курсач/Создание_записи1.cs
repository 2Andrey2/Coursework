using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;


namespace Курсач
{
    public partial class Создание_записи1 : Form
    {

        public static string ConnectBD = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=База.mdb";
        private OleDbConnection myConnection;

        public Создание_записи1()
        {
            InitializeComponent();

            myConnection = new OleDbConnection(ConnectBD);
            myConnection.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        try
        {
            string fard = "SELECT Книга_выдана FROM Читатели WHERE Книга_выдана = '" + textBox6.Text + "'";
            OleDbCommand comand = new OleDbCommand(fard, myConnection);
            OleDbDataReader fore = comand.ExecuteReader();
            List<string[]> data = new List<string[]>();
            while (fore.Read())
            {
                data.Add(new string[1]);
                data[data.Count - 1][0] = fore[0].ToString();
            }
            double m = data.Count();

            fard = "SELECT Количество FROM Книги WHERE Название = '" + textBox6.Text + "'";
            comand = new OleDbCommand(fard, myConnection);
            fore = comand.ExecuteReader();
            string companyCode = "";
            while (fore.Read())
            {
                companyCode = fore.GetValue(0).ToString();
            }
            if (m >= Convert.ToDouble(companyCode))
            {
                MessageBox.Show("Книги " + textBox6.Text + " нет в наличии");
            }
            else
            {
                fard = "INSERT INTO Читатели ( Фамилия, Имя, Отчество, Дата_выдачи, Книга_выдана) VALUES('"
                    + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + maskedTextBox1.Text + "','" + textBox6.Text + "')";
                comand = new OleDbCommand(fard, myConnection);
                fore = comand.ExecuteReader();
                fard = "UPDATE Книги SET Выдано = Выдано + 1 WHERE Название ='" + textBox6.Text + "'";
                comand = new OleDbCommand(fard, myConnection);
                fore = comand.ExecuteReader();
                MessageBox.Show("Запись успешно добавлена");
            }
         }
        catch
        {
            MessageBox.Show("Введите корректные данные");
        }
        }
    }
}
