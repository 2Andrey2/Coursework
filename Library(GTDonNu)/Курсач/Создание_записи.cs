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
    public partial class Создание_записи : Form
    {

        public static string ConnectBD = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=База.mdb";
        private OleDbConnection myConnection;

        public Создание_записи()
        {
            InitializeComponent();

            myConnection = new OleDbConnection(ConnectBD);
            myConnection.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string fard = "INSERT INTO Книги ( Название, Автор, Жанр, Издательство, Количество, Выдано ) VALUES('"
                    + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "'," + textBox6.Text + "," + 0 + ")";
                OleDbCommand comand = new OleDbCommand(fard, myConnection);
                OleDbDataReader fore = comand.ExecuteReader();
                MessageBox.Show("Запись успешно добавлена");
            }
            catch
            {
                MessageBox.Show("Введите корректные данные");
            }
        }
    }
}
