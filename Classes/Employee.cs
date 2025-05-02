using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EmploymentBureau.Classes
{
    public class Employee
    {
        [XmlElement("Name")]
        public string Name { get; set; } = "";

        [XmlElement("Age")]
        public uint Age { get; set; }

        [XmlElement("Male")]
        public bool Male { get; set; }

        [XmlElement("Passport")]
        public Passport Passport { get; set; }

        [XmlArray("WorkExperience")]
        [XmlArrayItem("Position")]
        public string[] WorkExperience { get; set; } = Array.Empty<string>();
    }
}
