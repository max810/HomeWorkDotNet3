using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkDotNet3
{
    public class Project
    {
        private static int counter = 1;

        public int Id { get; set; }
        public string Name { get; set; }
        public List<Worker> AssignedWorkers { get; } = new List<Worker>();

        public Project(string name)
        {
            Id = counter++;
            Name = name;
        }
    }
}
