using System.Runtime.InteropServices;

namespace DynamicWrapperCommon
{
    public static partial class GlobalAssemblyCacheHelper
    {
        public static partial class WinApi
        {
            /// <summary>
            ///     Represents the global assembly cache for use by the fusion technology.
            ///     https://docs.microsoft.com/en-us/dotnet/framework/unmanaged-api/fusion/iassemblycache-interface
            /// </summary>
            [ComImport]
            [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
            [Guid("e707dcde-d1cd-11d2-bab9-00c04f8eceae")]
            public interface IAssemblyCache
            {
                /// <summary>
                /// Reserved0s this instance.
                /// </summary>
                void Reserved0();

                /// <summary>
                ///     Queries the assembly information.
                ///     https://docs.microsoft.com/en-us/windows/win32/api/winsxs/nf-winsxs-iassemblycache-queryassemblyinfo
                /// </summary>
                /// <param name="flags">The flags.</param>
                /// <param name="assemblyName">Name of the assembly.</param>
                /// <param name="assemblyInfo">The assembly information.</param>
                /// <returns></returns>
                [PreserveSig]
                HResult QueryAssemblyInfo(QueryAssemblyInfoFlags flags, [MarshalAs(UnmanagedType.LPWStr)] string assemblyName,
                    ref AssemblyInfo assemblyInfo);
            }
        }
    }
}