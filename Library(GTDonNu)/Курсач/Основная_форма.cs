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
    public partial class Основная_форма : Form
    {
        public static string ConnectBD = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=База.mdb";

        //@"C:\Documents and Settings\Comp\Рабочий стол\Курсач\База.mdb"

        private OleDbConnection myConnection;

        public Основная_форма()
        {
            InitializeComponent();

            myConnection = new OleDbConnection(ConnectBD);
            myConnection.Open();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (label3.Text == "Режим редактирования")
            {
                if (MessageBox.Show("Синхронизировать данные?", "Внимание", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation) == DialogResult.OK)
                {
                    string fard;
                    for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        if (comboBox1.Text == "Книги")
                        {
                            fard = "UPDATE Книги SET Название ='" + dataGridView1.Rows[i].Cells[1].Value + "',Автор ='" + dataGridView1.Rows[i].Cells[2].Value + "',Жанр ='" + dataGridView1.Rows[i].Cells[3].Value + "',Издательство ='" + dataGridView1.Rows[i].Cells[4].Value + "',Количество =" + dataGridView1.Rows[i].Cells[5].Value + ",Выдано =" + dataGridView1.Rows[i].Cells[6].Value + " WHERE Номер =" + dataGridView1.Rows[i].Cells[0].Value;
                            OleDbCommand comand = new OleDbCommand(fard, myConnection);
                            OleDbDataReader fore = comand.ExecuteReader();
                        }
                        if (comboBox1.Text == "Читатели")
                        {
                            fard = "UPDATE Читатели SET Фамилия ='" + dataGridView1.Rows[i].Cells[1].Value + "',Имя ='" + dataGridView1.Rows[i].Cells[2].Value + "',Отчество ='" + dataGridView1.Rows[i].Cells[3].Value + "',Дата_выдачи ='" + dataGridView1.Rows[i].Cells[4].Value + "',Книга_выдана ='" + dataGridView1.Rows[i].Cells[5].Value + "' WHERE Номер =" + dataGridView1.Rows[i].Cells[0].Value;
                            OleDbCommand comand = new OleDbCommand(fard, myConnection);
                            OleDbDataReader fore = comand.ExecuteReader();
                        }
                    }
                    MessageBox.Show("Данные синхронизированы");
                }
            }
            myConnection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            string fard = comboBox2.Text;
            switch (fard)
            {
                case "Показать все книги":
                    fard = "SELECT * FROM книги";
                    break;
                case "Показать всех читателей":
                    fard = "SELECT * FROM Читатели";
                    break;
                case "Выбрать все книги жанра ...":
                    fard = "SELECT * FROM Книги WHERE Жанр LIKE '%" + textBox1.Text + "%'";
                    break;
                case "Выбрать все книги автора ...":
                    fard = "SELECT * FROM Книги WHERE Автор LIKE '%" + textBox1.Text + "%'";
                    break;
                case "Выбрать все книги издательства ...":
                    fard = "SELECT * FROM Книги WHERE Издательство LIKE '%" + textBox1.Text + "%'";
                    break;
                case "Выбрать всех читателей с именем ...":
                    fard = "SELECT * FROM Читатели WHERE Имя LIKE '%" + textBox1.Text + "%'";
                    break;
                case "Выбрать всех читателей с фамилией ...":
                    fard = "SELECT * FROM Читатели WHERE Фамилия LIKE '%" + textBox1.Text + "%'";
                    break;
                case "Выбрать всех читателей с отчеством ...":
                    fard = "SELECT * FROM Читатели WHERE Отчество LIKE '%" + textBox1.Text + "%'";
                    break;
                case "Выбрать всех читателей с выданой книгой ...":
                    fard = "SELECT * FROM Читатели WHERE Книга_выдана LIKE '%" + textBox1.Text + "%'";
                    break;
                case "Найти книгу с названием ...":
                    fard = "SELECT * FROM Книги WHERE Название LIKE '%" + textBox1.Text + "%'";
                    break;
            }
            OleDbCommand comand = new OleDbCommand(fard,myConnection);
            OleDbDataReader fore = comand.ExecuteReader();
            List<string[]> data = new List<string[]>();

            while (fore.Read())
            {
                if (comboBox1.Text == "Книги")
                {
                    data.Add(new string[7]);

                    data[data.Count - 1][0] = fore[0].ToString();
                    data[data.Count - 1][1] = fore[1].ToString();
                    data[data.Count - 1][2] = fore[2].ToString();
                    data[data.Count - 1][3] = fore[3].ToString();
                    data[data.Count - 1][4] = fore[4].ToString();
                    data[data.Count - 1][5] = fore[5].ToString();
                    data[data.Count - 1][6] = fore[6].ToString();
                }
                if (comboBox1.Text == "Читатели")
                {
                    data.Add(new string[6]);

                    data[data.Count - 1][0] = fore[0].ToString();
                    data[data.Count - 1][1] = fore[1].ToString();
                    data[data.Count - 1][2] = fore[2].ToString();
                    data[data.Count - 1][3] = fore[3].ToString();
                    data[data.Count - 1][4] = fore[4].ToString();
                    data[data.Count - 1][5] = fore[5].ToString();
                }
            }

            fore.Close();


            foreach (string[] s in data)
                dataGridView1.Rows.Add(s);
            textBox1.Text = "";
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            string a = comboBox1.Text;
            dataGridView1.DataMember = a;
            if (a == "Книги")
            {
                dataGridView1.ColumnCount = 7;
                dataGridView1.Columns[0].Name = "Номер";
                dataGridView1.Columns[1].Name = "Название";
                dataGridView1.Columns[2].Name = "Автор";
                dataGridView1.Columns[3].Name = "Жанр";
                dataGridView1.Columns[4].Name = "Издательство";
                dataGridView1.Columns[5].Name = "Количество";
                dataGridView1.Columns[6].Name = "Выдано";
                comboBox2.Items.Add("Показать все книги");
                comboBox2.Items.Add("Найти книгу с названием ...");
                comboBox2.Items.Add("Выбрать все книги жанра ...");
                comboBox2.Items.Add("Выбрать все книги автора ...");
                comboBox2.Items.Add("Выбрать все книги издательства ...");
            }
            if (a == "Читатели")
            {
                dataGridView1.ColumnCount = 6;
                dataGridView1.Columns[0].Name = "Номер";
                dataGridView1.Columns[1].Name = "Фамилия";
                dataGridView1.Columns[2].Name = "Имя";
                dataGridView1.Columns[3].Name = "Отчество";
                dataGridView1.Columns[4].Name = "Время выдачи";
                dataGridView1.Columns[5].Name = "Выдана книга";
                comboBox2.Items.Add("Показать всех читателей");
                comboBox2.Items.Add("Выбрать всех читателей с именем ...");
                comboBox2.Items.Add("Выбрать всех читателей с фамилией ...");
                comboBox2.Items.Add("Выбрать всех читателей с отчеством ...");
                comboBox2.Items.Add("Выбрать всех читателей с выданой книгой ...");
            }
            dataGridView1.Columns[0].Visible = false;
            button6_Click(sender, e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Проверка() == true)
            {
                if (comboBox1.Text == "Книги")
                {
                    Создание_записи ass = new Создание_записи();
                    ass.ShowDialog();
                }
                if (comboBox1.Text == "Читатели")
                {
                    Создание_записи1 ass = new Создание_записи1();
                    ass.ShowDialog();
                }
                button6_Click(sender, e);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Проверка() == true)
            {
                int w = 0;
                OleDbDataReader fore;
                OleDbCommand comand;
                string fard = "";
                double g = Convert.ToDouble(dataGridView1.Rows[Convert.ToInt32(dataGridView1.SelectedCells[0].RowIndex)].Cells[0].Value);
                string k = Convert.ToString(dataGridView1.Rows[Convert.ToInt32(dataGridView1.SelectedCells[0].RowIndex)].Cells[5].Value);
                if (comboBox1.Text == "Книги")
                {
                    fard = "DELETE FROM Книги WHERE Номер = " + g;
                    string fardd = "DELETE FROM Читатели WHERE Книга_выдана ='" + Convert.ToString(dataGridView1.Rows[Convert.ToInt32(dataGridView1.SelectedCells[0].RowIndex)].Cells[1].Value) + "'";
                    comand = new OleDbCommand(fardd, myConnection);
                    fore = comand.ExecuteReader();
                    w = 7;
                }
                if (comboBox1.Text == "Читатели")
                {
                    fard = "DELETE FROM Читатели WHERE Номер = " + g;
                    string fardd = "UPDATE Книги SET Выдано = Выдано - 1 WHERE Название ='" + k + "'";
                    comand = new OleDbCommand(fardd, myConnection);
                    fore = comand.ExecuteReader();
                    w = 6;
                }
                comand = new OleDbCommand(fard, myConnection);
                fore = comand.ExecuteReader();

                MessageBox.Show("Запись удалена");
                for (int i = 0; i < w; i++)
                {
                    dataGridView1.Rows[Convert.ToInt32(dataGridView1.SelectedCells[0].RowIndex)].Cells[i].Value = "#удалено";
                }
            }
        }

        static public string n = "";
        private void Основная_форма_Load(object sender, EventArgs e)
        {
            Приветствие h = new Приветствие();
            h.ShowDialog();
            comboBox1.Text = n;
            comboBox1_SelectedIndexChanged(sender,e);
            if (n == "Книги")
            { comboBox2.Text = "Показать все книги"; }
            if (n == "Читатели")
            { comboBox2.Text = "Показать всех читателей"; }
            button1_Click(sender, e);
            comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (Проверка() == true)
            {
                string fard;
                for (int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                {
                    if (comboBox1.Text == "Книги")
                    {
                        fard = "UPDATE Книги SET Название ='" + dataGridView1.Rows[i].Cells[1].Value + "',Автор ='" + dataGridView1.Rows[i].Cells[2].Value + "',Жанр ='" + dataGridView1.Rows[i].Cells[3].Value + "',Издательство ='" + dataGridView1.Rows[i].Cells[4].Value + "',Количество =" + dataGridView1.Rows[i].Cells[5].Value + ",Выдано =" + dataGridView1.Rows[i].Cells[6].Value + " WHERE Номер =" + dataGridView1.Rows[i].Cells[0].Value;
                        OleDbCommand comand = new OleDbCommand(fard, myConnection);
                        OleDbDataReader fore = comand.ExecuteReader();
                    }
                    if (comboBox1.Text == "Читатели")
                    {
                        fard = "UPDATE Читатели SET Фамилия ='" + dataGridView1.Rows[i].Cells[1].Value + "',Имя ='" + dataGridView1.Rows[i].Cells[2].Value + "',Отчество ='" + dataGridView1.Rows[i].Cells[3].Value + "',Дата_выдачи ='" + dataGridView1.Rows[i].Cells[4].Value + "',Книга_выдана ='" + dataGridView1.Rows[i].Cells[5].Value + "' WHERE Номер =" + dataGridView1.Rows[i].Cells[0].Value;
                        OleDbCommand comand = new OleDbCommand(fard, myConnection);
                        OleDbDataReader fore = comand.ExecuteReader();
                    }
                }
                MessageBox.Show("Данные синхронизированы");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            string fard = "";
            if(comboBox1.Text == "Книги")
            {
                fard = "SELECT * FROM книги";
            }
            if (comboBox1.Text == "Читатели")
            {
                fard = "SELECT * FROM Читатели";
            }
            OleDbCommand comand = new OleDbCommand(fard, myConnection);
            OleDbDataReader fore = comand.ExecuteReader();
            List<string[]> data = new List<string[]>();

            while (fore.Read())
            {
                if (comboBox1.Text == "Книги")
                {
                    data.Add(new string[7]);

                    data[data.Count - 1][0] = fore[0].ToString();
                    data[data.Count - 1][1] = fore[1].ToString();
                    data[data.Count - 1][2] = fore[2].ToString();
                    data[data.Count - 1][3] = fore[3].ToString();
                    data[data.Count - 1][4] = fore[4].ToString();
                    data[data.Count - 1][5] = fore[5].ToString();
                    data[data.Count - 1][6] = fore[6].ToString();
                }
                if (comboBox1.Text == "Читатели")
                {
                    data.Add(new string[6]);

                    data[data.Count - 1][0] = fore[0].ToString();
                    data[data.Count - 1][1] = fore[1].ToString();
                    data[data.Count - 1][2] = fore[2].ToString();
                    data[data.Count - 1][3] = fore[3].ToString();
                    data[data.Count - 1][4] = fore[4].ToString();
                    data[data.Count - 1][5] = fore[5].ToString();
                }
            }
            fore.Close();
            foreach (string[] s in data)
            dataGridView1.Rows.Add(s);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void руководствоToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Help.ShowHelp(this, "BD1.chm", HelpNavigator.TableOfContents);
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text != "Показать все книги" && comboBox2.Text != "Показать всех читателей")
            {
                textBox1.Text = "Введите параметр";
            }
            else
            {
                textBox1.Text = "";
            }
        }
        public static string Пароль1 = "";
        private void textBox1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }
        public bool Проверка()
        {
            if (label3.Text == "Режим чтения")
            {
                Пароль c = new Пароль();
                c.ShowDialog();
                if (Пароль1 == "Верно")
                {
                    label3.Text = "Режим редактирования";
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "Какой пароль" || textBox1.Text == "Какой пароль?" || textBox1.Text == "какой пароль" || textBox1.Text == "какой пароль?")
            {
                MessageBox.Show("123654");
            }
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            О_программе j = new О_программе();
            j.ShowDialog();
        }
    }
}
