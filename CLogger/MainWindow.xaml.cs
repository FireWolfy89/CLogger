 using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CLogger
{
   
    public partial class MainWindow : Window
    {
        Macros macro;
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            DateNameBox.Text = DateTime.Now.ToString("yy/MM/dd"); 
        }

        private void Calculate_Button(object sender, RoutedEventArgs e)
        {
            string dayText = DateNameBox.Text;
            string protText = ProtTextBox.Text;
            string carbsText = CarbsTextBox.Text;
            string fatText = FatTextBox.Text;

            try {
                    if (protText.Length > 4 || carbsText.Length > 4 || fatText.Length > 4)
                    {
                        throw new ArgumentException();
                    }
                    else if (protText.Contains("-") || carbsText.Contains("-") || fatText.Contains("-"))
                    {
                        throw new ArgumentException(); 
                    }
                    int prot = Convert.ToInt32(protText);
                    int carbs = Convert.ToInt32(carbsText);
                    int fat = Convert.ToInt32(fatText);

                    string cal = (prot * 4 + carbs * 4 + fat * 9).ToString();

                    Result res = new Result(dayText, protText, carbsText, fatText, cal);
                    res.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    res.ShowDialog();
            }

            catch (FormatException)
            {
                MessageBox.Show("Invalid input. Please provide a valid format!");
                return;
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Invalid input. Number is too big/negative!");
            }



        }

        private void Logs_Button(object sender, RoutedEventArgs e)
        {
            Logs logs = new Logs();
            logs.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            logs.ShowDialog();
        }

       
    }
}