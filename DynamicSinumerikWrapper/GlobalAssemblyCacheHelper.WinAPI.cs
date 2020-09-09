using System.Runtime.InteropServices;

namespace DynamicSinumerikWrapper
{
    public static partial class GlobalAssemblyCacheHelper
    {
        /// <summary>
        /// Fusion
        /// The fusion API enables a runtime host to access the properties of an application's resources in order to locate the correct versions of those resources for the application.
        /// https://docs.microsoft.com/en-us/dotnet/framework/unmanaged-api/fusion/
        /// Error Handling in COM
        /// https://docs.microsoft.com/en-us/windows/win32/learnwin32/error-handling-in-com
        /// </summary>
        private class WinAPI
        {
            public class Result
            {
                /// <summary>
                /// The successful S_OK
                /// </summary>
                public const int Successful = 0x0;

                /// <summary>
                /// The not successful S_FALSE 
                /// </summary>
                public const int NotSuccessful = 0x1;
            }

            /// <summary>
            /// Creates the assembly cache.
            /// </summary>
            /// <param name="ppAsmCache">The pp asm cache.</param>
            /// <param name="reserved">The reserved.</param>
            /// <returns></returns>
            [DllImport("fusion.dll")]
            public static extern int CreateAssemblyCache(out IAssemblyCache ppAsmCache, int reserved);

            /// <summary>
            /// Contains information about an assembly that is registered in the global assembly cache.
            /// https://docs.microsoft.com/en-us/dotnet/framework/unmanaged-api/fusion/assembly-info-structure
            /// </summary>
            [StructLayout(LayoutKind.Sequential)]
            public struct AssemblyInfo
            {
                /// <summary>
                /// The size, in bytes, of the structure. This field is reserved for future extensibility.
                /// </summary>
                public readonly int cbAssemblyInfo;
                /// <summary>
                /// Flags that indicate installation details about the assembly. The following values are supported:
                /// - The ASSEMBLYINFO_FLAG_INSTALLED value, which indicates that the assembly is installed.The current version of the.NET Framework always sets dwAssemblyFlags to this value.
                /// - The ASSEMBLYINFO_FLAG_PAYLOADRESIDENT value, which indicates that the assembly is a payload resident.The current version of the.NET Framework never sets dwAssemblyFlags to this value.
                /// </summary>
                public readonly int assemblyFlags;
                /// <summary>
                /// The total size, in kilobytes, of the files that the assembly contains.
                /// </summary>
                public readonly long assemblySizeInKB;

                /// <summary>
                /// A pointer to a string buffer that holds the current path to the manifest file. The path must end with a null character.
                /// </summary>
                [MarshalAs(UnmanagedType.LPWStr)] public string currentAssemblyPath;

                /// <summary>
                /// The number of wide characters, including the null terminator, that pszCurrentAssemblyPathBuf contains.
                /// </summary>
                public int cchBuf;
            }

            /// <summary>
            /// Represents the global assembly cache for use by the fusion technology.
            /// https://docs.microsoft.com/en-us/dotnet/framework/unmanaged-api/fusion/iassemblycache-interface
            /// </summary>
            [ComImport]
            [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
            [Guid("e707dcde-d1cd-11d2-bab9-00c04f8eceae")]
            public interface IAssemblyCache
            {
                void Reserved0();


                /// <summary>
                /// Queries the assembly information.
                /// https://docs.microsoft.com/en-us/windows/win32/api/winsxs/nf-winsxs-iassemblycache-queryassemblyinfo
                /// </summary>
                /// <param name="flags">The flags.</param>
                /// <param name="assemblyName">Name of the assembly.</param>
                /// <param name="assemblyInfo">The assembly information.</param>
                /// <returns></returns>
                [PreserveSig]
                int QueryAssemblyInfo(int flags, [MarshalAs(UnmanagedType.LPWStr)] string assemblyName,
                    ref AssemblyInfo assemblyInfo);
            }

        }
    }
}