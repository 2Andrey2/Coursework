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
    public partial class Пароль : Form
    {
        public Пароль()
        {
            InitializeComponent();
        }

        private void maskedTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (maskedTextBox1.Text == "123654")
            {
                Основная_форма.Пароль1 = "Верно";
                this.Close();
            }
        }
    }
}
