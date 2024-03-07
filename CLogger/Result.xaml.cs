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
            ResultTextBox.Text = cal;

        }

        private void Save_Button(object sender, RoutedEventArgs e)
        {
            Macros macro = new Macros()
            {
                Date = DateTime.Parse(RDayTextBox.Text),
                Protein = RProteinTextBox.Text,
                Carb = RCarbsTextBox.Text,
                Fat = RFatTextBox.Text,
                Result = ResultTextBox.Text,
            };

            

            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Macros>();
                connection.Insert(macro);

            }

            this.Close();
            
        }


    }
}
