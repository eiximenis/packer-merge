using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PackerMerge
{
    public class Program
    {
        public void Main(string[] args)
        {
            var parameters = new Parameters();
            parameters.Process(args);

            var input = parameters.InputFiles.Select(i => PackerTemplate.ReadFrom(i));
            var combinator = new PackerTemplateCombinator(input);
            combinator.Combine();
            var result = combinator.Output;

            result.SaveTo(parameters.OutputFile);
        }
    }
}
