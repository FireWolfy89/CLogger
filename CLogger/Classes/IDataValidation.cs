using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace CLogger.Classes
{
    //Interface added, so I can reduce the calculation logic code by instantiating the class(where it is implemented) in my window classes

    interface IDataValidation
    {
        bool IsInputValidDaily(string food, string protText, string carbText, string fatText);
        bool IsInputValid(string prot, string carb, string fat);
        bool IsInputValidProgress(string CW, string GW, TextBox CWBox, TextBox GWBox);

        string ConvertToDouble(string protText, string carbText, string fatText);
        double[] ConvertToDoubleArray(string Cweight, string Gweight, object Activiy);

        void Macro_Calculate(double[] Conversion, double proteinMultiplier, TextBox kcalbox, TextBox protBox, TextBox fatBox, TextBox carbsBox);

        double Activity_Level(object Activity);
    }


}
