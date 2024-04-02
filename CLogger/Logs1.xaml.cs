using CLogger.Classes;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    /// Interaction logic for Logs1.xaml
    /// </summary>
    public partial class Logs1 : Window
    {
        private Macros macro = new Macros();
        List<Macros> macros;
        private int currentDayIndex = 0;

        public Logs1()
        {
            InitializeComponent();
            ReadDataBase();
            
        }

        void ReadDataBase()
        {
            
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.databasePath))
            {
                conn.CreateTable<Macros>();
                macros = conn.Table<Macros>().ToList();
            }
            if (macros.Any())
            {
                DisplayDayData(currentDayIndex);

            }
            else MessageBox.Show("No logs available this time");
        }

        void DisplayDayData(int index)
        {
            try
            {
                if (index < 0)
                {
                    throw new Exception();
                }
                Macros currentDayData = macros[index];
                LDate.Content = currentDayData.Date.ToString("yyyy.MM.dd dddd");
                LProt.Text = currentDayData.Protein;
                LCarb.Text = currentDayData.Carb;
                LFat.Text = currentDayData.Fat;
                LRes.Text = currentDayData.Result;
            }

            catch(Exception)
            {
                NextDayButton.Visibility = Visibility.Collapsed;
                
            }
        }

        private void NextDay_Click(object sender, RoutedEventArgs e)
        {
           if (currentDayIndex < macros.Count - 1)
            {
                currentDayIndex++;
                DisplayDayData(currentDayIndex);
                PrevDayButton.Visibility = Visibility.Visible;
            }
            else
            {
                MessageBox.Show("No more logs available.");
                NextDayButton.Visibility= Visibility.Collapsed;
            }  
        }

        private void PrevDay_Click(object sender, RoutedEventArgs e)
        {
            if (currentDayIndex != 0)
            {
                NextDayButton.Visibility = Visibility.Visible;
                currentDayIndex--;
                DisplayDayData(currentDayIndex);
            }
            else
                currentDayIndex = 0;
        }

        private void Modify(object sender, RoutedEventArgs e)
        {
            
                Macros currentDayMacro = macros[currentDayIndex];

                Details det = new Details(currentDayMacro);
                det.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                this.Close();
                det.Show();
            

            ReadDataBase();

        }

        
    }
}
