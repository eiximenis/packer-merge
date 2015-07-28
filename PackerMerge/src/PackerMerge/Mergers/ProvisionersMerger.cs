using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PackerMerge.Helpers;

namespace PackerMerge.Mergers
{
    public class ProvisionersMerger
    {
        private readonly PackerTemplate _first;
        private readonly PackerTemplate _second;
        public ProvisionersMerger(PackerTemplate first, PackerTemplate second)
        {
            _first = first;
            _second = second;
        }

        public dynamic MergeProvisioners()
        {
            var pfirst = _first.Provisioners;
            var psecond = _second.Provisioners;

            if (pfirst == null && psecond == null) return null;

            if (pfirst == null)
            {
                return psecond.DeepClone();
            }

            var mixed = pfirst.DeepClone();

            if (psecond != null)
            {
                foreach (var provisioner in psecond)
                {
                    mixed.Add(provisioner);
                }
            }

            return mixed;

        }

    }
}
