using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PackerMerge
{
    public class PackerTemplateCombinator
    {
        private readonly PackerTemplate[] _input;
        private readonly PackerTemplate _output;

        public PackerTemplate Output
        {
            get
            {
                return _output;
            }
        }
        public PackerTemplateCombinator(IEnumerable<PackerTemplate> input)
        {
            _input = input.ToArray();
            _output = new PackerTemplate();
        }

        public PackerTemplateCombinator Combine()
        {
            foreach (var template in _input)
            {
                CombinatorStrategyAdd.Combine(_output, template);
            }
            return this;
        }

    }
}
