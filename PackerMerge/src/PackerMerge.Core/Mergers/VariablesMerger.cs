using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PackerMerge.Mergers
{
    public class VariablesMerger
    {
        private readonly PackerTemplate _first;
        private readonly PackerTemplate _second;


        public VariablesMerger(PackerTemplate first, PackerTemplate second)
        {
            _first = first;
            _second = second;
        }

        public dynamic MergeVariables()
        {
            var vfirst = _first.Variables;
            var vsecond = _second.Variables;

            if (vfirst == null && vsecond == null) return null;

            if (vfirst == null)
            {
                return vsecond.DeepClone();
            }

            var mixed = vfirst.DeepClone();
            if (vsecond != null)
            {
                foreach (var svar in vsecond)
                {
                    AddVariableIfNeeded(mixed, svar);
                }
            }

            return mixed;
        }

        private void AddVariableIfNeeded(dynamic currentVars, dynamic newVar)
        {
            foreach (var cvar in currentVars)
            {
                if (cvar.Name == newVar.Name) return;
            }

            currentVars.Add(newVar);
        }
    }
}
