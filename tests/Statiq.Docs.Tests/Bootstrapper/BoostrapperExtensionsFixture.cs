using System;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;
using Statiq.App;
using Statiq.Common;
using Statiq.Testing;

namespace Statiq.Docs.Tests.Bootstrapper
{
    [TestFixture]
    public class BoostrapperExtensionsFixture : BaseFixture
    {
        public class AddDocsTests : BoostrapperExtensionsFixture
        {
            [Test]
            public async Task AddsDefaultSourceFiles()
            {
                // Given
                App.Bootstrapper bootstrapper = App.Bootstrapper
                    .Factory
                    .CreateDocs(Array.Empty<string>());

                // When
                BootstrapperTestResult result = await bootstrapper.RunTestAsync();

                // Then
                result.ExitCode.ShouldBe((int)ExitCode.Normal);
                result.Engine.Settings.GetList<string>(DocsKeys.SourceFiles)
                    .ShouldBe(new[]
                    {
                        "../src/**/{!.git,!bin,!obj,!packages,!*.Tests,}/**/*.cs",
                        "../../src/**/{!.git,!bin,!obj,!packages,!*.Tests,}/**/*.cs"
                    });
            }
        }

        public class AddSourceFilesTests : BoostrapperExtensionsFixture
        {
            [Test]
            public async Task ReplacesDefaultSourceFiles()
            {
                // Given
                App.Bootstrapper bootstrapper = App.Bootstrapper
                    .Factory
                    .CreateDocs(Array.Empty<string>())
                    .AddSourceFiles("**/foo/*.cshtml");

                // When
                BootstrapperTestResult result = await bootstrapper.RunTestAsync();

                // Then
                result.ExitCode.ShouldBe((int)ExitCode.Normal);
                result.Engine.Settings.GetList<string>(DocsKeys.SourceFiles)
                    .ShouldBe(new[] { "**/foo/*.cshtml" });
            }

            [Test]
            public async Task AddsMultipleTimes()
            {
                // Given
                App.Bootstrapper bootstrapper = App.Bootstrapper
                    .Factory
                    .CreateDocs(Array.Empty<string>())
                    .AddSourceFiles("**/foo/*.cshtml")
                    .AddSourceFiles("**/bar/*.cshtml");

                // When
                BootstrapperTestResult result = await bootstrapper.RunTestAsync();

                // Then
                result.ExitCode.ShouldBe((int)ExitCode.Normal);
                result.Engine.Settings.GetList<string>(DocsKeys.SourceFiles)
                    .ShouldBe(
                        new[]
                        {
                            "**/foo/*.cshtml",
                            "**/bar/*.cshtml"
                        },
                        true);
            }

            [Test]
            public async Task KeepsDefaultIfAdded()
            {
                // Given
                App.Bootstrapper bootstrapper = App.Bootstrapper
                    .Factory
                    .CreateDocs(Array.Empty<string>())
                    .AddSourceFiles(
                        "src/**/{!.git,!bin,!obj,!packages,!*.Tests,}/**/*.cs",
                        "../src/**/{!.git,!bin,!obj,!packages,!*.Tests,}/**/*.cs");

                // When
                BootstrapperTestResult result = await bootstrapper.RunTestAsync();

                // Then
                result.ExitCode.ShouldBe((int)ExitCode.Normal);
                result.Engine.Settings.GetList<string>(DocsKeys.SourceFiles)
                    .ShouldBe(
                        new[]
                        {
                            "src/**/{!.git,!bin,!obj,!packages,!*.Tests,}/**/*.cs",
                            "../src/**/{!.git,!bin,!obj,!packages,!*.Tests,}/**/*.cs"
                        },
                        true);
            }

            [Test]
            public async Task KeepsDefaultIfNull()
            {
                // Given
                App.Bootstrapper bootstrapper = App.Bootstrapper
                    .Factory
                    .CreateDocs(Array.Empty<string>())
                    .AddSourceFiles(null);

                // When
                BootstrapperTestResult result = await bootstrapper.RunTestAsync();

                // Then
                result.ExitCode.ShouldBe((int)ExitCode.Normal);
                result.Engine.Settings.GetList<string>(DocsKeys.SourceFiles)
                    .ShouldBe(
                        new[]
                        {
                            "../src/**/{!.git,!bin,!obj,!packages,!*.Tests,}/**/*.cs",
                            "../../src/**/{!.git,!bin,!obj,!packages,!*.Tests,}/**/*.cs"
                        },
                        true);
            }

            [Test]
            public async Task KeepsDefaultIfEmpty()
            {
                // Given
                App.Bootstrapper bootstrapper = App.Bootstrapper
                    .Factory
                    .CreateDocs(Array.Empty<string>())
                    .AddSourceFiles(Array.Empty<string>());

                // When
                BootstrapperTestResult result = await bootstrapper.RunTestAsync();

                // Then
                result.ExitCode.ShouldBe((int)ExitCode.Normal);
                result.Engine.Settings.GetList<string>(DocsKeys.SourceFiles)
                    .ShouldBe(
                        new[]
                        {
                            "../src/**/{!.git,!bin,!obj,!packages,!*.Tests,}/**/*.cs",
                            "../../src/**/{!.git,!bin,!obj,!packages,!*.Tests,}/**/*.cs"
                        },
                        true);
            }
        }

        public class AddProjectFilesTests : BoostrapperExtensionsFixture
        {
            [Test]
            public async Task AddsProjectFiles()
            {
                // Given
                App.Bootstrapper bootstrapper = App.Bootstrapper
                    .Factory
                    .CreateDocs(Array.Empty<string>())
                    .AddProjectFiles("**/foo/*.csproj")
                    .AddProjectFiles("**/bar/*.csproj");

                // When
                BootstrapperTestResult result = await bootstrapper.RunTestAsync();

                // Then
                result.ExitCode.ShouldBe((int)ExitCode.Normal);
                result.Engine.Settings.GetList<string>(DocsKeys.ProjectFiles)
                    .ShouldBe(
                        new[]
                        {
                            "**/foo/*.csproj",
                            "**/bar/*.csproj"
                        },
                        true);
            }
        }

        public class AddSolutionFilesTests : BoostrapperExtensionsFixture
        {
            [Test]
            public async Task AddsSolutionFiles()
            {
                // Given
                App.Bootstrapper bootstrapper = App.Bootstrapper
                    .Factory
                    .CreateDocs(Array.Empty<string>())
                    .AddSolutionFiles("**/foo/*.sln")
                    .AddSolutionFiles("**/bar/*.sln");

                // When
                BootstrapperTestResult result = await bootstrapper.RunTestAsync();

                // Then
                result.ExitCode.ShouldBe((int)ExitCode.Normal);
                result.Engine.Settings.GetList<string>(DocsKeys.SolutionFiles)
                    .ShouldBe(
                        new[]
                        {
                            "**/foo/*.sln",
                            "**/bar/*.sln"
                        },
                        true);
            }
        }

        public class AddAssemblyFilesTests : BoostrapperExtensionsFixture
        {
            [Test]
            public async Task AddsAssemblyFiles()
            {
                // Given
                App.Bootstrapper bootstrapper = App.Bootstrapper
                    .Factory
                    .CreateDocs(Array.Empty<string>())
                    .AddAssemblyFiles("**/foo/*.dll")
                    .AddAssemblyFiles("**/bar/*.dll");

                // When
                BootstrapperTestResult result = await bootstrapper.RunTestAsync();

                // Then
                result.ExitCode.ShouldBe((int)ExitCode.Normal);
                result.Engine.Settings.GetList<string>(DocsKeys.AssemblyFiles)
                    .ShouldBe(
                        new[]
                        {
                            "**/foo/*.dll",
                            "**/bar/*.dll"
                        },
                        true);
            }
        }
    }
}