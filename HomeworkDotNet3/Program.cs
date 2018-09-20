using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkDotNet3
{
    public class Program
    {
        private static Company sampleCompany;
        private static Worker newlyCreatedWorker;
        private static readonly Dictionary<string, string> Help = new Dictionary<string, string>
        {
            { "colleagues", "<workerId> - print all colleagues from all projects" },
            { "project-count", "<workerId> - print number of projects for this worker" },
            { "worker", "<workerId> - print worker information" },
            { "assign-dept", "<deptId> <workerId> - assign worker to department" },
            { "assign-proj", "<deptId> <workerId> - assign worker to project" },
            { "withdraw", "<projId> <workerId> - withdraw worker from project" },
            { "fire", "<workerId> - fire worker (fully delete)" },
            { "new", "create new worker (will be temporarily saved)" },
            { "hire", "hire previously created worker (add to the company)" },
            { "print-all", "print all workers" },
            { "print-structure", "print company's departments and their workers" },
            { "move", "<dept from id> <dept to id> <workerId> move worker to another department" },
        };
        public static void Main(string[] args)
        {
            // компьютерная модель айти-компании
            // сотрудники в разных проектах 
            // запросы к сотрудникам/проектам
            sampleCompany = GenerateSampleCompany();
            string input;
            do
            {
                PrintPrompt();
                input = ReadCommand();
                if (input == "exit")
                {
                    break;
                }
                var command = ParseCommand(input);
                ExecuteCommand(command);
            }
            while (true);


            // dept mov id idworker to
            // proj withdraw id idworker
            // company fire id
        }

        private static void PrintPrompt()
        {
            Console.Write("$_>");
        }

        private static string ReadCommand()
        {
            string command = Console.ReadLine().Trim();
            return command;
        }

        private static PromptCommand ParseCommand(string cmd)
        {
            string[] tokens = cmd.Split();
            var cmdName = tokens[0];
            var args = tokens.Skip(1).ToArray();

            return new PromptCommand(cmdName, args);
        }

        private static void ExecuteCommand(PromptCommand command)
        {
            var args = command.Args;
            int arg0 = -1;
            int arg1 = -1;
            int arg2 = -1;
            if (args.Length > 0)
            {
                int.TryParse(command.Args[0], out arg0);
            }
            if (args.Length > 1)
            {
                int.TryParse(command.Args[1], out arg1);
            }
            if (args.Length > 2)
            {
                int.TryParse(command.Args[2], out arg2);
            }

            switch (command.Name)
            {
                case "colleagues":
                    sampleCompany.PrintColleagues(arg0);
                    break;
                case "project-count":
                    sampleCompany.PrintProjectCount(arg0);
                    break;
                case "worker":
                    sampleCompany.PrintWorker(arg0);
                    break;
                case "move":
                    sampleCompany.MoveWorker(arg0, arg1, arg2);
                    break;
                case "assign-dept":
                    sampleCompany.AssignToDept(arg0, arg1);
                    break;
                case "assign-proj":
                    sampleCompany.AssignToProject(arg0, arg1);
                    break;
                case "withdraw":
                    sampleCompany.WithdrawFromProject(arg0, arg1);
                    break;
                case "fire":
                    sampleCompany.Fire(arg0);
                    break;
                case "new":
                    CreateNewWorker();
                    break;
                case "hire":
                    sampleCompany.Hire(newlyCreatedWorker);
                    break;
                case "print-all":
                    sampleCompany.PrintAllWorkers(arg0 == 0 ? false : true);
                    break;
                case "print-structure":
                    sampleCompany.PrintStructure();
                    break;
                case "help":
                    if (arg0 == -1)
                    {
                        PrintHelp();
                    }
                    else
                    {
                        PrintHelp(args[0]);
                    }
                    break;
            }
        }

        private static Company GenerateSampleCompany()
        {
            Company company = new Company();
            Department dept1 = new Department("Logistic Department");
            Department dept2 = new Department("Business Department");
            Project project1 = new Project("Working station");
            Project project2 = new Project("Radio emittors");
            Project project3 = new Project("Micorwave prototype");
            Worker[] workers = new[]
            {
                new Worker("Alex Murphy", 1),
                new Worker("Alex Gray", 2),
                new Worker("Sally Rode", 3),
                new Worker("Peter Peter Peter", 4),
                new Worker("Coyote Peterson", 2),
                new Worker("Arbeiter John", 10),
                new Worker("Caesar Brutus Knifus Backus Stabus", 6),
                new Worker("Alex Murphy #2", 1)
            };
            company.Departments.Add(dept1);
            company.Departments.Add(dept2);
            dept1.Projects.Add(project1);
            dept1.Projects.Add(project2);
            dept2.Projects.Add(project3);
            Random rnd = new Random();
            foreach (var worker in workers)
            {
                company.Hire(worker);
                int deptNo = rnd.Next(1, 3);
                company.AssignToDept(deptNo, worker.Id);
                company.AssignToProject(deptNo == 1 ? rnd.Next(1, 3) : 3, worker.Id);
            }

            return company;
        }

        private static void PrintHelp(string funcName)
        {
            Console.WriteLine(funcName + ": " + Help[funcName]);
        }

        private static void PrintHelp()
        {
            foreach (var kv in Help)
            {
                Console.WriteLine(kv.Key + ": " + kv.Value);
            }
        }

        private static void CreateNewWorker()
        {
            Console.WriteLine("Enter new worker name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter new worker experience (years):");
            int experience = int.Parse(Console.ReadLine());
            Worker x = new Worker(name, experience);

            newlyCreatedWorker = x;
        }
    }
}
