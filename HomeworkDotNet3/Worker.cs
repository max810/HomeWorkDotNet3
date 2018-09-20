using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkDotNet3
{
    public class Worker
    {
        private static int counter = 1;

        public int Id { get; set; }
        public string Name { get; set; }
        public int Experience { get; set; }

        public Department Department { get; set; }
        public List<Project> Projects { get; } = new List<Project>();

        public Worker(string name, int exp)
        {
            Id = counter++;
            Name = name;
            Experience = exp;
        }

        public override string ToString()
        {
            return $"{Id}: {Name}, {Department?.Name ?? "no"} dept.";
        }

        public void PrintProjectCount()
        {
            Console.WriteLine(Projects.Count());
        }

        public void PrintColleagues()
        {
            // again, selecting into ToString is not necessary here, just to show
            var workGroups = Projects
                .Select(x => new
                {
                    ProjectInfo = x.Name,
                    WorkersInfo = x.AssignedWorkers.Where(y => y.Id != this.Id).Select(y => "\t" + y.ToString())
                });

            foreach(var groupInfo in workGroups)
            {
                Console.WriteLine(groupInfo.ProjectInfo);
                foreach(var workerInfo in groupInfo.WorkersInfo)
                {
                    Console.WriteLine(workerInfo);
                }
            }
        }
    }
}
