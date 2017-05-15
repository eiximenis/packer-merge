using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PackerMerge.Helpers;

namespace PackerMerge
{
    public static class PackerTemplateExtensions
    {
        public static PackerTemplateBuildersHelper GetBuildersHelper(this PackerTemplate template)
        {
            return new PackerTemplateBuildersHelper(template);
        }

        public static PackerTemplateBuildersHelper GetVariablesHelper(this PackerTemplate template)
        {
            return new PackerTemplateBuildersHelper(template);
        }

    }
}
