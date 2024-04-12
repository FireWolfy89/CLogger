using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CLogger.Classes
{
    //This is the class where I contain all my calculation logic and conversions/constraints

    class CommonDataValidation : IDataValidation
    {
        public string ConvertToDouble(string protText, string carbText, string fatText)
        {
            double prot = double.Parse(protText);
            double carb = double.Parse(carbText);
            double fat = double.Parse(fatText);
            string cal = (prot * 4 + carb * 4 + fat * 9).ToString("N1");

            return cal;
        }

        public bool IsInputValid(string protText, string carbText, string fatText)
        {
            if (protText.Length > 5 || carbText.Length > 5 || fatText.Length > 5)
            {
                throw new ArgumentException();
            }
            else if (protText.Contains("-") || carbText.Contains("-") || fatText.Contains("-"))
            {
                throw new ArgumentException();
            }
            return true;
        }


        public bool IsInputValidDaily(string food, string protText, string carbText, string fatText)
        {
            if (food.Length > 15 || protText.Length > 5 || carbText.Length > 5 || fatText.Length > 5)
            {
                throw new ArgumentException();
            }
            else if (protText.Contains("-") || carbText.Contains("-") || fatText.Contains("-"))
            {
                throw new ArgumentException();
            }
            return true;
        }

        public bool IsInputValidProgress(string CW, string GW, TextBox CWBox, TextBox GWBox)
        {
            if (CW.Length > 5 || GW.Length > 5 || CWBox.Text == string.Empty || GWBox.Text == string.Empty) throw new ArgumentException();

            else if (CW.Contains("-") || GW.Contains("-")) throw new ArgumentException();
            return true;
        }

        public double Activity_Level(object activityItem)
        {
            string activity = activityItem.ToString();

            switch (activity)
            {
                case "Active":
                    return 1.5;
                case "Sedentary":
                    return 1.3;
                case "Very Active":
                    return 1.9;
                default:
                    return double.Parse(activity);
            }
        }

        public double[] ConvertToDoubleArray(string Cweight, string Gweight, object Activity)
        {
            double CW = double.Parse(Cweight);
            double GW = double.Parse(Gweight);


            double[] array = { CW, GW, Activity_Level(Activity) };

            return array;
        }

        public void Macro_Calculate(double[] Conversion, double proteinMultiplier, TextBox kcalbox, TextBox protBox, TextBox carbsBox, TextBox fatBox)
        {
            {
                double kcal = (Conversion[1] * 22 * Activity_Level(Conversion[2]));
                kcalbox.Text = kcal.ToString("N1");


                double protein = Conversion[0] * proteinMultiplier;
                protBox.Text = protein.ToString("N1");


                double fat = (kcal * 0.20) / 9;
                fatBox.Text = fat.ToString("N1");


                double carbs = (kcal - ((protein * 4) + (fat * 9))) / 4;
                carbsBox.Text = carbs.ToString("N1");
            }

        }
    }



}

