using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PackerMerge.Mergers;

namespace PackerMerge
{
    public static class CombinatorStrategyAdd
    {

        public static PackerTemplate Combine(PackerTemplate current, PackerTemplate extraData)
        {
            var variables = new VariablesMerger(current, extraData).MergeVariables();
            var builders = new BuildersMerger(current, extraData).MergeBuilders();
            var provisioners = new ProvisionersMerger(current, extraData).MergeProvisioners();
            var postProcessors = new PostProcessorsMerger(current, extraData).MergePostProcessors();
            var merged = new PackerTemplate(variables, builders, provisioners, postProcessors);

            return merged;
        }

    }
}
