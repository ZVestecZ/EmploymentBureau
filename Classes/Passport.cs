using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EmploymentBureau
{
    public class Passport
    {
        [XmlElement("Number")]
        public uint Number { get; set; }

        [XmlElement("IssueDate")]
        public DateTime IssueDate { get; set; }

        [XmlElement("EndDate")]
        public DateTime EndDate { get; set; }

        [XmlArray("Registrations")]
        [XmlArrayItem("Registration")]
        public string[] Registrations { get; set; } = Array.Empty<string>();

        [XmlArray("Marriages")]
        [XmlArrayItem("Marriage")]
        public string[] Marriages { get; set; } = Array.Empty<string>();
    }
}
