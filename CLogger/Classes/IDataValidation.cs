﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CLogger.Classes
{
    //Interface added, so I can reduce the calculation logic code by instantiating the class(where it is implemented) in my window classes

    interface IDataValidation
    {
        bool IsInputValidDaily(string food, string protText, string carbText, string fatText);
        bool IsInputValid(string prot, string carb, string fat);
        string ConvertToDouble(string protText, string carbText, string fatText);


    }


}
