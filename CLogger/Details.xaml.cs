﻿using SQLite;
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
        public Details(Macros macro)
        {
            InitializeComponent();

            this.macro = macro;

            DDateNameBox.Text = macro.Date.ToString("yyyy.MM.dd");
            DProtTextBox.Text = macro.Protein;
            DCarbsTextBox.Text = macro.Carb;
            DFatTextBox.Text = macro.Fat;
        }
        private void Update_Button(object sender, RoutedEventArgs e)
        {
            macro.Date = DateTime.Parse(DDateNameBox.Text);
            macro.Protein = DProtTextBox.Text;
            macro.Carb = DCarbsTextBox.Text;
            macro.Fat = DFatTextBox.Text;

            try
            {
                if (macro.Protein.Length > 4 || macro.Carb.Length > 4 || macro.Fat.Length > 4)
                {
                    throw new ArgumentException();
                }
                else if (macro.Protein.Contains("-") || macro.Carb.Contains("-") || macro.Fat.Contains("-"))
                {
                    throw new ArgumentException();
                }
                int prot = Convert.ToInt32(macro.Protein);
                int carbs = Convert.ToInt32(macro.Carb);
                int fat = Convert.ToInt32(macro.Fat);

                macro.Result = (prot * 4 + carbs * 4 + fat * 9).ToString();

                using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
                {
                    connection.CreateTable<Macros>();
                    connection.Update(macro);
                   
                    

                }
            }

            catch(FormatException)
            {
                MessageBox.Show("Format error, please provide a valid number/text");
                return;
            }

            catch (ArgumentException)
            {
                MessageBox.Show("The input number is too big/negative!");
            }



            Close();
        }

        private void Delete_Button(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Macros>();
                connection.Delete(macro);
                
            }

            Close();
        }
    }
}
