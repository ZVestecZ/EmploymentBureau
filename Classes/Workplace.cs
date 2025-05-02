using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentBureau.Classes
{
    public class Workplace
    {
        public string Title { get; set; } = "";
        public string Post { get; set; } = "";
        public uint Wages { get; set; }
        public string[] Documents { get; set; } = Array.Empty<string>();
        public string[] WorkSchedules { get; set; } = Array.Empty<string>();
    }
}
