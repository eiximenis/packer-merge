using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PackerMerge
{
    public static class CombinatorStrategyAdd
    {
        public static PackerTemplate Combine(PackerTemplate current, PackerTemplate extraData)
        {
            if (extraData.Variables != null)
            {
                MergeVariables(current, extraData);
            }

            if (extraData.Builders != null)
            {
                MergeBuilders(current, extraData);
            }

            if (extraData.Provisioners != null)
            {
                MergeProvisioners(current, extraData);
            }


            return current;
        }

        private static void MergeVariables(PackerTemplate current, PackerTemplate extraData)
        {
            if (current.Variables == null)
            {
                current.Variables = extraData.Variables.DeepClone();
            }
            else
            {
                foreach (var variable in extraData.Variables)
                {
                    current.Variables.Add(variable);
                }
            }
        }

        private static void MergeBuilders(PackerTemplate current, PackerTemplate extraData)
        {
            if (current.Builders == null)
            {
                current.Builders = extraData.Builders.DeepClone();
            }
            else
            {
                foreach (var builder in extraData.Builders)
                {
                    current.Builders.Add(builder);
                }
            }
        }

        private static void MergeProvisioners(PackerTemplate current, PackerTemplate extraData)
        {
            if (current.Provisioners == null)
            {
                current.Provisioners = extraData.Provisioners.DeepClone();
            }
            else
            {
                foreach (var provisioner in extraData.Provisioners)
                {
                    current.Provisioners.Add(provisioner);
                }
            }
        }
    }
}
