using DynamicWrapperCommon;
using SinumerikWrapperInterfaces;
using System;
using System.CodeDom.Compiler;


namespace DynamicSinumerikWrapper
{
    public class DynamicSinumerikWrapperProvider
    {
        private const string NamespaceName = "StaticSinumerikWrapper";
        private const string ClassName = "SinumerikWrapper";
        private const string SinumerikOperateServicesWrapperName = "Siemens.Sinumerik.Operate.Services.Wrapper";
        private const string SinumerikOperateServicesName = "Siemens.Sinumerik.Operate.Services";
        private const string CompilerParametersOutputAssembly = "ShadowSinumerikWrapper";

        public static DynamicSinumerikWrapperProvider Instance { get; } = new DynamicSinumerikWrapperProvider();

        public void GenerateClass()
        {
            try
            {

                var x = GlobalAssemblyCacheHelper.GetAssemblies(SinumerikOperateServicesName);
                var sinumerikOperateServicesAssembly = GlobalAssemblyCacheHelper.LoadAssembly(SinumerikOperateServicesName);

                var compilerParameters = new CompilerParameters();
                compilerParameters.ReferencedAssemblies.Add(typeof(ISinumerikWrapper).Assembly.Location);
                compilerParameters.ReferencedAssemblies.Add(sinumerikOperateServicesAssembly.Location);
                compilerParameters.ReferencedAssemblies.Add(GlobalAssemblyCacheHelper.GetAssemblyLocation(SinumerikOperateServicesWrapperName));
                compilerParameters.GenerateInMemory = true;
                compilerParameters.OutputAssembly = CompilerParametersOutputAssembly;
                compilerParameters.ConditionalIncludeDebugInformation();

                var codeCompiler = CodeDomProvider.CreateProvider("CSharp");
                var results = codeCompiler.CompileAssemblyFromEmbeddedSource(compilerParameters, this, sinumerikOperateServicesAssembly.GetName().Version);
                
                results.WriteErrorsToConsole();
                results.WriteOutputToConsole();
                Console.WriteLine(results.CompiledAssembly.FullName);

                SinumerikWrapper = results.GetInstance<ISinumerikWrapper>(NamespaceName + "." + ClassName);
                Console.WriteLine("SinumerikWrapper Created");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

       

        public ISinumerikWrapper SinumerikWrapper { get; private set; }
    }
}