using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Reflection;
using System.Text;
using SinumerikWrapperInterfaces;

namespace DynamicSinumerikWrapper
{
    public class DynamicSinumerikWrapperProvider
    {
        private const string NamespaceName = "StaticSinumerikWrapper";
        private const string ClassName = "SinumerikWrapper";
        private const string SinumerikOperateServicesWrapperName = "Siemens.Sinumerik.Operate.Services.Wrapper";

        public static DynamicSinumerikWrapperProvider Instance { get; } = new DynamicSinumerikWrapperProvider();

        public void GenerateClass()
        {
            try
            {
                //  var source = CreateSource();
                var source = LoadSource();
                // Write out method body
                Console.WriteLine(source);

                //Classic CodeDOM
                var codeCompiler = CodeDomProvider.CreateProvider("CSharp");

                ////Roslyn CodeDOM
                //var provOptions = new Dictionary<string, string>
                //{
                //    {"CompilerVersion", "v3.5"}
                //};

                //var codeCompiler = new CSharpCodeProvider(provOptions);

                Console.WriteLine($"Assembly name: {SinumerikOperateServicesWrapperName}");
                var path = GlobalAssemblyCacheHelper.GetAssemblyPath(SinumerikOperateServicesWrapperName);
                Console.WriteLine("GAC Path: " + path);
                var assembly = Assembly.LoadFile(path);
                Console.WriteLine("Load assembly from path: " + path);
                var name = assembly.GetName().FullName;
                Console.WriteLine("Name: " + name);
                var version = assembly.GetName().Version;
                Console.WriteLine("Version: " + version);

                var compilerParameters = new CompilerParameters();
 
                //           compilerParameters.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(SiemensMCSinumerikSolutionLineWrapper.SlAlarm)).Location);
                compilerParameters.ReferencedAssemblies.Add(typeof(ISinumerikWrapper).Assembly.Location);
                compilerParameters.ReferencedAssemblies.Add(assembly.Location);

                compilerParameters.GenerateInMemory = true;

                Console.WriteLine("Compile assembly");
                var results = codeCompiler.CompileAssemblyFromSource(compilerParameters, source);


                // Compiler output
                if (results.Errors.HasErrors)
                {
                    foreach (CompilerError error in results.Errors)
                    {
                        if (error.IsWarning)
                        {
                            Console.WriteLine("Warning: [" + error.ErrorNumber + "] " + error.ErrorText + " in line " + error.Line + " file " + error.FileName);

                        }
                        else
                        {
                            Console.WriteLine("Error: [" + error.ErrorNumber + "] " + error.ErrorText + " in line " + error.Line + " file " + error.FileName);

                        }
                    }
                }

                foreach (var line in results.Output)
                {
                    Console.WriteLine(line);
                }

                var type = results.CompiledAssembly.GetType(NamespaceName + "." + ClassName);
                if (type == null)
                {
                    Console.WriteLine("type not created");
                    return;
                }

                var ctor = type.GetConstructor();
                if (ctor == null)
                {
                    Console.WriteLine("ctor not created");
                    return;
                }
                SinumerikWrapper = ctor.Invoke<ISinumerikWrapper>();
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

       

        private string LoadSource()
        {
            var assembly = Assembly.GetExecutingAssembly();
            const string resourceName = "DynamicSinumerikWrapper.SinumerikWrapper.cs";

            string resource = null;
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream == null)
                {
                    return null;
                }

                using (var reader = new StreamReader(stream))
                {
                    resource = reader.ReadToEnd();
                }
            }
            return resource;
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
