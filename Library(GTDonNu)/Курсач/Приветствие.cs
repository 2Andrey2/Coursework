using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Курсач
{
    public partial class Приветствие : Form
    {
        public Приветствие()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Основная_форма.n = "Книги";
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Основная_форма.n = "Читатели";
            this.Close();
        }

        private void Приветствие_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Environment.Exit(0);
        }
    }
}
