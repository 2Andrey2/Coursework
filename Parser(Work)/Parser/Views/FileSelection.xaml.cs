using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;

namespace Parser
{
    /// <summary>
    /// Логика взаимодействия для FileSelection.xaml
    /// </summary>
    public partial class FileSelection : Page
    {
        Frame frame;
        public FileSelection(Frame fram)
        {
            InitializeComponent();
            frame = fram;
        }

        private void FileSelectionB_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Multiselect = true;
            file.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            if (file.ShowDialog() == DialogResult.OK)
            {
                frame.Navigate(new ParserSetup(file.FileNames));
            }
            else
            {
                System.Windows.MessageBox.Show("Ошибка");
            }
        }
    }
}
