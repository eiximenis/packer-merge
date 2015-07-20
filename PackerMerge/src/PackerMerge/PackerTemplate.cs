using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace PackerMerge
{
    public class PackerTemplate
    {

        public dynamic Variables { get; set; }

        public dynamic Builders { get; set; }

        public dynamic Provisioners { get; set; }
        
        public static PackerTemplate ReadFrom(string file)
        {
            var template = new PackerTemplate();
            dynamic jsonData = JObject.Parse(File.ReadAllText(file));
            template.Variables = jsonData.variables;
            template.Builders = jsonData.builders;
            template.Provisioners = jsonData.provisioners;
            return template;
        }

        public void SaveTo(string file)
        {
            JObject template = new JObject();
            if (Variables != null) template.Add("variables", Variables);
            if (Builders != null) template.Add("builders", Builders);
            if (Provisioners != null) template.Add("provisioners", Provisioners);

            var json = JsonConvert.SerializeObject(template,
                Formatting.Indented,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            File.WriteAllText(file, json);
        }
    }
}
