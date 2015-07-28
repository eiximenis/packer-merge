using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PackerMerge.Helpers;

namespace PackerMerge.Mergers
{
    public class BuildersMerger
    {
        private readonly PackerTemplate _first;
        private readonly PackerTemplate _second;
        public BuildersMerger(PackerTemplate first, PackerTemplate second)
        {
            _first = first;
            _second = second;
        }

        public dynamic MergeBuilders()
        {
            var bfirst = _first.Builders;
            var bsecond = _second.Builders;

            if (bfirst == null && bsecond == null) return null;

            if (bfirst == null)
            {
                return bsecond.DeepClone();
            }

            var mixed = bfirst.DeepClone();

            if (bsecond != null)
            {

                foreach (var builder in bsecond)
                {
                    CombineBuilders(mixed, builder);
                }
            }


            return mixed;

        }

        private void CombineBuilders(dynamic currentBuilders, dynamic newBuilder)
        {
            foreach (var cbuilder in currentBuilders)
            {
                if (cbuilder.type == newBuilder.type)
                {
                    PerformBuilderCombination(cbuilder, newBuilder);
                    return;
                }
            }

            currentBuilders.Add(newBuilder.DeepClone());

        }

        private void PerformBuilderCombination(dynamic currentBuilder, dynamic newBuilder)
        {
            currentBuilder.floppy_files = CombineFloppyFiles(currentBuilder.floppy_files, newBuilder.floppy_files);
            DynamicObjectMergerFirstWin.Merge(currentBuilder, newBuilder);
        }

        private dynamic CombineFloppyFiles(dynamic currentFloppyFiles, dynamic newFloppyFiles)
        {
            if (currentFloppyFiles == null && newFloppyFiles == null) return null;
            if (currentFloppyFiles == null) return newFloppyFiles.DeepClone();

            var mixed = currentFloppyFiles.DeepClone();

            if (newFloppyFiles != null)
            {
                foreach (var cfloppy in newFloppyFiles)
                {
                    mixed.Add(cfloppy);
                }
            }
            return mixed;
        }
    }
}
