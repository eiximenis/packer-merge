using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PackerMerge.Helpers
{
    public class PackerTemplateVariablesHelper
    {
        private readonly PackerTemplate _owner;
        public PackerTemplateVariablesHelper(PackerTemplate owner)
        {
            _owner = owner;
        }

        public IDictionary<string, string> GetTypedVariables()
        {
            var parsed = new Dictionary<string, string>();
            dynamic variables = _owner.Variables;
            if (variables != null)
            {
                foreach (var x in variables)
                {
                    parsed.Add(x.Name, x.Value?.ToString() ?? string.Empty);
                }
            }

            return parsed;
        } 
    }
}
