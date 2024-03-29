using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;
using CLogger.Classes;


namespace CLogger
{

    public partial class MainWindow : Window
    {
        Macros macro;

        private IDataValidation dataValidation = new CommonDataValidation();

        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            DateNameBox.Text = DateTime.Now.ToString("yyyy.MM.dd");
        }

        private void Calculate_Button(object sender, RoutedEventArgs e)
        {
            try
            {
                dataValidation.IsInputValid(ProtTextBox.Text, CarbsTextBox.Text, FatTextBox.Text);

                string cal = dataValidation.ConvertToDouble(ProtTextBox.Text, CarbsTextBox.Text, FatTextBox.Text);

                Result1 res = new Result1(DateNameBox.Text, ProtTextBox.Text, CarbsTextBox.Text, FatTextBox.Text, cal);
                res.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                res.ShowDialog();
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid Input, Please provide a valid format");
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Invalid input. Nukber is either negative or too big!");
            }

        }

        private void Logs_Button(object sender, RoutedEventArgs e)
        {
            Logs logs = new Logs();
            logs.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            logs.ShowDialog();
        }

        private void CalFood(object sender, RoutedEventArgs e)
        {
            DailyWindow daily = new DailyWindow();
            daily.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            daily.ShowDialog(); 
        }
    }
}