using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PackerMerge.Helpers;

namespace PackerMerge.Mergers
{
    public class PostProcessorsMerger
    {
        private readonly PackerTemplate _first;
        private readonly PackerTemplate _second;
        public PostProcessorsMerger(PackerTemplate first, PackerTemplate second)
        {
            _first = first;
            _second = second;
        }

        public dynamic MergePostProcessors()
        {
            var ppfirst = _first.PostProcessors;
            var ppsecond = _second.PostProcessors;

            if (ppfirst == null && ppsecond == null) return null;

            if (ppfirst == null)
            {
                return ppsecond.DeepClone();
            }

            var mixed = ppfirst.DeepClone();

            foreach (var postProcessor in ppsecond)
            {
                mixed.Add(postProcessor);
            }

            return mixed;
        }
    }
}
