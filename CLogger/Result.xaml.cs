using SQLite;
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
using System.Windows.Shapes;


namespace CLogger
{
    /// <summary>
    /// Interaction logic for Result.xaml
    /// </summary>
    public partial class Result : Window
    {
        public Result(string day,string prot, string carbs, string fat, string cal)
        {
            
            InitializeComponent();

            RDayTextBox.Text = day;
            RProteinTextBox.Text = prot; 
            RCarbsTextBox.Text = carbs;
            RFatTextBox.Text = fat;
            ResultTextBox.Content = cal;

        }

        private void Save_Button(object sender, RoutedEventArgs e)
        {
            Macros macro = new Macros()
            {
                Date = DateTime.Parse(RDayTextBox.Text),
                Protein = RProteinTextBox.Text,
                Carb = RCarbsTextBox.Text,
                Fat = RFatTextBox.Text,
                Result = (string)ResultTextBox.Content,
            };

            

            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Macros>();
                connection.Insert(macro);

            }

            this.Close();
            
        }

        private void Details_Button_Click(object sender, RoutedEventArgs e)
        {
            string prot = RProteinTextBox.Text;
            string carbs = RCarbsTextBox.Text;  
            string fat = RFatTextBox.Text;
            string cal = (string)ResultTextBox.Content;
            

            SpecDetails specDetails = new SpecDetails(prot, carbs, fat, cal);
            specDetails.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            specDetails.ShowDialog();   
        }
    }
}
