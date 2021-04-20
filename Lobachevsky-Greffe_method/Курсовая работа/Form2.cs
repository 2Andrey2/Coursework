using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Курсовая_работа
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        
        int n;
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            // при изменении значения переменной n изменяются размеры первой и второй таблицы для ввода
            n = Convert.ToInt32(numericUpDown1.Value);
            dataGridView1.RowCount = 1;
            dataGridView1.ColumnCount = n+1;
            for (int i = 0; i < dataGridView1.ColumnCount; i++)
            {
                if (i != dataGridView1.ColumnCount - 1)
                {
                    dataGridView1.Columns[i].Name = "При " + (i + 1) + " x";
                }
                else
                {
                    dataGridView1.Columns[i].Name = "Свободный     член";
                }
            }
                label2.Text = "Введите " + (n + 1) + " коэффициентов уравнения, начиная с";
                for (int h = 0; h < dataGridView1.ColumnCount; h++)
                {
                    dataGridView1.Columns[h].Width = 110;
                }
        }

        string[,] vis = new string [100,100];

        private void button3_Click(object sender, EventArgs e)
        {
            // счётчики
            int i, j, k, s;
            // максимальное количество итераций
            int iter;
            iter = 10000;
            // точность 
            double eps,z;
            bool fl;
            // массив a
            double[,] a = new double[iter,2*n+1];
            // массив  S
            double[,] S = new double[iter,n+1];
            // результирующий массив  корней уравнения
            double[,] X = new double[iter,n+1];
            // запись решения
            //нулевая итерация для массива а
            //ввод коэффициентов при неизвестных в уравнении в массив a
                for (i = 0; i <= n; i++)
                {
                    a[0,i] = Convert.ToDouble(dataGridView1.Rows[0].Cells[i].Value);
                }
            //заполнение оставшейся части массива а нулями
                for (i = n+1; i <= 2*n; i++)
                {
                    a[0, i] = 0;
                }
             //ввод точности вычислений
                eps = Convert.ToDouble(textBox1.Text);
             //первая итерация для массивов а и X, нулевая итерация для массива S
                k = 1;
                S[0, 0] = 0;
                for (i = 1; i < n; i++)
                {
                    S[0, i] = 0;
                    for (s = 1; s <= i; s++)
                        S[0, i] = S[0, i] + Math.Pow((-1), s) * 2 * a[0, i - s] * a[0, i + s];
                }
                S[0, n] = 0;
                for (i = 0; i <= n; i++)
                    a[1, i] = Math.Pow((a[0, i]), 2) + S[0, i];
                for (i = n + 1; i <= 2 * n; i++)
                {
                    a[1, i] = 0;
                }
                for (i = 1; i <= n; i++)
                    {
                    z = Math.Pow(2, k);
                    X[1, i] = Math.Pow((a[1, i] / a[1, i - 1]),(1.0/(z)));
                    vis[i, k] = "(" + a[k, i] + "/" + a[k, i - 1] + ")^" + 1.0 / (z);
                    if (Math.Abs(Func((-X[1, i]), a, n)) < Math.Abs(Func((X[1, i]), a, n)))
                    X[1, i] = - X[1, i];
                    }
                
            do
                {
                // k-тая итерация для массивов а и X, k-1 итерация для массива S
                k = k + 1;
                S[k-1, 0] = 0;
                for (i = 1; i < n; i++)
                {
                    S[k-1, i] = 0;
                    for (s = 1; s <= i; s++)
                        S[k-1, i] = S[k-1, i] + Math.Pow((-1), s) * 2 * a[k-1, i - s] * a[k-1, i + s];
                }
                S[k-1, n] = 0;
                for (i = 0; i <= n; i++)
                a[k, i] = Math.Pow((a[k - 1, i]), 2) + S[k-1, i];
                for (i = n + 1; i <= 2 * n; i++)
                {
                    a[k, i] = 0;
                }

                for (i = 1; i <= n; i++)
                {
                z = Math.Pow(2, k);
                X[k, i] = Math.Pow((a[k, i] / a[k, i - 1]), (1.0 / (z)));
                vis[i, k] = "(" + a[k, i] + "/" + a[k, i - 1] + ")^" + 1.0 / (z);
                   
                    if (Math.Abs(Func((-X[k, i]),a,n)) < Math.Abs(Func((X[k, i]),a,n)))
                    X[k, i] = - X[k, i];
                }                
                
            fl = true;
            for (i = 1; i <= n; i++)
                if (Math.Abs(X[k, i] - X[k - 1, i]) < eps)
                    fl = false;
               }

            while (fl == true);

            // задание количества строк и столбцов для вывода
            dataGridView4.RowCount = k;
            dataGridView4.ColumnCount = n;

            // вывод массива X
            for (i = 0; i < k; i++)
                {
                   for (j = 0; j < n; j++)
                   dataGridView4.Rows[i].Cells[j].Value = X[i+1, j+1];
                  
                }
            for (int p = 0; p < iter; p++)
            {
                for (int q = 0; q < n + 1; q++)
                Данные.S[p,q] = a[p,q];
            }
            Данные.long1 = iter;
            Данные.long2 = n + 1;
            for (int h = 0; h < dataGridView4.ColumnCount; h++)
            {
                dataGridView4.Columns[h].Width = 200;
            }
        }
        private double Func(double xi,double[,] a, int n)
        {
            double f;
            int j;
            f = 0;
            for (j = 0; j <= n; j++)
                f = f + a[0, j] * Math.Pow(xi,(n-j));
            return f;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Visible = false;
            Form1 f = new Form1();
            f.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Расчеты f = new Расчеты();
            f.Show();
        }

        private void dataGridView4_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            int i = e.ColumnIndex;
            int j = e.RowIndex;
            textBox2.Width = Convert.ToInt32(vis[i + 1, j + 1].Length * 10);
            textBox2.Text = vis[i+1,j+1];
            textBox2.Location = new Point(Cursor.Position.X - this.Location.X , Cursor.Position.Y - this.Location.Y);
            textBox2.Visible = true;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            textBox2.Visible = false;
            timer1.Stop();
        }

    }
}
