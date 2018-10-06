using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metro_UWP.Models
{
    public class Line
    {
        public int Id { get; set; }
        public bool IsAvailableOnSunday { get; set; }
        public List<DateTime?> Times { get; set; }
    }
}
