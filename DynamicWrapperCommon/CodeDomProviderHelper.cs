using DynamicWrapperCommon.Exceptions;
using JetBrains.Annotations;
using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace DynamicWrapperCommon
{
    public static class CodeDomProviderHelper
    {
        public static CompilerResults CompileAssemblyFromEmbeddedSource([NotNull] this CodeDomProvider codeCompiler,
            [NotNull] CompilerParameters parameters, [NotNull] object self)
        {
            if (codeCompiler == null) throw new ArgumentNullException(nameof(codeCompiler));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            if (self == null) throw new ArgumentNullException(nameof(self));
            var sources = self.LoadEmbeddedSources().ToList();
            return codeCompiler.CompileAssemblyFromSource(parameters, sources.ToArray());
        }

        [Conditional("DEBUG")]
        public static void ConditionalIncludeDebugInformation([NotNull] this CompilerParameters compilerParameters)
        {
            compilerParameters.IncludeDebugInformation = true;
        }

        public static CompilerResults CompileAssemblyFromEmbeddedSource([NotNull] this CodeDomProvider codeCompiler,
            [NotNull] CompilerParameters parameters, [NotNull] object self, Version version)
        {
            if (codeCompiler == null) throw new ArgumentNullException(nameof(codeCompiler));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            if (self == null) throw new ArgumentNullException(nameof(self));
            var sources = self.LoadEmbeddedSources().ToList();
            sources.Add(AssemblyInfo(version.ToString()));
            return codeCompiler.CompileAssemblyFromSource(parameters, sources.ToArray());
        }

        private static string AssemblyInfo(string versionString)
        {
            return @"using System.Reflection;" + "\r\n" + "[assembly: AssemblyVersion(" + "\"" + versionString + "\"" + @")]";
        }

        public static CompilerResults CompileAssemblyFromEmbeddedSource([NotNull] this CodeDomProvider codeCompiler,
            [NotNull] CompilerParameters parameters, [NotNull] Assembly assembly)
        {
            if (codeCompiler == null) throw new ArgumentNullException(nameof(codeCompiler));
            if (parameters == null) throw new ArgumentNullException(nameof(parameters));
            if (assembly == null) throw new ArgumentNullException(nameof(assembly));
            return codeCompiler.CompileAssemblyFromSource(parameters, assembly.LoadEmbeddedSources().ToArray());
        }

        public static void WriteOutputToConsole([NotNull] this CompilerResults results)
        {
            if (results == null) throw new ArgumentNullException(nameof(results));
            foreach (var line in results.Output)
            {
                Console.WriteLine(line);
            }
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="results">The results.</param>
        /// <param name="namespaceName">Name of the namespace.</param>
        /// <param name="className">Name of the class.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// namespaceName
        /// or
        /// className
        /// </exception>
        [CanBeNull]
        public static T GetInstance<T>([NotNull] this CompilerResults results, [NotNull] string namespaceName, [NotNull] string className) where T : class
        {
            if (namespaceName == null) throw new ArgumentNullException(nameof(namespaceName));
            if (className == null) throw new ArgumentNullException(nameof(className));
            return results.GetInstance<T>(namespaceName + "." + className);
        }

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="results">The results.</param>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">
        /// results
        /// or
        /// name
        /// </exception>
        /// <exception cref="CompiledAssemblyException"></exception>
        /// <exception cref="GetTypeFromCompiledAssemblyException"></exception>
        [NotNull]
        public static T GetInstance<T>([NotNull] this CompilerResults results, [NotNull] string name) where T : class
        {
            if (results == null) throw new ArgumentNullException(nameof(results));
            if (name == null) throw new ArgumentNullException(nameof(name));
            var compiledAssembly = results.CompiledAssembly ?? throw new CompiledAssemblyException();
            var type = compiledAssembly.GetType(name) ?? throw new GetTypeFromCompiledAssemblyException(name, compiledAssembly);
            return type.GetInstance<T>();
        }

        /// <summary>
        /// Writes the errors to console.
        /// </summary>
        /// <param name="results">The results.</param>
        /// <exception cref="ArgumentNullException">results</exception>
        public static void WriteErrorsToConsole([NotNull] this CompilerResults results)
        {
            if (results == null) throw new ArgumentNullException(nameof(results));
            if (!results.Errors.HasErrors)
            {
                return;
            }
            foreach (CompilerError error in results.Errors)
            {
                if (error.IsWarning)
                {
                    Console.WriteLine("Warning: [" + error.ErrorNumber + "] " + error.ErrorText + " in line " +
                                      error.Line +
                                      " file " + error.FileName);
                }
                else
                {
                    Console.WriteLine("Error: [" + error.ErrorNumber + "] " + error.ErrorText + " in line " +
                                      error.Line +
                                      " file " + error.FileName);
                }
            }
        }
    }
}