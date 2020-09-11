using DynamicWrapperCommon.Exceptions;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace DynamicWrapperCommon
{
    public static partial class GlobalAssemblyCacheHelper
    {
        /// <summary>
        ///     The buffer size
        /// </summary>
        private const int BufferSize = 1024;

        /// <summary>
        ///     The initial character
        /// </summary>
        private const char InitialChar = '\0';

        /// <summary>
        ///     Gets the assembly path.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">name</exception>
        [NotNull]
        public static string GetAssemblyLocation([NotNull] string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            var result = WinApi.CreateAssemblyCache(out var assemblyCache, 0);
            if (result < WinApi.HResult.SOk)
            {
                throw new CreateAssemblyCacheException((int)result);
            }

            var info = new WinApi.AssemblyInfo
            {
                cchBuf = BufferSize,
                currentAssemblyPath = new string(InitialChar, BufferSize)
            };

            
            result = assemblyCache.QueryAssemblyInfo(WinApi.QueryAssemblyInfoFlags.Validate, name, ref info);
            if (result < WinApi.HResult.SOk)
            {
                throw new QueryAssemblyInfoException(name, (int)result);
            }

            return info.currentAssemblyPath;
        }

        

        /// <summary>
        ///     Loads the assembly.
        /// </summary>
        /// <param name="assemblyName">The assembly name.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">name</exception>
        [NotNull]
        public static Assembly LoadAssembly([NotNull] string assemblyName)
        {
            if (assemblyName == null)
            {
                throw new ArgumentNullException(nameof(assemblyName));
            }

            var path = GetAssemblyLocation(assemblyName);
            return Assembly.LoadFile(path);
        }

        public static bool TryGetAssemblyLocation([NotNull] AssemblyName name, out string path)
        {
            var culture = name.CultureName;
            if (culture == "")
                culture = "neutral";
            var text = $"{name.Name}, Version={name.Version}, Culture={culture}, PublicKeyToken={GetPublicKeyTokenFromAssembly(name)}, processorArchitecture={name.ProcessorArchitecture.ToString().ToLower()}";
            return TryGetAssemblyLocation(text, out path);
        }

        private static string GetPublicKeyTokenFromAssembly(AssemblyName assembly)
        {
            var bytes = assembly.GetPublicKeyToken();
            if (bytes == null || bytes.Length == 0)
                return "None";

            var publicKeyToken = string.Empty;
            for (int i = 0; i < bytes.GetLength(0); i++)
                publicKeyToken += string.Format("{0:x2}", bytes[i]);

            return publicKeyToken;
        }

        /// <summary>
        /// Tries the get assembly path.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">name</exception>
        public static bool TryGetAssemblyLocation([NotNull] string name, out string path)
        {
            //Siemens.Sinumerik.Operate.Services, Version=4.8.5.4, Culture=, PublicKeyToken=bdd90fa02fd1c4ee, processorArchitecture=x86
            //Siemens.Sinumerik.Operate.Services, Version=4.8.5.4, Culture=neutral, PublicKeyToken=bdd90fa02fd1c4ee, processorArchitecture=x86
            if (name == null) throw new ArgumentNullException(nameof(name));
            try
            {
                var finalName = name;
                var info = new WinApi.AssemblyInfo { cchBuf = BufferSize };
                info.currentAssemblyPath = new string(InitialChar, info.cchBuf);

                var result = WinApi.CreateAssemblyCache(out var assemblyCache, 0);
                if (result < WinApi.HResult.SOk)
                {
                    path = null;
                    return false;
                }

                result = assemblyCache.QueryAssemblyInfo(WinApi.QueryAssemblyInfoFlags.None, finalName, ref info);
                if (result != WinApi.HResult.SOk)
                {
                    path = null;
                    return false;
                }
                path = info.currentAssemblyPath;
                return true;
            }
            catch
            {
                path = null;
                return false;
            }
        }

        /// <summary>
        /// Tries the load assembly.
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <param name="assembly">The assembly.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">assemblyName</exception>
        public static bool TryLoadAssembly([NotNull] string assemblyName, out Assembly assembly)
        {
            if (assemblyName == null)
            {
                throw new ArgumentNullException(nameof(assemblyName));
            }
            try
            {
                if (!TryGetAssemblyLocation(assemblyName, out var path))
                {
                    assembly = null;
                    return false;
                }
                assembly = Assembly.LoadFile(path);
                return true;
            }
            catch
            {
                assembly = null;
                return false;
            }
        }

        /// <summary>
        /// Gets the assemblies.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
      
        public static IEnumerable<AssemblyName> GetAssemblies(string name)
        {
            var results = new List<AssemblyName>();

            if (WinApi.CreateAssemblyNameObject(out var assemblyName, name, WinApi.CreateAsmNameObjFlags.ParseFriendDisplayName, IntPtr.Zero) != WinApi.HResult.SOk)
            {
                return results;
            }

            if (WinApi.CreateAssemblyEnum(out var assemblyEnum, null, assemblyName, WinApi.CacheFlags.AsmCacheGac, IntPtr.Zero) != WinApi.HResult.SOk)
            {
                return results;
            }

            if (assemblyName == null)
            {
                return results;
            }

            while (assemblyEnum.GetNextAssembly(out var context, out assemblyName, 0) == (int)WinApi.HResult.SOk)
            {
                uint size = 260;
                var displayName = new StringBuilder((int)size);
                if (assemblyName.GetDisplayName(displayName, ref size, WinApi.DisplayFlags.Full) ==
                    WinApi.HResult.SOk)
                {
                    try
                    {
                        var assembly = displayName.ToString();

                        if (!string.IsNullOrWhiteSpace(assembly))
                        {
                            results.Add(new AssemblyName(assembly));
                        }
                    }
                    catch
                    {
                        // ignore
                    }
                }
            }
            Marshal.ReleaseComObject(assemblyEnum);
            return results;
        }


        public static IEnumerable<string> GetAssemblyNames(string name)
        {
            var results = new List<string>();

            if (WinApi.CreateAssemblyNameObject(out var assemblyName, name, WinApi.CreateAsmNameObjFlags.ParseFriendDisplayName, IntPtr.Zero) != WinApi.HResult.SOk)
            {
                return results;
            }

            if (WinApi.CreateAssemblyEnum(out var assemblyEnum, null, assemblyName, WinApi.CacheFlags.AsmCacheGac, IntPtr.Zero) != WinApi.HResult.SOk)
            {
                return results;
            }

            if (assemblyName == null)
            {
                return results;
            }

            while (assemblyEnum.GetNextAssembly(out var context, out assemblyName, 0) == (int)WinApi.HResult.SOk)
            {
                uint size = 260;
                var displayName = new StringBuilder((int)size);
                if (assemblyName.GetDisplayName(displayName, ref size, WinApi.DisplayFlags.Full) ==
                    WinApi.HResult.SOk)
                {
                    try
                    {
                        var assembly = displayName.ToString();

                        if (!string.IsNullOrWhiteSpace(assembly))
                        {
                            results.Add(assembly);
                        }
                    }
                    catch
                    {
                        // ignore
                    }
                }
            }
            Marshal.ReleaseComObject(assemblyEnum);
            return results;
        }


        public static IEnumerable<AssemblyName> GetAssemblies()
        {
            var results = new List<AssemblyName>();

            if (WinApi.CreateAssemblyEnum(out var assemblyEnum, null, null, WinApi.CacheFlags.AsmCacheGac, IntPtr.Zero) != WinApi.HResult.SOk)
            {
                return results;
            }

            while (assemblyEnum.GetNextAssembly(out var context, out var assemblyName, 0) == (int)WinApi.HResult.SOk)
            {
                uint size = 260;
                var displayName = new StringBuilder((int)size);
                if (assemblyName.GetDisplayName(displayName, ref size, WinApi.DisplayFlags.Full) ==
                    WinApi.HResult.SOk)
                {
                    try
                    {
                        var assembly = displayName.ToString();
                       
                        if (!string.IsNullOrWhiteSpace(assembly))
                        {
                            results.Add(new AssemblyName(assembly));
                        }
                    }
                    catch 
                    {
                        // ignore
                    }
                }
            }
            Marshal.ReleaseComObject(assemblyEnum);
            return results;
        }
    }
}