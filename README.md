# packer-merge
A template mergert for Packer to allow easy creation of templates based on a initial set of "partial" templates.

Very strong WIP by now, current version is very limited and without any warranty. Clone or fork at your own risk :)

Any idea is appreciated, so feel free to open issues to submit your ideas.

##Current usage
Very basic. Accepts two parameters:

    -i:template.json,template2.json,...,templateN.json -> Files to combine
    
    -o:template_outuput.json -> Packer template with combination of all input templates
  
Current version only works if input templates uses different sections of packer template (e.g. can have one template with
builders and another with provisioners, but can't combine two builders templates). This is in the scope of course :)

##Technology
Project is a asp.net5 console application, designed to be cross platform. More details on how compile and run it in Windows, MacxOSX and Linux soon :)

##Other options
If use Ruby take a look to Racker: https://github.com/aspring/racker
