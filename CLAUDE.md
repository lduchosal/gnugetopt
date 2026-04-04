# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

C# .NET port of GNU getopt (from glibc 2.0.6, via a Java port). Published as the `GnuGetOpt` NuGet package (version 0.9.2). The core library targets `netstandard2.0`.

## Build & Test Commands

```bash
# All commands run from the repo root
dotnet restore src
dotnet build src
dotnet test src/TestProject/TestProject.csproj

# Test with coverage
dotnet test src/TestProject/TestProject.csproj /p:CollectCoverage=true /p:CoverletOutputFormat=opencover

# Pack NuGet package
dotnet pack -c release src/Gnu.Getopt
```

## Solution Structure

The solution file is at `src/gnugetopt.sln` with three projects:

- **`src/Gnu.Getopt/`** — Core library (netstandard2.0). `Getopt.cs` is the main parser class (~1500 lines). `LongOpt.cs` defines long option types. Includes localized error messages (en, de, fr, hu, ja, nl, no) via Resources.
- **`src/ConsoleApplication/`** — Demo app showing getopt usage for IP network manipulation. Multi-targets Windows-only frameworks (net462–net48) plus netcoreapp3.1/net5.0/net6.0.
- **`src/TestProject/`** — MSTest unit tests (`ConsoleUnitTest.cs`, 11 test methods). On non-Windows, only targets net6.0.

## Architecture

The parser follows a stateful iteration pattern: instantiate `Getopt` with argv and an option string (e.g., `"abc:d::"`), then call `getopt()` in a loop until it returns `-1`. The current argument is in `Optarg`. Long options are defined via `LongOpt` objects passed to the constructor.

The ConsoleApplication demonstrates a callback-based delegation pattern: `ArgParsed` objects map option characters to delegate handlers that mutate a shared `ProgramContext`.

## Platform Notes

- Assembly signing (`Gnu.Getopt.snk`) is enabled on Windows only — conditional in the csproj.
- ConsoleApplication and TestProject multi-target many frameworks on Windows but only net6.0 on other platforms.

## CI/CD

- **GitHub Actions**: `build.yml` (SonarCloud on Windows), `dotnet.yml` (build/pack on Ubuntu), `net6.yml` (test on Ubuntu)
- **AppVeyor**: Windows builds with multi-framework testing, coveralls coverage, NuGet deployment
- **Mergify**: Auto-merges Dependabot PRs when CI passes

## Code Style

StyleCop.Analyzers is configured with relaxed documentation rules (most set to "suggestion"). See `stylecop.json` files and `src/.editorconfig` for details.
