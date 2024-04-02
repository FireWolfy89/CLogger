using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLogger.Classes
{
    internal class Weeks
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public Macros Macros { get; set; }

        public int WeekNo { get; set; }

    }
}
