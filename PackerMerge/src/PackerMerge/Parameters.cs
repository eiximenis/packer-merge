using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PackerMerge
{
    public class Parameters
    {
        private const string AddInputFlag = "-i:";
        private const string SetOutputFlag = "-o:";

        private readonly List<string> _inputFiles;
        public string OutputFile { get; private set; }

        public IEnumerable<string> InputFiles
        {
            get
            {
                return _inputFiles;
            }
        }

        public Parameters()
        {
            _inputFiles = new List<string>();
            OutputFile = "template.json";
        }

        public void Process( IEnumerable<string> args)
        {
            foreach (var arg in args)
            {
                if (!string.IsNullOrEmpty(arg))
                {
                    if (arg.StartsWith(AddInputFlag))
                    {
                        ProcessAddInputFlag(arg.Substring(AddInputFlag.Length));
                    }
                    else if (arg.StartsWith(SetOutputFlag))
                    {
                        ProcessSetOutputFlag(arg.Substring(SetOutputFlag.Length));
                    }
                }      
            }
        }

        private void ProcessAddInputFlag(string arg)
        {
            var input = arg.Split(',');
            foreach (var entry in input)
            {
                _inputFiles.Add(entry);
            }
        }

        private void ProcessSetOutputFlag(string arg)
        {
            OutputFile = arg;
        }
    }
}
