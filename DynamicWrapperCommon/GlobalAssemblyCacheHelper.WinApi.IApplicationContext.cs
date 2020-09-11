using System.Runtime.InteropServices;

namespace DynamicWrapperCommon
{
    public static partial class GlobalAssemblyCacheHelper
    {
        public static partial class WinApi
        {
            /// <summary>
            /// 
            /// </summary>
            [ComImport(), Guid("7C23FF90-33AF-11D3-95DA-00A024A85B51"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
            public interface IApplicationContext
            {
                /// <summary>
                /// Sets the context name object.
                /// </summary>
                /// <param name="pName">Name of the p.</param>
                void SetContextNameObject(IAssemblyName pName);

                /// <summary>
                /// Gets the context name object.
                /// </summary>
                /// <param name="ppName">Name of the pp.</param>
                void GetContextNameObject(out IAssemblyName ppName);

                void Set([MarshalAs(UnmanagedType.LPWStr)] string szName,
                    int pvValue,
                    uint cbValue,
                    uint dwFlags);

                /// <summary>
                /// Gets the specified sz name.
                /// </summary>
                /// <param name="szName">Name of the sz.</param>
                /// <param name="pvValue">The pv value.</param>
                /// <param name="pcbValue">The PCB value.</param>
                /// <param name="dwFlags">The dw flags.</param>
                void Get([MarshalAs(UnmanagedType.LPWStr)] string szName,
                    out int pvValue,
                    ref uint pcbValue,
                    uint dwFlags);

                /// <summary>
                /// Gets the dynamic directory.
                /// </summary>
                /// <param name="wzDynamicDir">The wz dynamic dir.</param>
                /// <param name="pdwSize">Size of the PDW.</param>
                void GetDynamicDirectory(out int wzDynamicDir,
                    ref uint pdwSize);
            }
        }
    }
}