using System;
using System.CodeDom.Compiler;
using System.Text;
using DynamicWrapperCommon;
using SinumerikWrapperInterfaces;

namespace ExperimentalDynamicSinumerikWrapper
{
    public class ExperimentalDynamicSinumerikWrapperProvider
    {
        private const string NamespaceName = "StaticSinumerikWrapper";
        private const string ClassName = "SinumerikWrapper";
        private const string SinumerikOperateServicesWrapperName = "Siemens.Sinumerik.Operate.Services.Wrapper";
        private const string SinumerikOperateServicesName = "Siemens.Sinumerik.Operate.Services";

        public static ExperimentalDynamicSinumerikWrapperProvider Instance { get; } = new ExperimentalDynamicSinumerikWrapperProvider();

        public void GenerateClass()
        {
            try
            {
                //  var source = CreateSource();
                //               var source = this.LoadEmbeddedSource("SinumerikWrapper.cs");
                ////var sources = this.LoadEmbeddedSources();
                ////Console.WriteLine(sources);

                //Classic CodeDOM
                var codeCompiler = CodeDomProvider.CreateProvider("CSharp");

                ////Roslyn CodeDOM
                //var provOptions = new Dictionary<string, string>
                //{
                //    {"CompilerVersion", "v3.5"}
                //};
                //var codeCompiler = new CSharpCodeProvider(provOptions);

                Console.WriteLine($"Assembly name: {SinumerikOperateServicesName}");
                var sinumerikOperateServicesAssembly = GlobalAssemblyCacheHelper.LoadAssembly(SinumerikOperateServicesName);
                if (sinumerikOperateServicesAssembly == null)
                {
                    Console.WriteLine("assembly not Loaded");
                    return;

                }

                {
                    var name = sinumerikOperateServicesAssembly.GetName().FullName;
                    Console.WriteLine("Name: " + name);
                    var version = sinumerikOperateServicesAssembly.GetName().Version;
                    Console.WriteLine("Version: " + version);
                }

                Console.WriteLine($"Assembly name: {SinumerikOperateServicesWrapperName}");
                var sinumerikOperateServicesWrapperAssembly = GlobalAssemblyCacheHelper.LoadAssembly(SinumerikOperateServicesWrapperName);
                if (sinumerikOperateServicesWrapperAssembly == null)
                {
                    Console.WriteLine("assembly not Loaded");
                    return;

                }

                {
                    var name = sinumerikOperateServicesWrapperAssembly.GetName().FullName;
                    Console.WriteLine("Name: " + name);
                    var version = sinumerikOperateServicesWrapperAssembly.GetName().Version;
                    Console.WriteLine("Version: " + version);
                }
                var compilerParameters = new CompilerParameters();
                compilerParameters.ReferencedAssemblies.Add(typeof(ISinumerikWrapper).Assembly.Location);
                compilerParameters.ReferencedAssemblies.Add(sinumerikOperateServicesAssembly.Location);
                compilerParameters.ReferencedAssemblies.Add(sinumerikOperateServicesWrapperAssembly.Location);
                compilerParameters.GenerateInMemory = true;

                Console.WriteLine("Compile assembly");
                //  var results = codeCompiler.CompileAssemblyFromSource(compilerParameters, sources.ToArray());
                var results = codeCompiler.CompileAssemblyFromEmbeddedSource(compilerParameters, this);
                results.WriteErrorsToConsole();
                results.WriteOutputToConsole();

                SinumerikWrapper = results.GetInstance<ISinumerikWrapper>(NamespaceName + "." + ClassName);
                if (SinumerikWrapper == null)
                {
                    Console.WriteLine("SinumerikWrapper not created");
                    return;
                }
                Console.WriteLine("Created");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        public void GenerateClassWithAssemblyCheck()
        {
            try
            {
                var sinumerikOperateServicesAssembly = GlobalAssemblyCacheHelper.LoadAssembly(SinumerikOperateServicesName);
                var sinumerikOperateServicesWrapperAssembly = GlobalAssemblyCacheHelper.LoadAssembly(SinumerikOperateServicesWrapperName);

                var compilerParameters = new CompilerParameters();
                compilerParameters.ReferencedAssemblies.Add(typeof(ISinumerikWrapper).Assembly.Location);
                compilerParameters.ReferencedAssemblies.Add(sinumerikOperateServicesAssembly.Location);
                compilerParameters.ReferencedAssemblies.Add(sinumerikOperateServicesWrapperAssembly.Location);
                compilerParameters.GenerateInMemory = true;

                var codeCompiler = CodeDomProvider.CreateProvider("CSharp");
                var results = codeCompiler.CompileAssemblyFromEmbeddedSource(compilerParameters, this);
                results.WriteErrorsToConsole();
                results.WriteOutputToConsole();

                SinumerikWrapper = results.GetInstance<ISinumerikWrapper>(NamespaceName + "." + ClassName);
                if (SinumerikWrapper == null)
                {
                    Console.WriteLine("SinumerikWrapper not created");
                    return;
                }
                Console.WriteLine("Created");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private string CreateSource()
        {
            var builder = new StringBuilder();
            builder.Append($"namespace {NamespaceName} {{\r\n");
            builder.Append("    using System;\r\n");
            builder.Append("    using SinumerikWrapperInterfaces;\r\n");
            builder.Append("    using SiemensMCSinumerikSolutionLineWrapper;\r\n");
            builder.Append($"    public class {ClassName} : SinumerikWrapperInterfaces.ISinumerikWrapper {{\r\n");
            builder.Append("        private const int MAGIC_NUMBER_CONNECTED = 30;\r\n");
            builder.Append("        public bool CheckConnection()\r\n");
            builder.Append("           {\r\n");
            builder.Append("               try\r\n");
            builder.Append("               {\r\n");
            builder.Append("                   int result;\r\n");
            builder.Append("                   using (var dataSvc = new DataSvcWrapper(string.Empty))\r\n");
            builder.Append("                   {\r\n");
            builder.Append("                       var status = new DataSvcStatusWrapper();\r\n");
            builder.Append("                       var dataSvcItem = new DataSvcItem(\"connect_state\", string.Empty, null);\r\n");
            builder.Append("                       dataSvc.Read(ref dataSvcItem, 500, 0, true, ref status, false);\r\n");
            builder.Append("                       result = Convert.ToInt32(dataSvcItem.Value);\r\n");
            builder.Append("                   }\r\n");
            builder.Append("                   if (result == MAGIC_NUMBER_CONNECTED)\r\n");
            builder.Append("                   {\r\n");
            builder.Append("                       return true;\r\n");
            builder.Append("                   }\r\n");
            builder.Append("               }\r\n");
            builder.Append("               catch (DataSvcException)\r\n");
            builder.Append("               {\r\n");
            builder.Append("               }\r\n");
            builder.Append("               catch (InvalidCastException)\r\n");
            builder.Append("               {\r\n");
            builder.Append("               }\r\n");
            builder.Append("               catch (FormatException)\r\n");
            builder.Append("               {\r\n");
            builder.Append("               }\r\n");
            builder.Append("               return false;\r\n");
            builder.Append("           }\r\n");
            builder.Append("    }\r\n");
            builder.Append("}\r\n");
            return builder.ToString();
        }

        public ISinumerikWrapper SinumerikWrapper { get; private set; }

    }
}
