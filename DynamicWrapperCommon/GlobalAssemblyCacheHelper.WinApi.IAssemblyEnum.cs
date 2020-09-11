using System.Runtime.InteropServices;

namespace DynamicWrapperCommon
{
    public static partial class GlobalAssemblyCacheHelper
    {
        public static partial class WinApi
        {
            /// <summary>
            /// Represents an enumerator for an array of IAssemblyName objects.
            /// https://docs.microsoft.com/en-us/dotnet/framework/unmanaged-api/fusion/iassemblyenum-interface
            /// </summary>
            [ComImport]
            [Guid("21B8916C-F28E-11D2-A473-00C04F8EF448")]
            [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
            public interface IAssemblyEnum
            {
                /// <summary>
                /// Gets the next assembly.
                /// </summary>
                /// <param name="ppAppCtx">The pp application CTX.</param>
                /// <param name="ppName">Name of the pp.</param>
                /// <param name="dwFlags">The dw flags.</param>
                /// <returns></returns>
                [PreserveSig]
                HResult GetNextAssembly(out IApplicationContext ppAppCtx,
                    out IAssemblyName ppName,
                    uint dwFlags);

                /// <summary>
                /// Resets this instance.
                /// </summary>
                /// <returns></returns>
                [PreserveSig]
                HResult Reset();

                /// <summary>
                /// Clones the specified pp enum.
                /// </summary>
                /// <param name="ppEnum">The pp enum.</param>
                /// <returns></returns>
                [PreserveSig]
                HResult Clone(out IAssemblyEnum ppEnum);
            }
        }
    }
}