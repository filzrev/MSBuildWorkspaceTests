
using FluentAssertions;
using Microsoft.Build.Framework;
using Microsoft.Build.Locator;
using Microsoft.Build.Logging;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using System.Diagnostics;

namespace MSBuildWorkspaceTests;

internal class Program
{
    static async Task Main(string[] args)
    {
        MSBuildLocator.RegisterDefaults();

        await MainCore();
    }

    private static async Task MainCore()
    {
        var msbuildProperties = new Dictionary<string, string>();
        {
            msbuildProperties["Configuration"] = "Release";
        }

        var msbuildLogger = new ConsoleLogger(LoggerVerbosity.Normal);

        var workspace = MSBuildWorkspace.Create(msbuildProperties);
        workspace.WorkspaceFailed += (sender, e) => Console.WriteLine($"{e.Diagnostic}");
        // workspace.LoadMetadataForReferencedProjects = true;

        var projectFiles = new[] {
            @"external/MonoGame/MonoGame.Framework.Content.Pipeline/MonoGame.Framework.Content.Pipeline.csproj",
            @"external/MonoGame/MonoGame.Framework/MonoGame.Framework.DesktopGL.csproj",
        }.Select(Path.GetFullPath).Reverse();

        List<Compilation> projectCompilations = new List<Compilation>();

        foreach (var projectFile in projectFiles)
        {
            if (await LoadCompilationFromProject(projectFile) is { } compilation)
            {
                projectCompilations.Add(compilation);
            }
        }

        int totalSymbolCount = 0;
        foreach (var compilation in projectCompilations)
        {
            var count = compilation.GetSymbolsWithName(x => true).Count();
            totalSymbolCount += count;
        }

        Console.WriteLine($"Count: {totalSymbolCount}");
        totalSymbolCount.Should().Be(54);
        return;

        // Local helper function
        async Task<Compilation?> LoadCompilationFromProject(string path)
        {
            var project = workspace.CurrentSolution.Projects.FirstOrDefault(p => StringComparer.OrdinalIgnoreCase.Equals(p.FilePath, path));

            if (project is null)
            {
                Console.WriteLine($"Loading project {path}");
                project = await workspace.OpenProjectAsync(path, msbuildLogger);
            }
            else
            {
                Console.WriteLine($"Specified project {path} is already loaded in workspace");
            }

            if (!project.SupportsCompilation)
            {
                Console.WriteLine($"Skip unsupported project {project.FilePath}.");
                return null;
            }

            return await project.GetCompilationAsync();
        }
    }


}
