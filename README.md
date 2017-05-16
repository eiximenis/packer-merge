# packer-merge

A template merger for [Packer](https://www.packer.io/) to allow easy creation of templates based on a initial set of "partial" templates.

Very strong WIP by now, current version is very limited and without any warranty. Clone or fork at your own risk :)

Any idea is appreciated, so feel free to open issues to submit your ideas.

##Current usage

Very basic. Has only one command `merge` with following usage:

```
merge -i template1.json -i template2.json -i templateN.json -o combinedTemplate.json
```

`-i` is for input templates and `-o` is for the result template

##Technology

Project is a _netcoreapp1.1_ console application, but almost all code is in the `PackerMerge.Core` project, which is _netstandard1.4_

##Other options

If use Ruby take a look to Racker: https://github.com/aspring/racker
