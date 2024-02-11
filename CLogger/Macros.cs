using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CLogger
{
    public class Macros
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Day { get; set; }

        public string Protein { get; set; }

        public string Carb { get; set; }

        public string Fat { get; set; }

        public string Result { get; set; }

       

    }
}
