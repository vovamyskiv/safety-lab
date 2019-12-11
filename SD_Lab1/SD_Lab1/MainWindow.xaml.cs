using System;
using System.Collections.Generic;
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

using System.IO;

namespace SD_Lab1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Run_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                int min = 0, max = 0;

                if(!Int32.TryParse(Input_Amount.Text, out int amount))
                {
                    throw new Exception("Amount is empty or not a number");
                }

                if (Limit_Check.IsChecked == true)
                {

                    if(!Int32.TryParse(Input_Min.Text, out min))
                    {
                        throw new Exception("Minimum is empty or not a number");
                    }

                    if (!Int32.TryParse(Input_Max.Text, out max))
                    {
                        throw new Exception("Maximum is empty or not a number");
                    }

                    if(min >= max)
                    {
                        throw new Exception("Min should be bigger tham Max");
                    }

                }
                
                var generator = new Algorithm(amount);

                IEnumerable<int> result = null;

                if (Limit_Check.IsChecked == true)
                {
                    result = generator.Run(min, max);
                }
                else
                {
                    result = generator.Run();
                }
                
                var output = String.Join(" ", result);

                var window = new ResultWindow();

                window.DataContext = output;

                window.Show();

            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Panel_StaticValues.DataContext = new
            {
                Algorithm.Modul,
                Algorithm.A,
                Algorithm.C,
                Algorithm.StartValue
            };
        }

        private void Limit_Check_Checked(object sender, RoutedEventArgs e)
        {
            Panel_Collapsed_Left.Visibility = Visibility.Visible;
            Panel_Collapsed_Right.Visibility = Visibility.Visible;
        }

        private void Limit_Check_Unchecked(object sender, RoutedEventArgs e)
        {
            Panel_Collapsed_Left.Visibility = Visibility.Collapsed;
            Panel_Collapsed_Right.Visibility = Visibility.Collapsed;
        }
    }
}
