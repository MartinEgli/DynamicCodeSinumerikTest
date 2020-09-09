using System;

namespace DynamicSinumerikWrapper
{
    public static partial class GlobalAssemblyCacheHelper
    {
        /// <summary>
        /// The buffer size
        /// </summary>
        private const int BufferSize = 1024;

        /// <summary>
        /// The initial character
        /// </summary>
        private const char InitialChar = '\0';

        /// <summary>
        /// Gets the assembly path.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">name</exception>
        public static string GetAssemblyPath(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            
            var finalName = name;
            var info = new GlobalAssemblyCacheHelper.WinAPI.AssemblyInfo { cchBuf = BufferSize};
            info.currentAssemblyPath = new string(InitialChar, info.cchBuf);

            GlobalAssemblyCacheHelper.WinAPI.IAssemblyCache assemblyCache;
            var result = GlobalAssemblyCacheHelper.WinAPI.CreateAssemblyCache(out assemblyCache, 0);
            if (result < GlobalAssemblyCacheHelper.WinAPI.Result.Successful)
            {
                return info.currentAssemblyPath;
            }

            result  = assemblyCache.QueryAssemblyInfo(0, finalName, ref info);
            if (result < GlobalAssemblyCacheHelper.WinAPI.Result.Successful)
            {
                return null;
            }

            return info.currentAssemblyPath;
        }
    }
}