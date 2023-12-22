using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Html;
using NUnit.Framework;
using Shouldly;
using Statiq.CodeAnalysis;
using Statiq.Common;
using Statiq.Testing;
using Statiq.Web;

namespace Statiq.Docs.Tests
{
    [TestFixture]
    public class IExecutionContextExtensionsFixture : BaseFixture
    {
        public class GetTypeLinkTests : IExecutionContextExtensionsFixture
        {
            [Test]
            public async Task ShouldReturnUnlinkedNameForSimpleType()
            {
                // Given
                const string code = @"
                    namespace Foo
                    {
                        public class Blue
                        {
                            string Green() => null;
                        }
                    }
                ";
                TestDocument document = new TestDocument(code);
                TestExecutionContext context = GetContext();
                IModule module = new AnalyzeCSharp();
                IReadOnlyList<TestDocument> results = await ExecuteAsync(document, context, module);
                IDocument typeDocument = GetMember(results, "Blue", "Green").Get<IDocument>(CodeAnalysisKeys.ReturnType);

                // When
                HtmlString typeLink = context.GetTypeLink(typeDocument);

                // Then
                typeLink.ToString().ShouldBe("string");
            }

            [Test]
            public async Task ShouldReturnUnlinkedNameForFrameworkType()
            {
                // Given
                const string code = @"
                    using System.Text;
                    namespace Foo
                    {
                        public class Blue
                        {
                            StringBuilder Green() => null;
                        }
                    }
                ";
                TestDocument document = new TestDocument(code);
                TestExecutionContext context = GetContext();
                IModule module = new AnalyzeCSharp();
                IReadOnlyList<TestDocument> results = await ExecuteAsync(document, context, module);
                IDocument typeDocument = GetMember(results, "Blue", "Green").Get<IDocument>(CodeAnalysisKeys.ReturnType);

                // When
                HtmlString typeLink = context.GetTypeLink(typeDocument);

                // Then
                typeLink.ToString().ShouldBe("StringBuilder");
            }

            [Test]
            public async Task ShouldReturnUnlinkedNameForGenericFrameworkType()
            {
                // Given
                const string code = @"
                    using System.Text;
                    namespace Foo
                    {
                        public class Blue
                        {
                            KeyValuePair<string, string> Green() => default;
                        }
                    }
                ";
                TestDocument document = new TestDocument(code);
                TestExecutionContext context = GetContext();
                IModule module = new AnalyzeCSharp();
                IReadOnlyList<TestDocument> results = await ExecuteAsync(document, context, module);
                IDocument typeDocument = GetMember(results, "Blue", "Green").Get<IDocument>(CodeAnalysisKeys.ReturnType);

                // When
                HtmlString typeLink = context.GetTypeLink(typeDocument);

                // Then
                typeLink.ToString().ShouldBe("KeyValuePair<wbr>&lt;string, <wbr>string&gt;<wbr>");
            }

            [Test]
            public async Task ShouldReturnLinkForClassType()
            {
                // Given
                const string code = @"
                    using System.Text;
                    namespace Foo
                    {
                        public class Blue
                        {
                            Red Green() => default;
                        }
                        public class Red
                        {
                        }
                    }
                ";
                TestDocument document = new TestDocument(code);
                TestExecutionContext context = GetContext();
                IModule module = new AnalyzeCSharp();
                IReadOnlyList<TestDocument> results = await ExecuteAsync(document, context, module);
                IDocument typeDocument = GetMember(results, "Blue", "Green").Get<IDocument>(CodeAnalysisKeys.ReturnType);

                // When
                HtmlString typeLink = context.GetTypeLink(typeDocument);

                // Then
                typeLink.ToString().ShouldBe(@"<a href=""/Foo/Red/index.html"">Red</a>");
            }

            [Test]
            public async Task ShouldReturnNameForSimpleNullableType()
            {
                // Given
                const string code = @"
                    using System.Text;
                    namespace Foo
                    {
                        public class Blue
                        {
                            int? Green() => default;
                        }
                    }
                ";
                TestDocument document = new TestDocument(code);
                TestExecutionContext context = GetContext();
                IModule module = new AnalyzeCSharp();
                IReadOnlyList<TestDocument> results = await ExecuteAsync(document, context, module);
                IDocument typeDocument = GetMember(results, "Blue", "Green").Get<IDocument>(CodeAnalysisKeys.ReturnType);

                // When
                HtmlString typeLink = context.GetTypeLink(typeDocument);

                // Then
                typeLink.ToString().ShouldBe(@"int?");
            }

            [Test]
            public async Task ShouldReturnLinkForNullableStruct()
            {
                // Given
                const string code = @"
                    using System.Text;
                    namespace Foo
                    {
                        public class Blue
                        {
                            Red? Green() => default;
                        }
                        public struct Red
                        {
                        }
                    }
                ";
                TestDocument document = new TestDocument(code);
                TestExecutionContext context = GetContext();
                IModule module = new AnalyzeCSharp();
                IReadOnlyList<TestDocument> results = await ExecuteAsync(document, context, module);
                IDocument typeDocument = GetMember(results, "Blue", "Green").Get<IDocument>(CodeAnalysisKeys.ReturnType);

                // When
                HtmlString typeLink = context.GetTypeLink(typeDocument);

                // Then
                typeLink.ToString().ShouldBe(@"<a href=""/Foo/Red/index.html"">Red?</a>");
            }

            [Test]
            public async Task ShouldReturnLinkForClosedGenericType()
            {
                // Given
                const string code = @"
                    using System.Text;
                    namespace Foo
                    {
                        public class Blue
                        {
                            Red<int> Green() => default;
                        }
                        public class Red<T>
                        {
                        }
                    }
                ";
                TestDocument document = new TestDocument(code);
                TestExecutionContext context = GetContext();
                IModule module = new AnalyzeCSharp();
                IReadOnlyList<TestDocument> results = await ExecuteAsync(document, context, module);
                IDocument typeDocument = GetMember(results, "Blue", "Green").Get<IDocument>(CodeAnalysisKeys.ReturnType);

                // When
                HtmlString typeLink = context.GetTypeLink(typeDocument);

                // Then
                typeLink.ToString().ShouldBe(@"<a href=""/Foo/Red_1/index.html"">Red</a><wbr>&lt;int&gt;<wbr>");
            }

            [Test]
            public async Task ShouldReturnLinkForOpenGenericType()
            {
                // Given
                const string code = @"
                    using System.Text;
                    namespace Foo
                    {
                        public class Blue
                        {
                            Red<T> Green<T>() => default;
                        }
                        public class Red<T>
                        {
                        }
                    }
                ";
                TestDocument document = new TestDocument(code);
                TestExecutionContext context = GetContext();
                IModule module = new AnalyzeCSharp();
                IReadOnlyList<TestDocument> results = await ExecuteAsync(document, context, module);
                IDocument typeDocument = GetMember(results, "Blue", "Green").Get<IDocument>(CodeAnalysisKeys.ReturnType);

                // When
                HtmlString typeLink = context.GetTypeLink(typeDocument);

                // Then
                typeLink.ToString().ShouldBe(@"<a href=""/Foo/Red_1/index.html"">Red</a><wbr>&lt;T&gt;<wbr>");
            }

            [Test]
            public async Task ShouldReturnNestedLinkForGenericType()
            {
                // Given
                const string code = @"
                    using System.Text;
                    namespace Foo
                    {
                        public class Blue
                        {
                            Red<Yellow> Green() => default;
                        }
                        public class Red<T>
                        {
                        }
                        public class Yellow
                        {
                        }
                    }
                ";
                TestDocument document = new TestDocument(code);
                TestExecutionContext context = GetContext();
                IModule module = new AnalyzeCSharp();
                IReadOnlyList<TestDocument> results = await ExecuteAsync(document, context, module);
                IDocument typeDocument = GetMember(results, "Blue", "Green").Get<IDocument>(CodeAnalysisKeys.ReturnType);

                // When
                HtmlString typeLink = context.GetTypeLink(typeDocument);

                // Then
                typeLink.ToString().ShouldBe(@"<a href=""/Foo/Red_1/index.html"">Red</a><wbr>&lt;<a href=""/Foo/Yellow/index.html"">Yellow</a>&gt;<wbr>");
            }

            [TestCase("(string Foo, string Bar)", @"(string Foo, <wbr>string Bar)")]
            [TestCase("(string Foo, string)", @"(string Foo, <wbr>string)")]
            [TestCase("(string, string)", @"(string, <wbr>string)")]
            [TestCase("(string, StringBuilder)", @"(string, <wbr>StringBuilder)")]
            [TestCase("(string Foo, Orange Bar)", @"(string Foo, <wbr><a href=""/Foo/Orange/index.html"">Orange</a> Bar)")]
            [TestCase("(string Foo, Red<string> Bar)", @"(string Foo, <wbr><a href=""/Foo/Red_1/index.html"">Red</a><wbr>&lt;string&gt;<wbr> Bar)")]
            [TestCase("(string Foo, Red<Orange> Bar)", @"(string Foo, <wbr><a href=""/Foo/Red_1/index.html"">Red</a><wbr>&lt;<a href=""/Foo/Orange/index.html"">Orange</a>&gt;<wbr> Bar)")]
            [TestCase("(string, Orange)", @"(string, <wbr><a href=""/Foo/Orange/index.html"">Orange</a>)")]
            [TestCase("(string Foo, Orange)", @"(string Foo, <wbr><a href=""/Foo/Orange/index.html"">Orange</a>)")]
            [TestCase("(string, Orange Foo)", @"(string, <wbr><a href=""/Foo/Orange/index.html"">Orange</a> Foo)")]
            [TestCase("(string, Red<string>)", @"(string, <wbr><a href=""/Foo/Red_1/index.html"">Red</a><wbr>&lt;string&gt;<wbr>)")]
            [TestCase("Tuple<string, string>", @"Tuple<wbr>&lt;string, <wbr>string&gt;<wbr>")]
            [TestCase("Tuple<string, StringBuilder>", @"Tuple<wbr>&lt;string, <wbr>StringBuilder&gt;<wbr>")]
            [TestCase("Tuple<string, Orange>", @"Tuple<wbr>&lt;string, <wbr><a href=""/Foo/Orange/index.html"">Orange</a>&gt;<wbr>")]
            [TestCase("Tuple<string, Red<string>>", @"Tuple<wbr>&lt;string, <wbr><a href=""/Foo/Red_1/index.html"">Red</a><wbr>&lt;string&gt;<wbr>&gt;<wbr>")]
            [TestCase("Tuple<string, Red<Orange>>", @"Tuple<wbr>&lt;string, <wbr><a href=""/Foo/Red_1/index.html"">Red</a><wbr>&lt;<a href=""/Foo/Orange/index.html"">Orange</a>&gt;<wbr>&gt;<wbr>")]
            public async Task ShouldHandleTupleSyntax(string tuple, string expected)
            {
                // Given
                string code = $@"
                    using System.Text;
                    namespace Foo
                    {{
                        public class Blue
                        {{
                            {tuple} Green() => (null, null);
                        }}
                        public class Orange
                        {{
                        }}
                        public class Red<T>
                        {{
                        }}
                    }}
                ";
                TestDocument document = new TestDocument(code);
                TestExecutionContext context = GetContext();
                IModule module = new AnalyzeCSharp();
                IReadOnlyList<TestDocument> results = await ExecuteAsync(document, context, module);
                IDocument typeDocument = GetMember(results, "Blue", "Green").Get<IDocument>(CodeAnalysisKeys.ReturnType);

                // When
                HtmlString typeLink = context.GetTypeLink(typeDocument);

                // Then
                typeLink.ToString().ShouldBe(expected);
            }

            // (string, string)
            // (string, StringBuilder
            // (string Foo, Red Bar)
            // (string Foo, Red<T> Bar)
            // Tuple<string, string>

            private TestExecutionContext GetContext() => new TestExecutionContext
            {
                FileSystem = new TestFileSystem
                {
                    RootPath = new NormalizedPath(TestContext.CurrentContext.TestDirectory)
                }
            };

            private IDocument GetMember(IReadOnlyList<IDocument> results, string className, string memberName) =>
                results.Single(x => x[CodeAnalysisKeys.Name].Equals(className))
                    .Get<IEnumerable<IDocument>>(CodeAnalysisKeys.Members)
                    .Single(x => x[CodeAnalysisKeys.Name].Equals(memberName));
        }
    }
}