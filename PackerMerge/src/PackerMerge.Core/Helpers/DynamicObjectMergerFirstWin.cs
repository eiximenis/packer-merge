using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PackerMerge.Helpers
{
    public static class DynamicObjectMergerFirstWin
    {
        public static void Merge(dynamic first, dynamic second)
        {
            if (second != null)
            {
                foreach (var prop in second)
                {
                    SetPropertyIfNeeded(prop, first);
                }
            }
        }

        private static void SetPropertyIfNeeded(dynamic prop, dynamic obj)
        {
            foreach (var objProp in obj)
            {
                if (objProp.Name == prop.Name) return;
            }

            obj.Add(prop);

        }
    }
}
