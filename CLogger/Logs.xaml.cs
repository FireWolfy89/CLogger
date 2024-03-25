using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for Logs.xaml
    /// </summary>
    public partial class Logs : Window
    {
        public ObservableCollection<Macros> MacrosList { get; set; }

        public Logs()
        {
            InitializeComponent();
            ReadDataBase();

        }

        void ReadDataBase()
        {
            List<Macros> macros;
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.databasePath))
            {
                conn.CreateTable<Macros>();
                macros = conn.Table<Macros>().ToList();
            }

            if (macros != null)
            {

              MacrosList = new ObservableCollection<Macros>(macros);  
              macrosListView.ItemsSource = MacrosList;

            }
        }

        private void macrosListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Macros selectedMacro = (Macros)macrosListView.SelectedItem;

            if(selectedMacro != null)
            {
                Details newDetails = new Details(selectedMacro);
                newDetails.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                this.Close();
                newDetails.ShowDialog();

                Logs log = new Logs();
                log.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                log.ShowDialog();   

                ReadDataBase();
            }
        }
    }
}
