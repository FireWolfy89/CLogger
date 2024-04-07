using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLogger.Classes
{
    class Goals
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string CWeight { get; set; }

        public string GWeight { get; set; }

        public string Age { get; set; }

        public string Activity { get; set; }

        public string Kcal { get; set; }

        public string Prot { get; set; }

        public string Carbs { get; set; }

        public string Fat { get; set; }

        public string Kg { get; set; }
    }
}
