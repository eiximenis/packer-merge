using McMaster.Extensions.CommandLineUtils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
namespace PackerMerge
{
    class Program
    {
        static void Main(string[] args)
        {
            var app = new CommandLineApplication() { Name = "packer-merge" };
            app.HelpOption("-?|-h|--help");

            app.OnExecute(() =>
            {
                return 0;
            });

            app.Command("merge", cmd =>
            {
                cmd.Description = "Merges some packer-templates in one new template";
                var inputPath = cmd.Option("-i|--input|--input-files <input-files>",
                                            "Input templates to use",
                                            CommandOptionType.MultipleValue);
                var outputPath = cmd.Option("-o|--output|--output-file <output-file>",
                                            "output path to leave the new merged template",
                                            CommandOptionType.SingleValue);
                cmd.OnExecute(() =>
                {

                    if (!inputPath.HasValue())
                    {
                        Console.WriteLine("Must specify some input templates");
                        return 1;
                    }


                    var input = inputPath.Values.Select(i => PackerTemplate.ReadFrom(i));
                    var combinator = new PackerTemplateCombinator(input);
                    combinator.Combine();
                    var result = combinator.Output;
                    result.SaveTo(outputPath.HasValue() ? outputPath.Value() : Path.Combine(".", "template.json"));
                    return 0;
                });
            });

            app.Command("new", cmd =>
            {
                var outputPath = cmd.Option("-o|--output|--output-file <output-file>",
                            "output path to leave the new merged template",
                            CommandOptionType.SingleValue);

                cmd.OnExecute(() =>
                {
                    var destination = outputPath.HasValue() ? outputPath.Value() : Path.Combine(".", "template.json");
                    var template = new PackerTemplate();
                    template.SaveTo(destination);
                    return 0;
                });


            });

            app.Execute(args);
        }
    }
}