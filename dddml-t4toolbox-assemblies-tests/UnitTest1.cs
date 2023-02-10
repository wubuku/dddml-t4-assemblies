using System.Text.RegularExpressions;
using Dddml.Serialization;
using Dddml.T4.Extensions;
using T4Toolbox;
using Microsoft.VisualStudio.TextTemplating;
using System.Text;
using System.Diagnostics;
using Mono.TextTemplating;
using Microsoft.VisualStudio.TextTemplating.VSHost;
using System.CodeDom.Compiler;

namespace dddml_t4toolbox_assemblies_tests;

public class UnitTest1
{

    [Fact]
    public void TestTemplate1()
    {
        var dddmlProj = "/Users/yangjiefeng/wms/dddml/wms.project";
        var boundedContext = BoundedContextUtils.LoadFromProjectFile(dddmlProj);

        // ///////////////////////////////////////////
        boundedContext.Refresh();
        boundedContext.ReplacePropertyTypesWithBaseTypes();
        // ///////////////////////////////////////////
        var generationOptions = new GenerationOptions();
        generationOptions.Java = true;
        generationOptions.NoMViewObjects = true;
        boundedContext.PrepareGeneration(generationOptions);
        var mviewObjects = boundedContext.GetMViewObjects();
        // ///////////////////////////////////////////
        var aggregate = boundedContext.Aggregates["OrganizationStructure"];
        var generator = new Dddml.T4.Templates.Java.AggregateDomainGeneratorImporter.AggregateDomainGenerator(
            aggregate
        );
        generator.OnlyDtos = false;
        // ///////////////////////////////////////////
        //var host = new VisualStudioTextTemplateHost(null, null, new DefaultVariableResolver(null, null, null), null);
        var host = new Host();
        var hostContainer = new HostContainerTextTransformation(host);
        TransformationContext.Initialize(hostContainer, hostContainer.GetInternalGenerationEnvironment());
        
        generator.Run();
        
        TransformationContext.Cleanup();
        Debug.WriteLine(host.ContextOutputFiles);
        host.Cleanup();
    }

    public class Host : TemplateGenerator, IServiceProvider, ITextTemplatingComponents
    {
        TransformationContextProvider _transformationContextProvider;

        TransformationContextProvider TransformationContextProvider { get => _transformationContextProvider; }

        public OutputFile[] ContextOutputFiles
        {
            get => TransformationContextProvider != null ? TransformationContextProvider.OutputFiles : null;
        }

        public Host()
        {
            _transformationContextProvider = new TransformationContextProvider();
        }

        public void Cleanup()
        {
            _transformationContextProvider.Cleanup();
        }

        object? IServiceProvider.GetService(Type serviceType)
        {
            if (serviceType == typeof(ITransformationContextProvider))
            {
                return _transformationContextProvider;
            }
            throw new InvalidOperationException("GetService type: " + serviceType);
        }

        ITextTemplatingEngineHost ITextTemplatingComponents.Host => throw new NotImplementedException();

        object ITextTemplatingComponents.Hierarchy 
        { 
            get => null;//throw new NotImplementedException(); 
            set => throw new NotImplementedException(); 
        }

        ITextTemplatingCallback ITextTemplatingComponents.Callback { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        ITextTemplatingEngine ITextTemplatingComponents.Engine => throw new NotImplementedException();

        string ITextTemplatingComponents.InputFile { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    }

    class TransformationContextProvider : ITransformationContextProvider
    {
        private OutputFile[] _outputFiles;

        public OutputFile[] OutputFiles { get => _outputFiles; }

        public void Cleanup() 
        {
            this._outputFiles = null;
        }

        string ITransformationContextProvider.GetMetadataValue(object hierarchy, string fileName, string metadataName)
        {
            return null;
        }

        string ITransformationContextProvider.GetPropertyValue(object hierarchy, string propertyName)
        {
            throw new NotImplementedException();
        }

        void ITransformationContextProvider.UpdateOutputFiles(string inputFile, OutputFile[] outputFiles)
        {
            this._outputFiles = outputFiles;
        }
    }

    class HostContainerTextTransformation : TextTransformation
    {
        public ITextTemplatingEngineHost Host { get; set; }

        public HostContainerTextTransformation(ITextTemplatingEngineHost host) 
        {
            this.Host = host;
        }

        internal StringBuilder GetInternalGenerationEnvironment()
        {
            return GenerationEnvironment;
        }

        public override string TransformText()
        {
            return GenerationEnvironment.ToString();
        }
    }

    [Fact]
    public void Test2()
    {
        //string input = "This is a sentence with 123 numbers.";
        //string pattern = @"\d+";
        //string replacement = "***";
        //string output = Regex.Replace(input, pattern, replacement);
        //Console.WriteLine(output);
        Tuple<string, string>[] rs = new Tuple<string, string>[]
        {
            new Tuple<string, string>(@"^.*(Dddml\.[^\\\/]*\.dll)$", "$1"),
            new Tuple<string, string>(@"^.*(YamlDotNet.*\.dll)$", "$1")
        };

        var refs = new string[]
        {
            @"%DddmlDotNetToolsSolutionDir%\Dddml.Core\bin\Debug\Dddml.Core.dll",
            @"%DddmlDotNetToolsSolutionDir%\Dddml.Serialization\bin\Debug\Dddml.Serialization.dll",
            @"%DddmlDotNetToolsSolutionDir%\Dddml.T4\bin\Debug\Dddml.T4.dll",
            @"%DddmlDotNetToolsSolutionDir%\packages\YamlDotNet.3.8.0\lib\net35\YamlDotNet.dll",

        };

        foreach (var @ref in refs)
        {
            var r = rs.Where(r => Regex.Match(@ref, r.Item1).Success).FirstOrDefault();
            if (r != null)
            {
                var newRef = Regex.Replace(@ref, r.Item1, r.Item2);
                System.Diagnostics.Debug.WriteLine(newRef);
            }
        }
    }

    // ////////////////////////////////////////
    private static readonly IList<Regex> SpecificAssemblyReferenceRegexList = new Regex[] {
            new Regex(@"^Dddml\.[^\\\/]*\.dll$"),
            new Regex(@"^YamlDotNet.*\.dll$"),
        };
    // ////////////////////////////////////////

    [Fact]
    public void Test1()
    {
        var refs = new string[]
        {
            @"%DddmlDotNetToolsSolutionDir%\Dddml.Core\bin\Debug\Dddml.Core.dll",
            @"%DddmlDotNetToolsSolutionDir%\Dddml.Serialization\bin\Debug\Dddml.Serialization.dll",
            @"%DddmlDotNetToolsSolutionDir%\Dddml.T4\bin\Debug\Dddml.T4.dll",
            @"%DddmlDotNetToolsSolutionDir%\packages\YamlDotNet.3.8.0\lib\net35\YamlDotNet.dll",

        };
        refs.ToList().ForEach(@ref =>
        {
            @ref = @ref.Replace("\\", Path.DirectorySeparatorChar.ToString());
            // replace some specific references...
            if (!String.IsNullOrEmpty(@ref))
            {
                if (!File.Exists(@ref))
                {
                    var fileName = Path.GetFileName(@ref);
                    var regex = SpecificAssemblyReferenceRegexList.Where(r => r.Match(fileName).Success).FirstOrDefault();
                    if (regex != null)
                    {
                        //try again
                        System.Diagnostics.Debug.WriteLine(fileName);
                    }
                }
            }

        });
    }
}
