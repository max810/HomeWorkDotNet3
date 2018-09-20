using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkDotNet3
{
    public class Company
    {
        public List<Department> Departments { get; set; } = new List<Department>();

        private readonly List<Worker> idleWorkers = new List<Worker>();

        public void MoveWorker(int depFromId, int depToId, int workerId)
        {
            var worker = Departments.First(x => x.Id == depFromId).Workers.First(x => x.Id == workerId);

            // seems illogical, but hey, as long as it works...
            Fire(workerId);
            Hire(worker);
            AssignToDept(depToId, workerId);
        }

        public void Hire(Worker worker)
        {
            idleWorkers.Add(worker);
        }

        public void Fire(int workerId)
        {
            // cascade delete

            Worker _worker = idleWorkers.FirstOrDefault(x => x.Id == workerId);
            if (_worker is Worker)
            {
                idleWorkers.Remove(_worker);
            }
            foreach (var dept in Departments)
            {
                _worker = dept.Workers.FirstOrDefault(x => x.Id == workerId);
                if (_worker is Worker)
                {
                    dept.Workers.Remove(_worker);
                    foreach (var proj in _worker.Projects)
                    {
                        proj.AssignedWorkers.Remove(_worker);
                    }
                }
            }
        }

        public void AssignToDept(int deptId, int workerId)
        {
            Department dept = Departments.First(x => x.Id == deptId);
            Worker worker = idleWorkers.First(x => x.Id == workerId);

            worker.Department = dept;
            dept.Workers.Add(worker);
            idleWorkers.Remove(worker);
        }

        public void PrintAllWorkers(bool desc = false)
        {
            var workers = Departments.SelectMany(x => x.Workers);
            workers = desc ? workers.OrderByDescending(x => x.Id) : workers.OrderBy(x => x.Id);
            foreach (var workerInfo in workers)
            {
                Console.WriteLine(workerInfo);
            }
        }

        public void PrintWorker(int workerId)
        {
            var worker = GetWorker(workerId);
            Console.WriteLine(worker.ToString());
        }

        public void PrintColleagues(int workerId)
        {
            var worker = GetWorker(workerId);
            if (worker is Worker)
            {
                worker.PrintColleagues();
            }
        }

        public void PrintProjectCount(int workerId)
        {
            var worker = GetWorker(workerId);
            if (worker is Worker)
            {
                worker.PrintProjectCount();
            }
        }

        public void AssignToProject(int projectId, int workerId)
        {
            var worker = GetWorker(workerId);
            if (worker is Worker)
            {
                worker.Department.AssignToProject(projectId, workerId);
            }
        }

        public void WithdrawFromProject(int projectId, int workerId)
        {
            var worker = GetWorker(workerId);
            if (worker is Worker)
            {
                worker.Department.WithdrawFromProject(projectId, workerId);
            }
        }

        public void PrintStructure()
        {
            Console.WriteLine("Company 'WTM' - whatever that means:");
            foreach(var dept in Departments)
            {
                Console.WriteLine($"{dept.Name}, {dept.Projects.Count} project(s), {dept.Workers.Count} workers.");
                foreach(var worker in dept.Workers)
                {
                    Console.WriteLine($"\t{worker.ToString()}");
                }
            }
        }

        private Worker GetWorker(int workerId)
        {
            var dept = Departments.FirstOrDefault(x => x.Workers.Any(y => y.Id == workerId));
            return dept?.Workers.First(x => x.Id == workerId);
        }
    }
}
