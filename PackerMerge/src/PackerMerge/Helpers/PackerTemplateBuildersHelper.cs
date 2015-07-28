using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PackerMerge.Helpers
{
    public class PackerTemplateBuildersHelper
    {
        private readonly PackerTemplate _owner;
        private const string CommonBuilderType = "*";

        public PackerTemplateBuildersHelper(PackerTemplate owner)
        {
            _owner = owner;
        }

        public dynamic GetCommonBuilder()
        {
            var builders = _owner.Builders;
            if (builders == null)
            {
                return null;
            }

            foreach (var builder in builders)
            {
                if (builder.type == CommonBuilderType)
                {
                    return builder;
                }
            }

            return null;
        }

        public IDictionary<string, dynamic> GetBuilders()
        {
            var parsed = new Dictionary<string, dynamic>();
            var builders = _owner.Builders;
            var commonBuilder = GetCommonBuilder();
            if (builders != null)
            {
                foreach (var builder in builders)
                {
                    if (builder.type != CommonBuilderType)
                    {
                        parsed.Add(builder.type.ToString(), builder);
                    }
                }
            }

            return parsed;
        }

        public dynamic this[string type]
        {
            get
            {
                foreach (var builder in _owner.Builders)
                {
                    if (builder.type == type)
                    {
                        return builder;
                    }
                }

                return null;
            }
        }
    }
}
