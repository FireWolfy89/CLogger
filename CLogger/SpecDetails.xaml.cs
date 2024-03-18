using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
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
    /// Interaction logic for SpecDetails.xaml
    /// </summary>
    public partial class SpecDetails : Window
    {
        public SeriesCollection SeriesCollection { get; set; }

        public SpecDetails(string prot, string carbs, string fat, string cal)
        {
            InitializeComponent();
            KcalLabel.Content = "Total calories: " + cal + " kcal";


            SeriesCollection = new SeriesCollection
            {
               new PieSeries
               {
                   Title = "Protein(g)",
                   Values = new ChartValues<ObservableValue>{ new ObservableValue(double.Parse(prot))},
                   DataLabels = true
               },
               new PieSeries
               {
                   Title = "Carbohydrate(g)",
                   Values = new ChartValues<ObservableValue>{new ObservableValue(double.Parse(carbs))},
                   DataLabels = true,
               },

               new PieSeries
               {
                   Title = "Fat(g)",
                   Values = new ChartValues<ObservableValue>{new ObservableValue(double.Parse(fat))},
                   DataLabels = true
               }

            };

            DataContext = this;


        }

    }
}

