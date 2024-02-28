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
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void Calculate_Button(object sender, RoutedEventArgs e)
        {
            string dayText = (DayComboBox.SelectedItem as ComboBoxItem).Content.ToString();
            string protText = ProtTextBox.Text;
            string carbsText = CarbsTextBox.Text;
            string fatText = FatTextBox.Text;
            
            try {
                if (protText.Length > 4 || carbsText.Length > 4 || fatText.Length > 4)
                {
                    throw new ArgumentException();
                }
                    int prot = Convert.ToInt32(protText);
                    int carbs = Convert.ToInt32(carbsText);
                    int fat = Convert.ToInt32(fatText);

                    string cal = (prot * 4 + carbs * 4 + fat * 9).ToString();

                    Result res = new Result(dayText, protText, carbsText, fatText, cal);
                    res.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    this.Close();
                    res.ShowDialog();
                
                
            }

            catch (FormatException)
            {
                MessageBox.Show("Format error, please provide a valid number");
                return;
            }
            catch (ArgumentException)
            {
                MessageBox.Show("The input number is too big.");
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