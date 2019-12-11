using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SD_Lab1
{
    /// <summary>
    /// Логика взаимодействия для ResultWindow.xaml
    /// </summary>
    public partial class ResultWindow : Window
    {
        public ResultWindow()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            if (dialog.ShowDialog() == true)
            {
                using (var stream = new StreamWriter(dialog.FileName, false, Encoding.Default))
                {
                    stream.Write((string)DataContext);
                    stream.Close();
                }

                Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Output_Result.AppendText((string)DataContext);
        }

        private void Period_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Period: " + (new Algorithm(0).FindPeriod()).ToString());
        }
    }
}
