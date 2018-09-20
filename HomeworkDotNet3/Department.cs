using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkDotNet3
{
    public class Department
    {
        private static int counter = 1;

        public int Id { get; set; }
        public string Name { get; set; }
        public List<Project> Projects { get; } = new List<Project>();
        public List<Worker> Workers { get; } = new List<Worker>();

        public Department(string name)
        {
            Name = name;
            Id = counter++;
        }

        public void AssignToProject(int projectId, int workerId)
        {
            Project project = Projects.First(x => x.Id == projectId);
            Worker worker = Workers.First(x => x.Id == workerId);

            if (worker.Projects.All(x => x.Id != projectId))
            {
                worker.Projects.Add(project);
                project.AssignedWorkers.Add(worker);
            }
        }

        public void WithdrawFromProject(int projectId, int workerId)
        {
            Worker worker = Workers.First(x => x.Id == projectId);
            Project project = worker.Projects.FirstOrDefault(x => x.Id == projectId);

            if (project is Project)
            {
                worker.Projects.Remove(project);
                project.AssignedWorkers.Remove(worker);
            }
        }
    }
}
