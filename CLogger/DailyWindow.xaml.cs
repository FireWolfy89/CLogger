using CLogger.Classes;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
    /// Interaction logic for DailyWindow.xaml
    /// </summary>
    public partial class DailyWindow : Window
    {
        public ObservableCollection<Daily> DailyList { get; set; }

        private IDataValidation dataValidation = new CommonDataValidation();

        private Daily SelectedDaily;

        public DailyWindow()
        {
            InitializeComponent();
            ReadDataBase();
            DailyDate.Content = DateTime.Now.ToString("yyyy.MM.dd dddd");
        }

        private void DailyCalc(object sender, RoutedEventArgs e)
        {
            try
            {
                dataValidation.IsInputValid(DailyProt.Text, DailyCarb.Text, DailyFat.Text);

                string cal = dataValidation.ConvertToDouble(DailyProt.Text, DailyCarb.Text, DailyFat.Text);

                DailyRes.Text = cal;

                Daily daily = new Daily()
                {
                    Name = DailyFood.Text,
                    Protein = DailyProt.Text,
                    Carb = DailyCarb.Text,
                    Fat = DailyFat.Text,
                    Result = DailyRes.Text,
                };

                using (SQLiteConnection connection = new SQLiteConnection(DB2.databasePath))
                {
                    connection.CreateTable<Daily>();
                    connection.Insert(daily);
                }
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

            ReadDataBase();
            Clear_Button(null, null);

        }

        
        private void ReadDataBase()
        {
            List<Daily> daily;
            using (SQLiteConnection connection = new SQLiteConnection(DB2.databasePath))
            {
                connection.CreateTable<Daily>();
                daily = connection.Table<Daily>().ToList();
            }

            if (daily != null)
            {
                DailyList = new ObservableCollection<Daily>(daily);
                DailyListView.ItemsSource = DailyList;
            }
        }

        //Only digits can be the input of the "Food" Textbox
        private void PreviewText_Input(object sender, TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (char.IsDigit(c))
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        private void DailyListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedDaily = (Daily)DailyListView.SelectedItem;

            if (SelectedDaily != null)
            {
                DailyCalcButton.Visibility = Visibility.Collapsed;
                DailyFood.Text = SelectedDaily.Name;
                DailyProt.Text = SelectedDaily.Protein;
                DailyCarb.Text = SelectedDaily.Carb;
                DailyFat.Text = SelectedDaily.Fat;
                DailyRes.Text = SelectedDaily.Result;
            }
        }

        private void Delete_Button(object sender, RoutedEventArgs e)
        {

            if (SelectedDaily != null)
            {
                using (SQLiteConnection conn = new SQLiteConnection(DB2.databasePath))
                {
                    conn.CreateTable<Daily>();
                    conn.Delete(SelectedDaily);
                }
            }

            ReadDataBase();
            DailyCalcButton.Visibility = Visibility.Visible;
            Clear_Button(null, null);
        }

        private void Update_Button(object sender, RoutedEventArgs e)
        {
            
            if (SelectedDaily != null)
            {
                try
                {
                    SelectedDaily.Name = DailyFood.Text;
                    SelectedDaily.Protein = DailyProt.Text;
                    SelectedDaily.Carb = DailyCarb.Text;
                    SelectedDaily.Fat = DailyFat.Text;

                    dataValidation.IsInputValidDaily(DailyFood.Text, DailyProt.Text, DailyCarb.Text, DailyFat.Text);

                    SelectedDaily.Result = dataValidation.ConvertToDouble(DailyProt.Text, DailyCarb.Text, DailyFat.Text);

                    MessageBox.Show("The total calories of the food after update: " + SelectedDaily.Result);

                    using (SQLiteConnection conn = new SQLiteConnection(DB2.databasePath))
                    {
                        conn.CreateTable<Daily>();
                        conn.Update(SelectedDaily);
                    }
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


            ReadDataBase();
            Clear_Button(null, null);
            DailyCalcButton.Visibility = Visibility.Visible;

        }

        private void Clear_Button(object sender, RoutedEventArgs e)
        {
            DailyFood.Text = string.Empty;
            DailyProt.Text = string.Empty;
            DailyCarb.Text = string.Empty;
            DailyFat.Text = string.Empty;
            DailyRes.Text = string.Empty;

            DailyCalcButton.Visibility = Visibility.Visible;
        }

        private void ShowButton(object sender, RoutedEventArgs e)
        {
            double totalProtein = 0;
            double totalCarb = 0;
            double totalFat = 0;
            double totalResult = 0;

            foreach (var daily in DailyList)
            {
                
                if (double.TryParse(daily.Protein, out double protein) &&
                    double.TryParse(daily.Carb, out double carb) &&
                    double.TryParse(daily.Fat, out double fat) &&
                    double.TryParse(daily.Result, out double result))
                {
                    totalProtein += protein;
                    totalCarb += carb;
                    totalFat += fat;
                    totalResult += result;
                }
            }
            string date = DateTime.Now.ToString("yyyy.MM.dd");

            Result1 resultWindow = new Result1(date, 
                                    totalProtein.ToString(), 
                                    totalCarb.ToString(), 
                                    totalFat.ToString(), 
                                    totalResult.ToString());

            resultWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Close();
            resultWindow.Show();
            

        }
    }
}
