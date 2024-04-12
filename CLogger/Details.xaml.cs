using CLogger.Classes;
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
    /// Interaction logic for Details.xaml
    /// </summary>
    public partial class Details : Window
    {
        Macros macro;
        private IDataValidation dataValidation = new CommonDataValidation();

        public Details(Macros macro)
        {
            InitializeComponent();

            this.macro = macro;

            DDateNameBox.Content = macro.Date.ToString("yyyy.MM.dd");
            DProtTextBox.Text = macro.Protein;
            DCarbsTextBox.Text = macro.Carb;
            DFatTextBox.Text = macro.Fat;
        }
        private void Update_Button(object sender, RoutedEventArgs e)
        {
            macro.Date = DateTime.Parse((string)DDateNameBox.Content);
            macro.Protein = DProtTextBox.Text;
            macro.Carb = DCarbsTextBox.Text;
            macro.Fat = DFatTextBox.Text;

            try
            {
                dataValidation.IsInputValid(DProtTextBox.Text, DCarbsTextBox.Text, DFatTextBox.Text);

                macro.Result = dataValidation.ConvertToDouble(DProtTextBox.Text, DCarbsTextBox.Text, DFatTextBox.Text);

                using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
                {
                    connection.CreateTable<Macros>();
                    connection.Update(macro);
                }

                MessageBox.Show("Record updated! New calorie for the day is: " + macro.Result);
            }

            catch(FormatException)
            {
                MessageBox.Show("Format error, please provide a valid number/text");
                return;
            }

            catch (ArgumentException)
            {
                MessageBox.Show("Invalid input. Number is either negative or too big!");
            }



            Close();
            Logs1 log1 = new Logs1();
            log1.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            log1.Show();
        }

        private void Delete_Button(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Macros>();
                connection.Delete(macro);
                
            }
            
            MessageBox.Show("Log deleted!");
            Close();

        }
    }
}
