using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metro_UWP.Models
{
    public class Station
    {
        public enum Directions { SM,MS};
        public int Id { get; set; }
        public string NameFR { get; set; }
        public string NameAR { get; set; }
        public string Desc { get; set; }
        public double Long { get; set; }
        public double Lat { get; set; }
        public int Direction { get; set; }
    }

}
