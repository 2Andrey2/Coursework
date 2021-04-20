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
    public partial class Расчеты : Form
    {
        public Расчеты()
        {
            InitializeComponent();
        }

        private void Расчеты_Activated(object sender, EventArgs e)
        {
            dataGridView1.ColumnCount = Convert.ToInt32(Данные.long2);
            int h = dataGridView1.ColumnCount;
            for (int i = 0; i < Convert.ToInt32(Данные.long1); i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    if (Данные.S[i,j] == 0)
                    {
                        h = h - 1;
                        if (h == 0)
                        {
                            dataGridView1.RowCount = i;
                            goto metka1;
                        }
                    }
                }
            }
            metka1:;
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    dataGridView1[j,i].Value = Данные.S[i,j];
                }
            }
            for (int i = 0; i < Convert.ToInt32(Данные.long2); i++)
            {
                dataGridView1.Columns[i].Width = 200;
                dataGridView1.Columns[i].Name = "x" + (i + 1);
            }
        }
    }
}
