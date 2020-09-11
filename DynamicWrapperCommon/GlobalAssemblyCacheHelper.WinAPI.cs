using System;
using System.Runtime.InteropServices;

namespace DynamicWrapperCommon
{
    public static partial class GlobalAssemblyCacheHelper
    {
        /// <summary>
        ///     Fusion
        ///     The fusion API enables a runtime host to access the properties of an application's resources in order to locate the
        ///     correct versions of those resources for the application.
        ///     https://docs.microsoft.com/en-us/dotnet/framework/unmanaged-api/fusion/
        ///     Error Handling in COM
        ///     https://docs.microsoft.com/en-us/windows/win32/learnwin32/error-handling-in-com
        /// </summary>
        public static partial class WinApi
        {
            /// <summary>
            ///     Creates the assembly cache.
            /// </summary>
            /// <param name="ppAsmCache">The pp asm cache.</param>
            /// <param name="reserved">The reserved.</param>
            /// <returns></returns>
            [DllImport("fusion.dll")]
            public static extern HResult CreateAssemblyCache(out IAssemblyCache ppAsmCache, int reserved);

            /// <summary>
            /// Creates the assembly enum.
            /// </summary>
            /// <param name="pEnum">The p enum.</param>
            /// <param name="pAppCtx">The p application CTX.</param>
            /// <param name="pName">Name of the p.</param>
            /// <param name="dwFlags">The dw flags.</param>
            /// <param name="pvReserved">The pv reserved.</param>
            /// <returns></returns>
            [DllImport("Fusion.dll", SetLastError = true)]
            public static extern HResult CreateAssemblyEnum(out IAssemblyEnum pEnum, IApplicationContext pAppCtx,
                IAssemblyName pName, CacheFlags dwFlags, IntPtr pvReserved);

            /// <summary>
            ///     Creates the assembly name object.
            /// </summary>
            /// <param name="ppAssemblyNameObj">The pp assembly name object.</param>
            /// <param name="szAssemblyName">Name of the sz assembly.</param>
            /// <param name="flags">The flags.</param>
            /// <param name="pvReserved">The pv reserved.</param>
            /// <returns></returns>
            [DllImport("Fusion.dll", SetLastError = true, CharSet = CharSet.Unicode)]
            public static extern HResult CreateAssemblyNameObject(out IAssemblyName ppAssemblyNameObj,
                [MarshalAs(UnmanagedType.LPWStr)] string szAssemblyName, CreateAsmNameObjFlags flags,
                IntPtr pvReserved);
        }
    }
}