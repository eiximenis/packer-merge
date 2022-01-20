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

            var currentFloppyFilesAndPath = new Dictionary<string, string>();
            var newFloppyFilesAndPath = new Dictionary<string, string>();
            foreach (var cfp in currentFloppyFiles)
            {
                var entry = cfp.ToString();
                var tokens = entry.Split('/', StringSplitOptions.RemoveEmptyEntries);
                var name = tokens[tokens.Length - 1];
                currentFloppyFilesAndPath.Add(name, entry);
            }

            if (newFloppyFiles != null)
            {

                foreach (var nfp in newFloppyFiles)
                {
                    var entry = nfp.ToString();
                    var tokens = entry.Split('/', StringSplitOptions.RemoveEmptyEntries);
                    var name = tokens[tokens.Length - 1];
                    newFloppyFilesAndPath.Add(name, entry);
                }

                foreach (var newFloppyEntryKey in newFloppyFilesAndPath.Keys)
                {
                    if (currentFloppyFilesAndPath.ContainsKey(newFloppyEntryKey))
                    {
                        currentFloppyFilesAndPath[newFloppyEntryKey] = newFloppyFilesAndPath[newFloppyEntryKey];
                    }
                    else
                    {
                        currentFloppyFilesAndPath.Add(newFloppyEntryKey, newFloppyFilesAndPath[newFloppyEntryKey]);
                    }
                }
            }

            var mixed = new Newtonsoft.Json.Linq.JArray(currentFloppyFilesAndPath.Values);

            return mixed;
        }
    }
}
