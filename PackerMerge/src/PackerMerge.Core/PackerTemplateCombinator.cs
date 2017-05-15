using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PackerMerge
{
    public class PackerTemplateCombinator
    {
        private readonly PackerTemplate[] _input;
        private PackerTemplate _output;

        public PackerTemplate Output
        {
            get
            {
                return _output;
            }
        }
        public PackerTemplateCombinator(IEnumerable<PackerTemplate> input)
        {
            _output = new PackerTemplate();
            _input = input.ToArray();
        }

        public PackerTemplateCombinator Combine()
        {
            foreach (var template in _input)
            {
                _output = CombinatorStrategyAdd.Combine(_output, template);
            }
            return this;
        }

    }
}
