using System;
using System.IO;
using System.Linq;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tooling.ProcessTasks;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

class BuildTasks : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main () => Execute<BuildTasks>(x => x.Build);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    AbsolutePath SolutionFile => RootDirectory / "Agi.LifeRunbook.slnx";
    AbsolutePath WebProjectDirectory => RootDirectory / "src" / "Agi.LifeRunbook.Web";
    AbsolutePath ArtifactsDirectory => RootDirectory / "artifacts";
    AbsolutePath TestResultsDirectory => ArtifactsDirectory / "test-results";

    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            if (Directory.Exists(ArtifactsDirectory))
            {
                Directory.Delete(ArtifactsDirectory, recursive: true);
            }

            Directory.CreateDirectory(ArtifactsDirectory);
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(settings => settings
                .SetProjectFile(SolutionFile));
        });

    Target InstallClientLibs => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            var abpCli = ToolPathResolver.GetPathExecutable("abp");
            if (abpCli is null)
            {
                throw new Exception("ABP CLI is required to restore web client libraries. Install it with: dotnet tool install -g Volo.Abp.Cli");
            }

            StartProcess(abpCli, "install-libs", WebProjectDirectory).AssertZeroExitCode();
        });

    Target Build => _ => _
        .DependsOn(InstallClientLibs)
        .Executes(() =>
        {
            DotNetBuild(settings => settings
                .SetProjectFile(SolutionFile)
                .SetConfiguration(Configuration)
                .EnableNoRestore());
        });

    Target Test => _ => _
        .DependsOn(Build)
        .Executes(() =>
        {
            Directory.CreateDirectory(TestResultsDirectory);
            DotNetTest(settings => settings
                .SetProjectFile(SolutionFile)
                .SetConfiguration(Configuration)
                .EnableNoRestore()
                .EnableNoBuild()
                .SetResultsDirectory(TestResultsDirectory)
                .SetLoggers("trx"));
        });

}
