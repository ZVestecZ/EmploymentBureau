using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace EmploymentBureau.Classes
{
    internal class WorkplaceManager
    {
        private List<Workplace> workplaces = new List<Workplace>();

        public List<Workplace> GetWorkplaces()
        {
            return workplaces;
        }

        public void SerializeToJson(string filePath)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(workplaces, options);
            File.WriteAllText(filePath, jsonString);
        }

        public void DeserializeFromJson(string filePath)
        {
            if (File.Exists(filePath))
            {
                string jsonString = File.ReadAllText(filePath);
                workplaces = JsonSerializer.Deserialize<List<Workplace>>(jsonString) ?? new List<Workplace>();
            }
            else
            {
                workplaces = new List<Workplace>();
            }
        }
    }
}
