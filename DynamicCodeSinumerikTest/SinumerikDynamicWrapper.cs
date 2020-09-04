using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace DynamicCodeSinumerikTest
{
    public class SinumerikDynamicWrapper
    {
        private const string NamespaceName = "DynamicWrappers";
        private const string ClassName = "DynamicWrapper";

        public static SinumerikDynamicWrapper Instance { get; } = new SinumerikDynamicWrapper();
        
        public void GenerateClass()

        {
            try
            {
                var builder = new StringBuilder();

                builder.Append($"namespace {NamespaceName} {{\r\n");

                builder.Append("    using System;\r\n");

                builder.Append("    using DynamicCodeSinumerikTest;\r\n");

                builder.Append("    using SiemensMCSinumerikSolutionLineWrapper;\r\n");
               
                builder.Append("    public class ");

                builder.Append(ClassName);

                builder.Append(" : DynamicCodeSinumerikTest.ISinumerikWrapper {\r\n");

                builder.Append("        private const int MAGIC_NUMBER_CONNECTED = 30;\r\n");

                builder.Append("        public bool CheckConnection()\r\n");
                builder.Append("           {\r\n");
                builder.Append("               try\r\n");
                builder.Append("               {\r\n");
                builder.Append("                   int result;\r\n");
                builder.Append("                   using (var dataSvc = new DataSvcWrapper(string.Empty))\r\n");
                builder.Append("                   {\r\n");
                builder.Append("                       var status = new DataSvcStatusWrapper();\r\n");
                builder.Append(
                    "                       var dataSvcItem = new DataSvcItem(\"connect_state\", string.Empty, null);\r\n");
                builder.Append(
                    "                       dataSvc.Read(ref dataSvcItem, 500, 0, true, ref status, false);\r\n");
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

                builder.Append("        public static void CopyProps()");

                builder.Append("        {\r\n");

                builder.Append("        }\r\n");

                builder.Append("    }\r\n");

                builder.Append("}\r\n");

                // Write out method body

                Console.WriteLine(builder.ToString());

                var codeCompiler = CodeDomProvider.CreateProvider("CSharp");

                var compilerParameters = new CompilerParameters();

                Console.WriteLine("Assembly: Siemens.Sinumerik.Operate.Services.Wrapper");

                var path = GetAssemblyPath("Siemens.Sinumerik.Operate.Services.Wrapper");
                Console.WriteLine("Path: " + path);

                var a = Assembly.LoadFile(path);

                var version = a.GetName().Version;
                Console.WriteLine("Version: " + version);

                //           compilerParameters.ReferencedAssemblies.Add(Assembly.GetAssembly(typeof(SiemensMCSinumerikSolutionLineWrapper.SlAlarm)).Location);
                 compilerParameters.ReferencedAssemblies.Add(typeof(ISinumerikWrapper).Assembly.Location);
                compilerParameters.ReferencedAssemblies.Add(a.Location);

                compilerParameters.GenerateInMemory = true;

                var results = codeCompiler.CompileAssemblyFromSource(compilerParameters, builder.ToString());


                // Compiler output

                foreach (var line in results.Output)
                {
                    Console.WriteLine(line);
                }

                var type = results.CompiledAssembly.GetType(NamespaceName +"." + ClassName);
                if (type == null)
                {
                    Console.WriteLine("type not created");
                }
                Type[] emptyArgumentTypes = Type.EmptyTypes;
                ConstructorInfo ctor = type.GetConstructor(emptyArgumentTypes);
                if (ctor == null)
                {
                    Console.WriteLine("ctor not created");
                }
                SinumerikWrapper = ctor.Invoke(new object[] { }) as ISinumerikWrapper;
                if (SinumerikWrapper == null)
                {
                    Console.WriteLine("SinumerikWrapper not created");

                }
                Console.WriteLine("Created");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public ISinumerikWrapper SinumerikWrapper { get; set; }


        public static string GetAssemblyPath(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");

            string finalName = name;
            AssemblyInfo aInfo = new AssemblyInfo();
            aInfo.cchBuf = 1024; // should be fine...
            aInfo.currentAssemblyPath = new String('\0', aInfo.cchBuf);

            IAssemblyCache ac;
            int hr = CreateAssemblyCache(out ac, 0);
            if (hr >= 0)
            {
                hr = ac.QueryAssemblyInfo(0, finalName, ref aInfo);
                if (hr < 0)
                    return null;
            }

            return aInfo.currentAssemblyPath;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct AssemblyInfo
        {
            public int cbAssemblyInfo;
            public int assemblyFlags;
            public long assemblySizeInKB;
            [MarshalAs(UnmanagedType.LPWStr)]
            public string currentAssemblyPath;
            public int cchBuf; // size of path buf.
        }

        [DllImport("fusion.dll")]
        private static extern int CreateAssemblyCache(out IAssemblyCache ppAsmCache, int reserved);

        [ComImport, InterfaceType(ComInterfaceType.InterfaceIsIUnknown), Guid("e707dcde-d1cd-11d2-bab9-00c04f8eceae")]
        private interface IAssemblyCache
        {
            void Reserved0();

            [PreserveSig]
            int QueryAssemblyInfo(int flags, [MarshalAs(UnmanagedType.LPWStr)] string assemblyName, ref AssemblyInfo assemblyInfo);
        }


       

    } 
    public interface ISinumerikWrapper
        {
            bool CheckConnection();
        }
}
