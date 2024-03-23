using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CLogger
{
    //Interface added, so I can reduce the calculation logic code by instantiating the class(where it is implemented) in my window classes

    interface IDataValidation
    {
        bool IsInputValid(string protText, string carbText, string fatText);
        bool IsInputValid_daily(string foodtext, string prot, string carbs, string fat);
        string ConvertToDouble(string protText, string carbText, string fatText);
        
        
    }


}
