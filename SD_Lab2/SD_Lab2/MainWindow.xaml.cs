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
using System.Windows.Navigation;
using System.Windows.Shapes;

using SD_Lab2.Hash;

namespace SD_Lab2
{
    public partial class MainWindow : Window
    {
        private MD5 md5 = new MD5();

        public MainWindow()
        {
            InitializeComponent();
        }

        private string BytesToHex(byte[] array)
        {
            return BitConverter.ToString(array).Replace("-", "");
        }



        //-----------------------

        private void OpenPanel_Click(object sender, RoutedEventArgs e)
        {
            if(Panel_Check.Visibility == Visibility.Collapsed)
            {
                Panel_Check.Visibility = Visibility.Visible;
            }
            else
            {
                Panel_Check.Visibility = Visibility.Collapsed;
            }
        }

        private void CheckFile_Click(object sender, RoutedEventArgs e)
        {
            if(Output_FileMessage.Text.Length == 0 || Output_FileHash.Text.Length == 0)
            {
                MessageBox.Show("Перед перевіркою виберіть файли з повідомленням і хешем");
                return;
            }

            var bytesHash = md5.GetHash(_FileMessageBuffer);

            var hexHash = BytesToHex(bytesHash);

            if (hexHash.CompareTo(_FileHashBuffer) == 0)
            {
                MessageBox.Show("Файл цілісний");
            }
            else
            {
                MessageBox.Show("Файл НЕ цілісний");
            }
        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {
            var bytes = md5.GetHash(Input_Text.Text);
            Output_Hash.Text = BytesToHex(bytes);
        }

        private string _FileMessageBuffer;

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            if(dialog.ShowDialog() == true)
            {
                using (var stream = new StreamReader(dialog.FileName, System.Text.Encoding.Default))
                {
                    _FileMessageBuffer = stream.ReadToEnd();
                    stream.Close();
                }
            }

            Output_FileMessage.Text = dialog.FileName;
        }

        private string _FileHashBuffer;

        private void OpenHashFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            if (dialog.ShowDialog() == true)
            {
                using (var stream = new StreamReader(dialog.FileName, System.Text.Encoding.Default))
                {
                    _FileHashBuffer = stream.ReadToEnd();
                    stream.Close();
                }
            }

            Output_FileHash.Text = dialog.FileName;
        }

        private void SaveFile_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();

            if (dialog.ShowDialog() == true)
            {
                using (var stream = new StreamWriter(dialog.FileName, false, Encoding.Default))
                {
                    stream.Write(Output_Hash.Text);
                    stream.Close();
                }
            }
        }
    }
}
