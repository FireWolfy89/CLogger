using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CLogger
{
    public class Daily
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Protein {  get; set; }

        public string Carb { get; set; }

        public string Fat { get; set; }

        public string Result { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

    }
}
