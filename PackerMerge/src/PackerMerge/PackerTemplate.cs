using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PackerMerge
{
    /// <summary>
    /// This class contains the info for a package template. Is basically
    /// a thin wrapper over the dynamic json structure of a packer template file.
    /// </summary>
    public class PackerTemplate
    {

        public dynamic Variables { get; private set; }

        public dynamic Builders { get; private set; }

        public dynamic Provisioners { get; private set; }

        public dynamic PostProcessors { get; private set; }

        public PackerTemplate() { }

        public PackerTemplate(dynamic var, dynamic builders, dynamic prov, dynamic pp)
        {
            if (var != null) Variables = var;
            if (builders != null) Builders = builders;
            if (prov != null) Provisioners = prov;
            if (pp != null) PostProcessors = pp;
        }

        public void MergeVariables(dynamic otherVariables)
        {
            if (Variables == null)
            {
                Variables = otherVariables.DeepClone();
            }
            foreach (var otherVar in otherVariables)
            {
                Variables.Add(otherVar);
            }
        }

        public void MergeBuilders(dynamic otherBuilders, string builderName = null)
        {
            if (Builders == null)
            {
                Builders = otherBuilders.DeepClone();
            }
        }

        public static PackerTemplate ReadFrom(string file)
        {
            var template = new PackerTemplate();
            dynamic jsonData = JObject.Parse(File.ReadAllText(file));
            template.Variables = jsonData.variables;            
            template.Builders = jsonData.builders;
            template.Provisioners = jsonData.provisioners;
            template.PostProcessors = jsonData["post-processors"];
            return template;
        }

        public void SaveTo(string file)
        {
            JObject template = new JObject();
            if (Variables != null) template.Add("variables", Variables);
            if (Builders != null) template.Add("builders", Builders);
            if (Provisioners != null) template.Add("provisioners", Provisioners);
            if (PostProcessors != null) template.Add("post-processors", PostProcessors);

            var json = JsonConvert.SerializeObject(template,
                Formatting.Indented,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            File.WriteAllText(file, json);
        }
    }
}
