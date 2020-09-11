using System;
using System.Runtime.InteropServices;
using System.Text;

namespace DynamicWrapperCommon
{
    public static partial class GlobalAssemblyCacheHelper
    {
        public static partial class WinApi
        {
            /// <summary>
            /// The IAssemblyName interface represents a side-by-side assembly name.
            /// The side-by-side assembly name consists of a set of name-value pairs that describe the side-by-side assembly.
            /// An instance of the IAssemblyName interface is obtained by calling the CreateAssemblyNameObject function.
            /// https://docs.microsoft.com/en-us/windows/win32/api/winsxs/nn-winsxs-iassemblyname
            /// </summary>
            [Guid("CD193BC0-B4BC-11d2-9833-00C04FC31D2E")]
            [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
            public interface IAssemblyName
            {
                /// <summary>
                /// Sets the property.
                /// </summary>
                /// <param name="propertyId">The property identifier.</param>
                /// <param name="pvProperty">The pv property.</param>
                /// <param name="cbProperty">The cb property.</param>
                /// <returns></returns>
                [PreserveSig]
                HResult SetProperty(uint propertyId, IntPtr pvProperty, uint cbProperty);

                /// <summary>
                /// Gets the property.
                /// </summary>
                /// <param name="propertyId">The property identifier.</param>
                /// <param name="pvProperty">The pv property.</param>
                /// <param name="pcbProperty">The PCB property.</param>
                /// <returns></returns>
                [PreserveSig]
                HResult GetProperty(uint propertyId, IntPtr pvProperty, ref uint pcbProperty);

                /// <summary>
                /// Finalizes this instance.
                /// </summary>
                /// <returns></returns>
                [PreserveSig]
                HResult Finalize();

                /// <summary>
                /// Gets the display name.
                /// </summary>
                /// <param name="szDisplayName">Display name of the sz.</param>
                /// <param name="pccDisplayName">Display name of the PCC.</param>
                /// <param name="dwDisplayFlags">The dw display flags.</param>
                /// <returns></returns>
                [PreserveSig]
                HResult GetDisplayName([Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder szDisplayName,
                    ref uint pccDisplayName,
                    DisplayFlags dwDisplayFlags);

                /// <summary>
                /// Binds to object.
                /// </summary>
                /// <param name="refIID">The reference iid.</param>
                /// <param name="pAsmBindSink">The p asm bind sink.</param>
                /// <param name="pApplicationContext">The p application context.</param>
                /// <param name="szCodeBase">The sz code base.</param>
                /// <param name="llFlags">The ll flags.</param>
                /// <param name="pvReserved">The pv reserved.</param>
                /// <param name="cbReserved">The cb reserved.</param>
                /// <param name="ppv">The PPV.</param>
                /// <returns></returns>
                [PreserveSig]
                HResult BindToObject(object refIID,
                    object pAsmBindSink,
                    IApplicationContext pApplicationContext,
                    [MarshalAs(UnmanagedType.LPWStr)] string szCodeBase,
                    long llFlags,
                    int pvReserved,
                    uint cbReserved,
                    out int ppv);

                /// <summary>
                /// Gets the name.
                /// </summary>
                /// <param name="lpcwBuffer">The LPCW buffer.</param>
                /// <param name="pwzName">Name of the PWZ.</param>
                /// <returns></returns>
                [PreserveSig]
                HResult GetName(ref uint lpcwBuffer,
                    [Out(), MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwzName);

                /// <summary>
                /// Gets the version.
                /// </summary>
                /// <param name="pdwVersionHi">The PDW version hi.</param>
                /// <param name="pdwVersionLow">The PDW version low.</param>
                /// <returns></returns>
                [PreserveSig]
                HResult GetVersion(out uint pdwVersionHi, out uint pdwVersionLow);

                /// <summary>
                /// Determines whether the specified p name is equal.
                /// </summary>
                /// <param name="pName">Name of the p.</param>
                /// <param name="dwCmpFlags">The dw CMP flags.</param>
                /// <returns></returns>
                [PreserveSig]
                HResult IsEqual(IAssemblyName pName,
                    uint dwCmpFlags);

                /// <summary>
                /// Clones the specified p name.
                /// </summary>
                /// <param name="pName">Name of the p.</param>
                /// <returns></returns>
                [PreserveSig]
                HResult Clone(out IAssemblyName pName);
            }
        }
    }
}