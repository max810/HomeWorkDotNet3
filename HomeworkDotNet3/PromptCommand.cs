using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkDotNet3
{
    public class PromptCommand
    {
        public string Name { get; set; }
        public string[] Args { get; }

        public PromptCommand(string name, string[] args)
        {
            Name = name;
            Args = args;
        }
    }
}
