using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EmploymentBureau.Classes
{
    [XmlRoot("Employees")]
    public class EmployeeList
    {
        [XmlElement("Employee")]
        public List<Employee> Employees { get; set; } = new List<Employee>();
    }
}
 