using LiveCharts.Defaults;
using LiveCharts.Wpf;
using LiveCharts;
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
using CLogger.Classes;

namespace CLogger
{
    /// <summary>
    /// Interaction logic for Result1.xaml
    /// </summary>
    public partial class Result1 : Window
    {
        public SeriesCollection SeriesCollection { get; set; }

        public Result1(string day, string prot, string carbs, string fat, string cal)
        {

            InitializeComponent();

            R1Date.Text = day;
            R1Prot.Text = prot;
            R1Carbs.Text = carbs;
            R1Fat.Text = fat;
            R1Res.Text = cal;

            SeriesCollection = new SeriesCollection
            {
                new PieSeries
                {
                    Title= "Protein(g)",
                    Values= new ChartValues<ObservableValue> { new ObservableValue(double.Parse(prot))},
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Carbohydrate(g)",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(double.Parse(carbs)) },
                    DataLabels = true
                },
                new PieSeries
                {
                    Title = "Fat(g)",
                    Values = new ChartValues<ObservableValue> { new ObservableValue(double.Parse(fat)) },
                    DataLabels = true
                }

            };

            DataContext = this;

        }

        private void Save_Button(object sender, RoutedEventArgs e)
        {
            DateTime date = DateTime.Parse(R1Date.Text);

            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Macros>();
                var existingRecord = connection.Table<Macros>().FirstOrDefault(m => m.Date == date);

                
                if (existingRecord == null)
                {
                    Macros macro = new Macros()
                    {
                        Date = date,
                        Protein = R1Prot.Text,
                        Carb = R1Carbs.Text,
                        Fat = R1Fat.Text,
                        Result = R1Res.Text,
                    };

                    connection.Insert(macro);
                }
                else
                {
                    MessageBox.Show("A record with this date already exists.");
                }
            }

            this.Close();
        }


    }
}

