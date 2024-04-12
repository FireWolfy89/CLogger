using CLogger.Classes;
using SQLite;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
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
    /// Interaction logic for Progress.xaml
    /// </summary>
    public partial class Progress : Window
    {
        private CommonDataValidation dataValidation = new CommonDataValidation();
        private List<Goals> goalslist;
        private Goals goal;

        public Progress()
        {
            InitializeComponent();
            ReadDataBase();
        }

        void ReadDataBase()
        {
            using (SQLite.SQLiteConnection conn = new SQLite.SQLiteConnection(App.databasePath))
            {
                conn.CreateTable<Goals>();
                goalslist = conn.Table<Goals>().ToList();

                goal = goalslist.FirstOrDefault();

                if (goal != null)
                {
                    CWeight.Text = goal.CWeight;
                    GWeight.Text = goal.GWeight;
                    Activity.Text = goal.Activity;

                    kcalBox.Text = goal.Kcal;
                    ProtBox.Text = goal.Prot;
                    CarbsBox.Text = goal.Carbs;
                    FatBox.Text = goal.Fat;
                    WeightChange.Text = goal.TextInfo;
                    KgBox.Text = goal.Kg;
                }
                else
                {
                    CWeight.Text = string.Empty;
                    GWeight.Text = string.Empty;


                    kcalBox.Text = string.Empty;
                    ProtBox.Text = string.Empty;
                    CarbsBox.Text = string.Empty;
                    FatBox.Text = string.Empty;
                    WeightChange.Text = string.Empty;
                    KgBox.Text = string.Empty;  
                }

            }
        }

        private void Calculate_Plan(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataValidation.IsInputValidProgress(CWeight.Text, GWeight.Text, CWeight, GWeight))
                {
                    double[] Conversion = dataValidation.ConvertToDoubleArray(CWeight.Text, GWeight.Text, Activity.Text);

                    if (Conversion[0] > Conversion[1])
                    {
                        dataValidation.Macro_Calculate(Conversion, 2.2, kcalBox, ProtBox, CarbsBox, FatBox);
                        WeightChange.Text = "The amount of kgs that you have to lose/week: ";
                        KgBox.Text = "1";
                    }

                    else if (Conversion[0] < Conversion[1])
                    {
                        dataValidation.Macro_Calculate(Conversion, 1.8, kcalBox, ProtBox, CarbsBox, FatBox);
                        WeightChange.Text = "The amount of kgs that you have to gain/week: ";
                        KgBox.Text = "0.25 - 0.5";
                    }
                    

                }

            }
            catch (Exception)
            {
                MessageBox.Show("Please input only positive numbers with a maximum length of 5 digits.");
            }
        }


        private void Clear_Goals(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Goals>();


                var existingGoal = connection.Table<Goals>().FirstOrDefault();

                if (existingGoal != null)
                {

                    connection.Delete(existingGoal);
                    
                    MessageBox.Show("The goal has been cleared!");

                }
                else
                {
                    MessageBox.Show("There are no goals to clear!");
                }
            }
  
            ReadDataBase();
        }

        private void Set_Goals(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dataValidation.IsInputValidProgress(CWeight.Text, GWeight.Text, CWeight, GWeight))
                {
                    using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
                    {
                        connection.CreateTable<Goals>();


                        var existingGoal = connection.Table<Goals>().FirstOrDefault();

                        if (existingGoal != null)
                        {

                            existingGoal.CWeight = CWeight.Text;
                            existingGoal.GWeight = GWeight.Text;
                            existingGoal.Activity = Activity.Text;
                            existingGoal.Prot = ProtBox.Text;
                            existingGoal.Carbs = CarbsBox.Text;
                            existingGoal.Fat = FatBox.Text;
                            existingGoal.Kcal = kcalBox.Text;
                            existingGoal.TextInfo = WeightChange.Text;
                            existingGoal.Kg = KgBox.Text;

                            connection.Update(existingGoal);
                            MessageBox.Show("The goal has been updated!");
                        }
                        else
                        {

                            Goals goal = new Goals
                            {
                                CWeight = CWeight.Text,
                                GWeight = GWeight.Text,
                                Activity = Activity.Text,
                                Prot = ProtBox.Text,
                                Carbs = CarbsBox.Text,
                                Fat = FatBox.Text,
                                Kcal = kcalBox.Text,
                                TextInfo = WeightChange.Text,
                                Kg = KgBox.Text
                            };

                            connection.Insert(goal);
                            MessageBox.Show("The goal has been set!");
                        }
                    }
                }

            }
            catch(Exception)
            {
                MessageBox.Show("Please input only positive numbers with a maximum length of 5 digits.");
            }


        }

    }
    
}
