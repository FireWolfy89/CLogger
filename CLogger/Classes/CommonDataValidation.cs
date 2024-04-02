using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            string cal = (prot * 4 + carb * 4 + fat * 9).ToString();

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

    }



}

