# GnuGetOpt

C#.NET port of Java port of GNU getopt from glibc 2.0.6

[![NuGet](https://img.shields.io/nuget/v/GnuGetOpt)](https://www.nuget.org/packages/gnugetopt)
[![NuGet Downloads](https://img.shields.io/nuget/dt/GnuGetOpt)](https://www.nuget.org/packages/gnugetopt)
[![Build](https://github.com/lduchosal/gnugetopt/actions/workflows/dotnet.yml/badge.svg)](https://github.com/lduchosal/gnugetopt/actions/workflows/dotnet.yml)
[![SonarCloud](https://github.com/lduchosal/gnugetopt/actions/workflows/build.yml/badge.svg)](https://github.com/lduchosal/gnugetopt/actions/workflows/build.yml)
![GitHub last commit](https://img.shields.io/github/last-commit/lduchosal/gnugetopt)
[![License](https://img.shields.io/github/license/lduchosal/gnugetopt)](LICENSE)

## Installation

```
dotnet add package GnuGetOpt
```

## Usage

### Short options

Parse command-line options using a format string where each character is a valid option. A colon after a character means the option requires an argument. Two colons mean the argument is optional.

```csharp
using Gnu.Getopt;

var g = new Getopt("myapp", args, "ab:c::d");

int c;
while ((c = g.getopt()) != -1)
{
    switch (c)
    {
        case 'a':
        case 'd':
            Console.WriteLine("Option: " + (char)c);
            break;

        case 'b':
        case 'c':
            Console.WriteLine("Option: " + (char)c + " Arg: " + g.Optarg);
            break;

        case '?':
            // getopt() already printed an error
            break;
    }
}

// Remaining non-option arguments
for (int i = g.Optind; i < args.Length; i++)
{
    Console.WriteLine("Argument: " + args[i]);
}
```

```
$ myapp -a -b value -d file1 file2
Option: a
Option: b Arg: value
Option: d
Argument: file1
Argument: file2
```

### Long options

Define long options using `LongOpt` objects. Each long option maps to a short option character or a custom integer value.

```csharp
using Gnu.Getopt;
using System.Text;

var longopts = new LongOpt[]
{
    new LongOpt("help", Argument.No, null, 'h'),
    new LongOpt("output", Argument.Required, null, 'o'),
    new LongOpt("verbose", Argument.No, null, 'v'),
};

var g = new Getopt("myapp", args, "ho:v", longopts);

int c;
while ((c = g.getopt()) != -1)
{
    switch (c)
    {
        case 'h':
            Console.WriteLine("Help requested");
            break;
        case 'o':
            Console.WriteLine("Output: " + g.Optarg);
            break;
        case 'v':
            Console.WriteLine("Verbose mode");
            break;
    }
}
```

```
$ myapp --verbose --output=result.txt input.txt
Verbose mode
Output: result.txt
```

### Option string format

| Format | Meaning |
|--------|---------|
| `a` | Option `-a`, no argument |
| `b:` | Option `-b`, required argument |
| `c::` | Option `-c`, optional argument |
| `:abc` | Return `:` instead of `?` for missing arguments |
| `+abc` | POSIX mode: stop at first non-option |
| `-abc` | Return in order: non-options returned as argument to option `1` |

### Properties

| Property | Description |
|----------|-------------|
| `Optarg` | The argument value for the current option |
| `Optind` | Index of the next element to be processed in argv |
| `Optopt` | The option character that caused an error |
| `Opterr` | Set to `false` to suppress error messages |
| `Longind` | Index into the long option array of the last long option found |

## Breaking changes in 1.0.0

### Nullable reference types

All projects now have `<Nullable>enable</Nullable>`. Properties that can be null are annotated:

- `Getopt.Optarg` is now `string?`
- `LongOpt` constructor `flag` parameter accepts `StringBuilder?`

### Renamed types (ConsoleApplication only)

The demo application types were renamed to follow .NET conventions:

| Before | After |
|--------|-------|
| `ActionEnum` | `ProgramAction` |
| `CidrParseEnum` | `CidrParseMode` |

### Namespace changes (ConsoleApplication only)

| Before | After |
|--------|-------|
| `System.Net.ConsoleApplication` | `ConsoleApplication` |
| `System.Net.TestProject` | `TestProject` |

### Removed legacy CI

AppVeyor, Travis CI, and Coveralls configurations have been removed. CI/CD is now handled entirely by GitHub Actions.

## License

[BSD-2-Clause](LICENSE)
