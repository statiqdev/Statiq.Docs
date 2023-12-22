using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using Shouldly;
using Statiq.App;
using Statiq.CodeAnalysis;
using Statiq.Common;
using Statiq.Testing;
using Statiq.Web;

namespace Statiq.Docs.Tests.Pipelines
{
    [TestFixture]
    public class ApiFixture : BaseFixture
    {
        public class ExecuteTests : ApiFixture
        {
            [Test]
            public async Task ShouldCreateXrefForClass()
            {
                // Given
                App.Bootstrapper bootstrapper = App.Bootstrapper
                    .Factory
                    .CreateDocs(Array.Empty<string>());
                TestFileProvider fileProvider = new TestFileProvider
                {
                    {
                        "/input/index.md",
                        "Index"
                    },
                    {
                        "/src/code.cs",
                        @"
                            using System.Text;
                            namespace Foo
                            {
                                public class Blue
                                {
                                    public Red Green() => default;
                                }
                                public class Red
                                {
                                }
                            }
                        "
                    }
                };

                // When
                BootstrapperTestResult result = await bootstrapper.RunTestAsync(fileProvider);

                // Then
                result.ExitCode.ShouldBe((int)ExitCode.Normal);
                IDocument document = result
                    .Outputs[nameof(Statiq.Docs.Pipelines.Api)][Phase.Process]
                    .Single(x => x[CodeAnalysisKeys.Name].Equals("Blue"));
                document.GetString(WebKeys.Xref).ShouldBe("api-Foo.Blue");
            }

            [Test]
            public async Task ShouldCreateXrefForGenericClass()
            {
                // Given
                App.Bootstrapper bootstrapper = App.Bootstrapper
                    .Factory
                    .CreateDocs(Array.Empty<string>());
                TestFileProvider fileProvider = new TestFileProvider
                {
                    {
                        "/input/index.md",
                        "Index"
                    },
                    {
                        "/src/code.cs",
                        @"
                            using System.Text;
                            namespace Foo
                            {
                                public class Blue<Fizz>
                                {
                                    public Red Green() => default;
                                }
                                public class Red
                                {
                                }
                            }
                        "
                    }
                };

                // When
                BootstrapperTestResult result = await bootstrapper.RunTestAsync(fileProvider);

                // Then
                result.ExitCode.ShouldBe((int)ExitCode.Normal);
                IDocument document = result
                    .Outputs[nameof(Statiq.Docs.Pipelines.Api)][Phase.Process]
                    .Single(x => x[CodeAnalysisKeys.Name].Equals("Blue"));
                document.GetString(WebKeys.Xref).ShouldBe("api-Foo.Blue-Fizz-");
            }

            [Test]
            public async Task ShouldCreateXrefForMethod()
            {
                // Given
                App.Bootstrapper bootstrapper = App.Bootstrapper
                    .Factory
                    .CreateDocs(Array.Empty<string>());
                TestFileProvider fileProvider = new TestFileProvider
                {
                    {
                        "/input/index.md",
                        "Index"
                    },
                    {
                        "/src/code.cs",
                        @"
                            using System.Text;
                            namespace Foo
                            {
                                public class Blue
                                {
                                    public Red Green() { return null };
                                }
                                public class Red
                                {
                                }
                            }
                        "
                    }
                };

                // When
                BootstrapperTestResult result = await bootstrapper.RunTestAsync(fileProvider);

                // Then
                result.ExitCode.ShouldBe((int)ExitCode.Normal);
                IDocument document = result
                    .Outputs[nameof(Statiq.Docs.Pipelines.Api)][Phase.Process]
                    .Single(x => x[CodeAnalysisKeys.Name].Equals("Green"));
                document.GetString(WebKeys.Xref).ShouldBe("api-Foo.Blue.Green()");
            }

            [Test]
            public async Task ShouldCreateXrefForProperty()
            {
                // Given
                App.Bootstrapper bootstrapper = App.Bootstrapper
                    .Factory
                    .CreateDocs(Array.Empty<string>());
                TestFileProvider fileProvider = new TestFileProvider
                {
                    {
                        "/input/index.md",
                        "Index"
                    },
                    {
                        "/src/code.cs",
                        @"
                            using System.Text;
                            namespace Foo
                            {
                                public class Blue
                                {
                                    public Red Green => default;
                                }
                                public class Red
                                {
                                }
                            }
                        "
                    }
                };

                // When
                BootstrapperTestResult result = await bootstrapper.RunTestAsync(fileProvider);

                // Then
                result.ExitCode.ShouldBe((int)ExitCode.Normal);
                IDocument document = result
                    .Outputs[nameof(Statiq.Docs.Pipelines.Api)][Phase.Process]
                    .Single(x => x[CodeAnalysisKeys.Name].Equals("Green"));
                document.GetString(WebKeys.Xref).ShouldBe("api-Foo.Blue.Green");
            }

            [Test]
            public async Task ShouldCreateXrefForField()
            {
                // Given
                App.Bootstrapper bootstrapper = App.Bootstrapper
                    .Factory
                    .CreateDocs(Array.Empty<string>());
                TestFileProvider fileProvider = new TestFileProvider
                {
                    {
                        "/input/index.md",
                        "Index"
                    },
                    {
                        "/src/code.cs",
                        @"
                            using System.Text;
                            namespace Foo
                            {
                                public class Blue
                                {
                                    public Red Green = default;
                                }
                                public class Red
                                {
                                }
                            }
                        "
                    }
                };

                // When
                BootstrapperTestResult result = await bootstrapper.RunTestAsync(fileProvider);

                // Then
                result.ExitCode.ShouldBe((int)ExitCode.Normal);
                IDocument document = result
                    .Outputs[nameof(Statiq.Docs.Pipelines.Api)][Phase.Process]
                    .Single(x => x[CodeAnalysisKeys.Name].Equals("Green"));
                document.GetString(WebKeys.Xref).ShouldBe("api-Foo.Blue.Green");
            }
        }
    }
}